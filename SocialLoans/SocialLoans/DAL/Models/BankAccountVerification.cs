using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class BankAccountVerification
    {
        public int Deposit1 { get; set; }
        public int Deposit2 { get; set; }
        
        public DateTime? VerifiedDate { get; set; }
        public bool IsVerified { get { return VerifiedDate.HasValue; } }

        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public string VerficationIPv4 { get; set; }
        public string Device { get; set; }
        public string DeviceId { get; set; }

    }
}
