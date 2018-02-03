using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using DAL.Models;
using Transaction = DAL.Models.Transaction;
using System.Linq;

namespace DAL.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Transaction GetLastTransaction(int bankAccountId);
    }

    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        ApplicationDbContext context;

        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        

        public Transaction GetLastTransaction(int bankAccountId)
        {
            return context.Transactions.OrderByDescending(t => t.CreatedDate).FirstOrDefault();
        }

    }
}
