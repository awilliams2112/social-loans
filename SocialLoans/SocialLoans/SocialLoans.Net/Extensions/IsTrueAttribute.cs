using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialLoans.Net.Extensions
{
    public class IsTrueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
        
        
        public override bool IsValid(object value)
        {
            if (value is Boolean)
            {
                return (bool)value == true;
            }

            return false;
        }

    }
}
