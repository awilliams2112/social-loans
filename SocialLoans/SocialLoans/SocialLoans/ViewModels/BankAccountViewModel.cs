using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialLoans.ViewModels
{
    public class BankAccountViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Routing { get; set; }

        [Required]
        public string AccountNumber { get; set; }
        
        public bool IsActive { get; set; }
    }
}
