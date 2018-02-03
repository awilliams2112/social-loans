using System;
using System.Collections.Generic;
using System.Text;

namespace SocialLoans.PaymentApi.Stripe
{
    public interface IStripeService
    {
    }

    public class StripeService : IStripeService
    {
        public void PayOut() { }
    }
}
