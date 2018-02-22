using DAL;
using DAL.Models;
using SocialLoans.Communications;
using SocialLoans.Logging;
using System;

namespace SocialLoans.Domain
{
    public interface ISocialLoansAuthentication
    {
        void SendPhoneConfirmationCode(ApplicationUser user, string phoneNumber);
        PhoneConfirmResult PhoneConfirm(string email, string code);
    }

    public class SocialLoansAuthentication : ISocialLoansAuthentication
    {
        ILog log;
        IUnitOfWork dataApi; //call this ISocialLoansDataApi
        ICommunicationService commSerivice;

        public SocialLoansAuthentication(ILog log, IUnitOfWork unitOfWork, ISmsSender SmsSender)
        {
            this.log = log;
            this.dataApi = unitOfWork;
        }
        
        public PhoneConfirmResult PhoneConfirm(string email, string code)
        {
            PhoneConfirmResult result = new PhoneConfirmResult();
            PhoneCode pCode = dataApi.Accounts.GetPhoneCode(email, code);

            if(pCode == null)
            {
                return new PhoneConfirmResult { InvalidCode = true };
            }

            if(pCode.Expires > DateTime.Now)
            {
                return new PhoneConfirmResult { IsExpired = true };
            }


            return new PhoneConfirmResult { IsSucess = true };
        }

        public void SendPhoneConfirmationCode(ApplicationUser user, string phoneNumber)
        {
            log.Info($"Sending Confirmation code to user {user.Email} at {phoneNumber} {nameof(SocialLoansAuthentication.SendPhoneConfirmationCode)}");
            
            PhoneCode phoneCode = new PhoneCode()
            {
                Code = CodeGenerators.New(5),
                Expires = DateTime.Now.Add(TimeSpan.FromMinutes(10)),
                Email = user.Email,
                Phone = phoneNumber
            };
        
            dataApi.Accounts.InsertPhoneCode(phoneCode);

            commSerivice.SendPhoneCodeSms(phoneCode);

        }

}

}

