using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        BankAccount GetActive(string userId);
    }

    public class BankAccountRepository : Repository<BankAccount>,  IBankAccountRepository
    {
        ApplicationDbContext context;

        public BankAccountRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public BankAccount GetActive(string userId)
        {
            BankAccount acct = context.BankAccounts.FirstOrDefault(a => a.IsActive);

            return acct;
        }

    }
}
