using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class BankAccount : AuditableEntity
    {
        public int Id { get; set; }
        
        public string StripeIdBankAccount { get; set; }

        public string Routing { get; set; }

        public string Last4AccountNumber { get; set; }

        public bool IsVerified { get; set; }

        public bool IsAuthorized { get; set; }

        public bool IsActive { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
