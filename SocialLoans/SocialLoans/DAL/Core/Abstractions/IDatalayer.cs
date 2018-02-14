using DAL.Domains.Abstractions;
using DAL.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.New
{
    public interface IDataDomains
    {

        IDbContextTransaction BeginTransaction();
        void Commit();
        void Rollback();


        ILoggingDAL Logging { get; }
        IPaymentsDAL PaymentDomainDL { get; }

    }
}
