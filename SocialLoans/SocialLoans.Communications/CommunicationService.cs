using DAL.Models;
using SocialLoan.Utilities;
using System;
using System.Collections.Generic;
using SocialLoans.Domain;

namespace SocialLoans.Communications
{
    public interface ICommunicationService
    {
        void SendWelcomeEmail(ApplicationUser user);
        void SendPhoneCodeSms(PhoneCode phoneCode);
    }

    public class CommunicationService : ICommunicationService
    {
        ISmsSender smsSender;
        IEmailSender emailSender;

        public CommunicationService(ISmsSender smsSender)
        {
            this.smsSender = smsSender;
        }

        public void SendPhoneCodeSms(PhoneCode phoneCode)
        {
            if(string.IsNullOrEmpty(phoneCode.Code))
            {
                //TODO exception
                throw new Exception("Phone Code cannot be empty");
            }

            Dictionary<string, string> tokens = new Dictionary<string, string>();
            tokens.Add("code", phoneCode.Code);
            
            string template = "Social Loans: Confirmation code is {{code}}. This code will expire in 10 mins. Please don't reply to this message";
            string message = Blender.Blend(template, tokens);
            
            this.smsSender.Send(phoneCode.Phone, message);
        }

        public void SendWelcomeEmail(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
