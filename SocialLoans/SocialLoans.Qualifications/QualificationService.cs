using DAL;
using DAL.Core;
using DAL.Core.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;

namespace SocialLoans.Qualifications
{
    public class QualificationService
    {
        IAccountManager accountManager;
        IUnitOfWork unitOfWork;
        QualificationSettings settings;

        public QualificationService(IAccountManager accountManager, IUnitOfWork unitOfWork)
        {
            this.accountManager = accountManager;
            this.unitOfWork = unitOfWork;

            settings = new QualificationSettings();
        }

        public QualificationResult Qualify(string userId)
        {
            QualificationResult result = new QualificationResult();

            QualificationSettings settings = new QualificationSettings();

            LoanQualificationApplication app = unitOfWork.LoanApplications.GetLatestApp(userId);

            if (app.NetIncome < settings.MinimumNetIncome)
            {
                result.QualifyAmount = 0;
                result.DeniedReason = "Net income doesn't meet required minimum";
                return result;
            }

            result.QualifyAmount = QualifyAmount(app);

            if(DateTime.Now > (app.CreatedDate.AddDays(settings.AppValidDays)))
            {
                result.DeniedReason = "Qualification out of date";
                return result;
            }
            
            List<Loan> loansInNegStatus = unitOfWork.Loans.GetLoansInNegativeStatus(userId);

            if (loansInNegStatus.Count > 0)
            {
                result.DeniedReason = "laon(s) in negative status";
                return result;
            }

            

            BankAccount activeBankAccount = unitOfWork.BankAccounts.GetActive(userId);

            if (activeBankAccount == null)
            {
                result.DeniedReason = "No Active Bank account";
                return result;
            }

            Transaction lastTransaction = unitOfWork.Transactions.GetLastTransaction(activeBankAccount.Id);

            if (lastTransaction != null &&
                lastTransaction.StatusId == (int)TransactionStatuses.Bounced ||
                lastTransaction.StatusId == (int)TransactionStatuses.Failed)
            {

                result.DeniedReason = "Last Transaction Failed";
                return result;
            }

            if (activeBankAccount.IsAuthorized)
            {
                result.DeniedReason = "Bank Account Not Authorized";
                return result;
            }

            if (activeBankAccount.IsVerified)
            {
                result.DeniedReason = "Bank Account Not Verified";
                return result;
            }



            return result;
        }

        public decimal QualifyAmount(LoanQualificationApplication app)
        {
            decimal amount = app.NetIncome * settings.NetIncomeThresold;

            List<Loan> loans = unitOfWork.Loans.GetAllLoansAsBorrower(app.ApplicationUserId);

            int loanCount = loans.Count;

            amount = loanCount == 0 ?
                amount > settings.MaxiumAmountOnFirstLoan ? settings.MaxiumAmountOnFirstLoan : amount
                : amount;

            return amount;
        }
    }

    public class QualifyAmountResult 
    {
        public decimal Amount { get; set; }
        public string Note { get; set; }
    } 

    public class QualificationSettings
    {
        /// <summary>
        /// Minimun net income needed for qualification
        /// </summary>
        public decimal MinimumNetIncome { get; set; }
        /// <summary>
        /// Max to lend as a percentage of net income
        /// </summary>
        public decimal NetIncomeThresold { get; set; }
        /// <summary>
        /// Max to lend on first loan
        /// </summary>
        public decimal MaxiumAmountOnFirstLoan { get; set; }

        /// <summary>
        /// Number of days an application is valid
        /// </summary>
        public int AppValidDays { get; set; }
    }

    public class QualificationResult
    {
        public int Id { get; set; }
        public decimal QualifyAmount { get; set; }
        public bool Qualified { get; set; }
        public string DeniedReason { get; set; }
    }
}
