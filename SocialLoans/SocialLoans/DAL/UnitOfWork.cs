// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using SocialLoans.Logging;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;

        ICustomerRepository _customers;
        IProductRepository _products;
        IOrdersRepository _orders;

        ILoanRespository _loans;
        ILoanApplicationsRepository _loanApplications; 
        IBankAccountRepository _bankAccounts; 
        ITransactionRepository _transactions; 

        IDisclosureRepository _disclosures;


        IImportRepository _imports;

        ILogger log;

        public UnitOfWork(ApplicationDbContext context, ILogger log)
        {
            _context = context;
            this.log = log;
        }



        public ICustomerRepository Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new CustomerRepository(_context);

                return _customers;
            }
        }
        
        public IProductRepository Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductRepository(_context);

                return _products;
            }
        }
        
        public IOrdersRepository Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new OrdersRepository(_context);

                return _orders;
            }
        }
        
        public ILoanRespository Loans
        {
            get
            {
                if (_loans == null)
                    _loans = new LoanRespository(_context);

                return _loans;
            }
        }
        
        public IDisclosureRepository Disclosures
        {
            get
            {
                if (_disclosures == null)
                    _disclosures = new DisclosureRepository(_context);

                return _disclosures;
            }
        }

        public ILoanApplicationsRepository LoanApplications
        {
            get
            {
                if (_loanApplications == null)
                    _loanApplications = new LoanApplicationsRepository(_context);

                return _loanApplications;
            }
        } 

        public IBankAccountRepository BankAccounts
        {
            get
            {
                if (_bankAccounts == null)
                    _bankAccounts = new BankAccountRepository(_context);

                return _bankAccounts;
            }
        }

        public ITransactionRepository Transactions
        {
            get
            {
                if (_transactions == null)
                    _transactions = new TransactionRepository(_context);

                return _transactions;
            }
        }

        public IImportRepository Imports
        {
            get
            {
                if (_imports == null)
                    _imports = new ImportRespository(_context, log);

                return _imports;
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
