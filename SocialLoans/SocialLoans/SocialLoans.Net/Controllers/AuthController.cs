// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNet.Security.OpenIdConnect.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using AspNet.Security.OpenIdConnect.Server;
using OpenIddict.Core;
using AspNet.Security.OpenIdConnect.Primitives;
using DAL.Models;
using DAL.Core;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using SocialLoans.Net.ViewModels;
using DAL.Core.Interfaces;
using SocialLoans.Logging;
using SocialLoans.Domain;
using SocialLoans.Net.Models;
using Microsoft.AspNetCore.Authorization;
using SocialLoans.Net.Models;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860


namespace SocialLoans.Controllers
{
    public class AuthController : Controller
    {
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountManager _accountManager;
        private readonly ILog _logging;
        private readonly ISocialLoansAuthentication socialLoansAuthentication;

        public AuthController(
            IOptions<IdentityOptions> identityOptions,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IAccountManager accountManager,
            ISocialLoansAuthentication socialLoansAuthentication,
            ILog logging)
       {
            _identityOptions = identityOptions;
            _signInManager = signInManager;
            _userManager = userManager;
            _logging = logging;

            _accountManager = accountManager;

            this.socialLoansAuthentication = socialLoansAuthentication;
        }

        [HttpGet("~/login")]
        public IActionResult Login(string returnUrl)
        {
            LoginModel model = new LoginModel();

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost("~/login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model, string returnUrl)
        {
            _logging.Info($"User {model.Email} Login Attempt");

            if (ModelState.IsValid)
            {
                ApplicationUser user = _accountManager.GetUserByEmailAsync(model.Email).Result;

                if(user == null)
                {
                    _logging.Info($"User {model.Email} Login Result: Unsuccessful");
                    _logging.Info($"User {model.Email} does not exists");

                    model.Errors.Add("Sorry, Invalid Email or Password");
                    return View(model);
                }
                
                var signInResult = _signInManager.CheckPasswordSignInAsync(user, model.Password, model.RememberMe).Result;
                
                if(signInResult.IsLockedOut)
                {
                    _logging.Info($"User {model.Email} Login Result: Unsuccessful");
                    _logging.Info($"User {model.Email} is locked out");

                    model.Errors.Add("Sorry, your account has been locked out");
                    return View(model);
                }

                if(signInResult.IsNotAllowed)
                {
                    _logging.Info($"User {model.Email} Login Result: Unsuccessful");
                    _logging.Info($"User {model.Email} is not allowed");

                    model.Errors.Add("Sorry, there is a problem with your account");
                    return View(model);
                }
                
                if (signInResult.Succeeded)
                {
                    _logging.Info($"User {model.Email} Login Result: Sucessful");
                    
                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");

                    return RedirectToRoute(returnUrl);
                }

                _logging.Info($"User {model.Email} Login Result: Unsuccessful");

                model.Errors.Add("Sorry, invalid Email and Password");
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet("~/register")]
        public IActionResult Register()
        {
            RegisterModel model = new RegisterModel();

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost("~/register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO don't allow minors 
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    DateOfBirth = model.Dob
                };

                IdentityResult result = _userManager.CreateAsync(user, model.Password).Result;

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    RedirectToAction("AccountSetup", "Account");
                }


                foreach(var err in result.Errors)
                {
                    model.Errors.Add(err.Description);
                }
            }
            
            return View(model);
        }

        public IActionResult PhoneConfirmation()
        {
            return View();
        }

        [HttpPost("~/api/auth/phone")]
        public IActionResult SendPhoneConfirmCode(string phone)
        {
            string username = HttpContext.User.Identity.Name;

            ApplicationUser user = _accountManager.GetUserByEmailAsync(username).Result;
            
            if(user == null)
            {
                _logging.Error($"USER {user.Email} NOT FOUND");

                return BadRequest("user not found");
            }

            socialLoansAuthentication.SendPhoneConfirmationCode(user, phone);

            return NoContent();
        }

        [HttpPost("~/api/auth/phone/confirm")]
        [Produces(typeof(PhoneConfirmResult))]
        public IActionResult PhoneConfirmCode(PhoneConfirmModel model)
        {
            string username = HttpContext.User.Identity.Name;

            ApplicationUser user = _accountManager.GetUserByEmailAsync(username).Result;

            if (user == null)
            {

                _logging.Error($"USER {user.Email} NOT FOUND");

                return BadRequest("user not found");   
            }

            PhoneConfirmResult result = socialLoansAuthentication.PhoneConfirm(user.Email, model.Code);

            return Ok(result);
        }



        [HttpPost("~/connect/token")]
        [Produces("application/json")]
        public async Task<IActionResult> Exchange(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                var user = await _userManager.FindByEmailAsync(request.Username) ?? await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "Please check that your email and password is correct"
                    });
                }

