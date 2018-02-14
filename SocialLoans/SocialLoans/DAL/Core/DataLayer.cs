using SocialLoans.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Linq;
using DAL.Domains.Abstractions;
using DAL.Domains;
using DAL.New;
using Microsoft.EntityFrameworkCore.Storage;

namespace DAL
{
    public class DataDomains : IDataDomains
    {
        ILog log;
        ApplicationDbContext context;

        ILoggingDAL logging;

        public DataDomains(ApplicationDbContext context, ILog log)
        {
            this.context = context;
            this.log = log;
        }

        public ILoggingDAL Logging
        {
            get
            {
                if (logging != null)
                {
                    logging = new LoggingDAL(context);
                }
                return logging;
            }
        }

        public IPaymentsDAL PaymentDomainDL => throw new NotImplementedException();

        public IDbContextTransaction BeginTransaction()
        {
            return this.context.Database.BeginTransaction();
        }

        public void Commit()
        {
            this.context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            this.context.Database.RollbackTransaction();
        }
        
    }
}
