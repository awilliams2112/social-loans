using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialLoans.Net.ViewModels
{
    public class LoginModel
    {
        public LoginModel()
        {
            Errors = new List<string>();
        }

        [Required]
        public string Email { get; set;}

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        
        public List<string> Errors { get; set; }

        public string ReturnUrl { get; set; }
    }
        
}
