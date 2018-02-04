// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        IOrdersRepository Orders { get; }
        IDisclosureRepository Disclosures { get; }
        ILoanRespository Loans { get; }
        ILoanApplicationsRepository LoanApplications { get; }
        IBankAccountRepository BankAccounts { get; }
        ITransactionRepository Transactions { get; }
        IImportRepository Imports { get; }
        IAccountRepository Accounts { get; }

        IDbContextTransaction BeginTransaction();

        int SaveChanges();
    }

    
}
