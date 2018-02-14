using DAL;
using DAL.Core.Interfaces;
using DAL.Domains;
using DAL.Domains.Abstractions;
using DAL.Models;
using DAL.New;
using SocialLoans.PaymentApi.Stripe;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialLoans.PaymentApi
{
    public interface IBankService
    {
        void AddBankAccount(BankAccount bankAccount);
        List<BankAccount> GetBankAccounts(string currentUserId);
        BankAccountVerifyResult VerifyAccount(BankAccountVerification bv);
    }

    public class BankService : IBankService
    {
        IDataDomains dataLayer;
        IAccountManager accountManager;
        IStripeService stripeService;
        IPaymentsDAL paymentRepo;

        public BankService(IDataDomains dataLayer, IAccountManager accountManager, IStripeService stripeService)
        {
            this.paymentRepo = dataLayer.PaymentDomainDL;
            this.accountManager = accountManager;
            this.stripeService = stripeService;
        }

        public void AddBankAccount(BankAccount bankAccount)
        {
            paymentRepo.InsertBankAccount(bankAccount);
        }

        public List<BankAccount> GetBankAccounts(string currentUserId)
        {
            return null;
        }

        public void UpdateBankAccount(BankAccount bankAccount)
        {

        }

        public BankAccountVerifyResult VerifyAccount(BankAccountVerification bv)
        {

            BankAccount acct = paymentRepo.GetBankAccount(bv.BankAccountId);

            ApplicationUser user = accountManager.GetUserByIdAsync(acct.UserId).Result;

            BankAccountVerifyResult result = new BankAccountVerifyResult();

            var options = new BankAccountVerifyOptions
            {
                AmountOne = bv.Deposit1,
                AmountTwo = bv.Deposit2,
            };

            var service = new BankAccountService();
            CustomerBankAccount bankAccount = service.Verify(
              user.StripeIdCustomer,
              acct.StripeIdBankAccount,
              options
            );


            result.IsVerified = bankAccount.Status == StripeStatuses.verified ? true : false;

            return result;
        }
    }
}

public class StripeStatuses
{
    public static string verified = "verified";
}
public class BankAccountVerifyResult
{
    public bool IsVerified { get; set; }
}
