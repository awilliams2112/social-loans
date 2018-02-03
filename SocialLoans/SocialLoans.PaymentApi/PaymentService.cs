using DAL.Core.Interfaces;
using DAL.Models;
using System;
using Stripe.net;
using Stripe;
using Stripe.Infrastructure;

namespace SocialLoans.PaymentApi
{

    public interface IPaymentService { }

    public class PaymentService : IPaymentService
    {
        IAccountManager _accountManager;

        public PaymentService(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public void Payout(PayoutRequest payoutRequest)
        {
            if(string.IsNullOrEmpty(payoutRequest.ToUserId))
            {
                throw new Exception("Error payoutrequest.touserId is required");
            }
            
            if (payoutRequest.ToUser == null)
                payoutRequest.ToUser = _accountManager.GetUserByIdAsync(payoutRequest.ToUserId).Result;

            if (payoutRequest.ToUser == null)
                throw new Exception($"Error user cannot be found id: {payoutRequest.ToUserId}");


            //StripeCustomer cust;

            
            //payoutRequest.ToUser.StripeIdCustomer;

            //Stripe.IStripeService.
            //Stripe.BankAccountService f;
            
            //Stripe.
        }

        public void Charge() { }
    }

    public class PayoutRequest 
    {
        public string ToUserId { get; set; }

        public ApplicationUser ToUser { get; set; }

        public decimal Amount { get; set; }
        public string TriggerEventId { get; set; }
    }

    public class ChargeRequest
    {
        public string FromUserId { get; set; }
        public decimal Amount { get; set; }
        public string TriggerEventId { get; set; }
    }

    //public class BankService
    //{
    //    public void AddBank
    //    {

    //    }

    //    public void updateBank
    //    {

    //    }
    //}

}
