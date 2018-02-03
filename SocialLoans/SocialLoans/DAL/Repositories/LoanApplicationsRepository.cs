using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public interface ILoanApplicationsRepository : IRepository<LoanQualificationApplication>
    {
        LoanQualificationApplication GetLatestApp(string userId);
    }

    public class LoanApplicationsRepository : Repository<LoanQualificationApplication>, ILoanApplicationsRepository
    {
        ApplicationDbContext context;

        public LoanApplicationsRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public LoanQualificationApplication GetLatestApp(string userId)
        {
            var app = context.LoanApplications
                        .Where(a => a.ApplicationUserId == userId)
                        .OrderByDescending(a => a.CreatedDate)
                        .FirstOrDefault();

            return app;
        }
    }
}
