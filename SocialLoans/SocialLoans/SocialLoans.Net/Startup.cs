// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Newtonsoft.Json;
using DAL.Core;
using DAL.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SocialLoans.ViewModels;
using SocialLoans.Helpers;
using SocialLoans.Authorization;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Swagger;
using AppPermissions = DAL.Core.ApplicationPermissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using SocialLoans.PaymentApi;
using SocialLoans.PaymentApi.Stripe;
using Microsoft.AspNetCore.Authentication.Cookies;
using SocialLoans.Domain;
using SocialLoans.Communications;
using DAL.New;

namespace SocialLoans
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        //private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration/*, IHostingEnvironment env*/)
        {
            Configuration = configuration;
            //_hostingEnvironment = env;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("SocialLoans"));
               
                //options.UseOpenIddict();
            });

            // add identity

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Add cookie authentication

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

            //maybe useful
            //https://stackoverflow.com/questions/42105434/asp-net-core-change-default-redirect-for-unauthorized/45922216#45922216
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(WebSettings.LoginExpireMinutes);
            });


            // Configure Identity options and password complexity here
            //services.Configure<IdentityOptions>(options =>
            //{
            //    // User settings
            //    options.User.RequireUniqueEmail = true;

            //    //    //// Password settings
            //    //    //options.Password.RequireDigit = true;
            //    //    //options.Password.RequiredLength = 8;
            //    //    //options.Password.RequireNonAlphanumeric = false;
            //    //    //options.Password.RequireUppercase = true;
            //    //    //options.Password.RequireLowercase = false;

            //    //    //// Lockout settings
            //    //    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //    //    //options.Lockout.MaxFailedAccessAttempts = 10;

            //    //options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
            //    //options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
            //    //options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            //});


            // Register the OpenIddict services.
            //services.AddOpenIddict(options =>
            //{
            //    options.AddEntityFrameworkCoreStores<ApplicationDbContext>();
            //    options.AddMvcBinders();
            //    options.EnableTokenEndpoint("/connect/token");
            //    options.AllowPasswordFlow();
            //    options.AllowRefreshTokenFlow();

            //    //if (_hostingEnvironment.IsDevelopment()) //Uncomment to only disable Https during development
            //    options.DisableHttpsRequirement();

            //    //options.UseRollingTokens(); //Uncomment to renew refresh tokens on every refreshToken request
            //    //options.AddSigningKey(new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration["STSKey"])));
            //});


            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = OAuthValidationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = OAuthValidationDefaults.AuthenticationScheme;

            //}).AddOAuthValidation();

            //services.AddAuthentication("Cookieauth").AddCookie()
            //.AddCookie("Cookieauth", options =>
            //{
            //    options.LoginPath = new PathString("~/login");
            //    options.AccessDeniedPath = new PathString("/error?unauth");
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            //});

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options => {
            //    options.LoginPath = "/login/";
            //});

            //services.AddAuthentication(options => { options.DefaultAuthenticateScheme = "Cookie"; }).AddCookie(options =>
            //{
            //    options.LoginPath = new PathString("~/login");
            //    options.AccessDeniedPath = new PathString("/error?unauth");
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            //});

            // Add cors
            services.AddCors();

            // Add framework services.
            services.AddMvc();


            // Enforce https during production. To quickly enable ssl during development. Go to: Project Properties->Debug->Enable SSL
            //if (!_hostingEnvironment.IsDevelopment())
            //    services.Configure<MvcOptions>(options => options.Filters.Add(new RequireHttpsAttribute()));


            //Todo: ***Using DataAnnotations for validation until Swashbuckle supports FluentValidation***
            //services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());


            //.AddJsonOptions(opts =>
            //{
            //    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //});



            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("BearerAuth", new ApiKeyScheme
                {
                    Name = "Authorization",
                    Description = "Login with your bearer authentication token. e.g. Bearer <auth-token>",
                    In = "header",
                    Type = "apiKey"
                });

                c.SwaggerDoc("v1", new Info { Title = "SocialLoans API", Version = "v1" });
            });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(Authorization.Policies.ViewAllUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ViewUsers));
            //    options.AddPolicy(Authorization.Policies.ManageAllUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageUsers));

            //    options.AddPolicy(Authorization.Policies.ViewAllRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ViewRoles));
            //    options.AddPolicy(Authorization.Policies.ViewRoleByRoleNamePolicy, policy => policy.Requirements.Add(new ViewRoleAuthorizationRequirement()));
            //    options.AddPolicy(Authorization.Policies.ManageAllRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageRoles));

            //    options.AddPolicy(Authorization.Policies.AssignAllowedRolesPolicy, policy => policy.Requirements.Add(new AssignRolesAuthorizationRequirement()));
            //});

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });


            // Configurations
            services.Configure<SmtpConfig>(Configuration.GetSection("SmtpConfig"));


            // Business Services
            services.AddScoped<IEmailer, Emailer>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IStripeService, StripeService>();
            services.AddScoped<ISocialLoansAuthentication, SocialLoansAuthentication>();
            services.AddScoped<IDataDomains, DataDomains>();
            services.AddScoped<ICommunicationService, CommunicationService>();

            // Repositories
            services.AddScoped<SocialLoans.Logging.ILog, SocialLoans.Logging.NullLogger>();
            services.AddScoped<IUnitOfWork, HttpUnitOfWork>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<ISmsSender, SmsSender>();

            

            // Auth Handlers
            services.AddSingleton<IAuthorizationHandler, ViewUserAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ManageUserAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ViewRoleAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, AssignRolesAuthorizationHandler>();

            // DB Creation and Seeding
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Warning);
            loggerFactory.AddFile(Configuration.GetSection("Logging"));

            Utilities.ConfigureLogger(loggerFactory);
            EmailTemplates.Initialize(env);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                // Enforce https during production
                //var rewriteOptions = new RewriteOptions()
                //    .AddRedirectToHttps();
                //app.UseRewriter(rewriteOptions);

                app.UseExceptionHandler("/Home/Error");
            }
            

            //Configure Cors
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());


            app.UseStaticFiles();
            app.UseAuthentication();
            
            // for login reroute configuration
            //https://stackoverflow.com/questions/40217623/redirect-to-login-when-unauthorized-in-asp-net-core
            

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.ApplicationJson;

                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        string errorMsg = JsonConvert.SerializeObject(new { error = error.Error.Message });
                        await context.Response.WriteAsync(errorMsg).ConfigureAwait(false);
                    }
                });
            });


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialLoans API V1");
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class WebSettings
    {
        public static int LoginExpireMinutes = 30;
    }
}
