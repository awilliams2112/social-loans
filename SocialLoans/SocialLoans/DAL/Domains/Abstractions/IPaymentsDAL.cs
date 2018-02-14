using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domains.Abstractions
{
    public interface IPaymentsDAL
    {
        BankAccount GetActiveBankAccount(string userId);
        void InsertBankAccount(BankAccount account);
        void UpdateBankAccount(BankAccount account);

        List<RoutingNumber> GetAllRoutingNumbers();
        BankAccount GetBankAccount(object bankAccountId);
    }
}
