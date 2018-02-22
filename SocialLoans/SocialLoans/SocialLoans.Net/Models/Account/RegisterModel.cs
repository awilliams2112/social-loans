using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SocialLoans.Net.Extensions;

namespace SocialLoans.Net.Models
{
    public class RegisterModel
    {
        public RegisterModel()
        {
            Errors = new List<string>();
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name ="Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        public DateTime Dob
        {
            get
            {
                DateTime dob;

                if(DateTime.TryParse(DobString, out dob))
                {
                    return dob;
                }

                return DateTime.MinValue;
            }
        }

        [Display(Name ="Date of birth (Format DD/MM/YYYY")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of birth is required")]
        public string DobString { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be in correct format, ex name@nomail.com")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        public string ConfirmPassword { get; set; }
        
        [Display(Name = "Agree to terms of Services?")]
        [IsTrue(ErrorMessage = "Must agree to terms of service")]
        public bool AgreeToTermsOfService { get; set; }

        public List<string> Errors { get; set; }
    }
}