                // Ensure the user is enabled.
                if (!user.IsEnabled)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The specified user account is disabled"
                    });
                }


                // Validate the username/password parameters and ensure the account is not locked out.
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

                // Ensure the user is not already locked out.
                if (result.IsLockedOut)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The specified user account has been suspended"
                    });
                }

                // Reject the token request if two-factor authentication has been enabled by the user.
                if (result.RequiresTwoFactor)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "Invalid login procedure"
                    });
                }

                // Ensure the user is allowed to sign in.
                if (result.IsNotAllowed)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The specified user is not allowed to sign in"
                    });
                }

                if (!result.Succeeded)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "Please check that your email and password is correct"
                    });
                }



                // Create a new authentication ticket.
                var ticket = await CreateTicketAsync(request, user);

                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }
            else if (request.IsRefreshTokenGrantType())
            {
                // Retrieve the claims principal stored in the refresh token.
                var info = await HttpContext.AuthenticateAsync(OpenIdConnectServerDefaults.AuthenticationScheme);

                // Retrieve the user profile corresponding to the refresh token.
                // Note: if you want to automatically invalidate the refresh token
                // when the user password/roles change, use the following line instead:
                // var user = _signInManager.ValidateSecurityStampAsync(info.Principal);
                var user = await _userManager.GetUserAsync(info.Principal);
                if (user == null)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The refresh token is no longer valid"
                    });
                }

                // Ensure the user is still allowed to sign in.
                if (!await _signInManager.CanSignInAsync(user))
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The user is no longer allowed to sign in"
                    });
                }

                // Create a new authentication ticket, but reuse the properties stored
                // in the refresh token, including the scopes originally granted.
                var ticket = await CreateTicketAsync(request, user);

                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }
            return BadRequest(new OpenIdConnectResponse
            {
                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                ErrorDescription = "The specified grant type is not supported"
            });
        }

        private async Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, ApplicationUser user)
        {
            // Create a new ClaimsPrincipal containing the claims that
            // will be used to create an id_token, a token or a code.
            var principal = await _signInManager.CreateUserPrincipalAsync(user);

            // Create a new authentication ticket holding the user identity.
            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(), OpenIdConnectServerDefaults.AuthenticationScheme);


            //if (!request.IsRefreshTokenGrantType())
            //{
            // Set the list of scopes granted to the client application.
            // Note: the offline_access scope must be granted
            // to allow OpenIddict to return a refresh token.
            ticket.SetScopes(new[]
            {
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Email,
                    OpenIdConnectConstants.Scopes.Phone,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess,
                    OpenIddictConstants.Scopes.Roles
            }.Intersect(request.GetScopes()));
            //}

            ticket.SetResources(request.GetResources());

            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.

            foreach (var claim in ticket.Principal.Claims)
            {
                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                if (claim.Type == _identityOptions.Value.ClaimsIdentity.SecurityStampClaimType)
                    continue;


                var destinations = new List<string> { OpenIdConnectConstants.Destinations.AccessToken };

                // Only add the iterated claim to the id_token if the corresponding scope was granted to the client application.
                // The other claims will only be added to the access_token, which is encrypted when using the default format.
                if ((claim.Type == OpenIdConnectConstants.Claims.Subject && ticket.HasScope(OpenIdConnectConstants.Scopes.OpenId)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Name && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Role && ticket.HasScope(OpenIddictConstants.Claims.Roles)) ||
                    (claim.Type == CustomClaimTypes.Permission && ticket.HasScope(OpenIddictConstants.Claims.Roles)))
                {
                    destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
                }


                claim.SetDestinations(destinations);
            }


            var identity = principal.Identity as ClaimsIdentity;


            if (ticket.HasScope(OpenIdConnectConstants.Scopes.Profile))
            {
                if (!string.IsNullOrWhiteSpace(user.JobTitle))
                    identity.AddClaim(CustomClaimTypes.JobTitle, user.JobTitle, OpenIdConnectConstants.Destinations.IdentityToken);

                if (!string.IsNullOrWhiteSpace(user.FullName))
                    identity.AddClaim(CustomClaimTypes.FullName, user.FullName, OpenIdConnectConstants.Destinations.IdentityToken);

                if (!string.IsNullOrWhiteSpace(user.Configuration))
                    identity.AddClaim(CustomClaimTypes.Configuration, user.Configuration, OpenIdConnectConstants.Destinations.IdentityToken);
            }

            if (ticket.HasScope(OpenIdConnectConstants.Scopes.Email))
            {
                if (!string.IsNullOrWhiteSpace(user.Email))
                    identity.AddClaim(CustomClaimTypes.Email, user.Email, OpenIdConnectConstants.Destinations.IdentityToken);
            }

            if (ticket.HasScope(OpenIdConnectConstants.Scopes.Phone))
            {
                if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                    identity.AddClaim(CustomClaimTypes.Phone, user.PhoneNumber, OpenIdConnectConstants.Destinations.IdentityToken);
            }


            return ticket;
        }
    }
}
