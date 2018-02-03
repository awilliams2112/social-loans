using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Loan : AuditableEntity
    {
        public int Id { get; set; }

        public string PaymentFrequency { get; set; }

        public decimal Principal { get; set; }

        public decimal Interest { get; set; }

        public decimal InterestCutPercentage { get; set; }

        public decimal PaymentAmount { get; set; }

        public decimal PaymentAmountCut { get; set; }

        public DateTime MaturityDate { get; set; }

        public DateTime FirstPaymentDate { get; set; }

        public string BorrowerId { get; set; }

        public int LoanStatusId { get; set; }

        public LoanStatus LoanStatus { get; set; }

        public ApplicationUser Borrower { get; set; }

        public string LenderId { get; set; }

        public ApplicationUser Lender { get; set; }

    }

    public class LoanStatus : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsNegative { get; internal set; }
    }
}
