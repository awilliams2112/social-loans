using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Transaction : AuditableEntity
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }

        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        public int StatusId { get; set; }

        public TransactionStatus Status { get; set; }
    }

    public class TransactionStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsNegative { get; set; }
    }
}
