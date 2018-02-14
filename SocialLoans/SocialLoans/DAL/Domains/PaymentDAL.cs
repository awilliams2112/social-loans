using DAL.Domains.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Linq;

namespace DAL.Domains
{
    public class PaymentDAL : IPaymentsDAL
    {
        ApplicationDbContext context;

        public PaymentDAL(ApplicationDbContext context)
        {
            this.context = context;
        }

        public BankAccount GetActiveBankAccount(string userId)
        {
            return context.BankAccounts.FirstOrDefault(b => b.UserId == userId && b.IsActive);
        }

        public void InsertBankAccount(BankAccount account)
        {
            context.BankAccounts.Add(account);

            context.SaveChanges();
        }

        public void UpdateBankAccount(BankAccount account)
        {
            context.BankAccounts.Update(account);

            context.SaveChanges();
        }
        public List<RoutingNumber> GetAllRoutingNumbers()
        {
            return context.RoutingNumbers.ToList();
        }

        public BankAccount GetBankAccount(object bankAccountId)
        {
            throw new NotImplementedException();
        }
    }
}
