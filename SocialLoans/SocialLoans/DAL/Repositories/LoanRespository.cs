using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    public interface ILoanRespository : IRepository<Loan>
    {
        List<Loan> GetLoansInNegativeStatus(string userId);
        List<Loan> GetAllLoansAsBorrower(string applicationUserId);
    }

    public class LoanRespository : Repository<Loan>, ILoanRespository
    {
        ApplicationDbContext context;

        public LoanRespository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public List<Loan> GetAllLoansAsBorrower(string applicationUserId)
        {
            return context.Loans.Where(l => l.BorrowerId == applicationUserId).ToList();
        }

        public List<Loan> GetLoansInNegativeStatus(string userId)
        {
            return context.Loans.Where(l => l.LoanStatus.IsNegative).ToList();
        }
    }
}
