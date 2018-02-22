using SocialLoans.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;

namespace SocialLoans.Communications
{
    public interface ISmsSender
    {
        //TODO make async
        void Send(string to, string message);
    }

    public class SmsSender : ISmsSender
    {
        ILog log;
        TwilioRestClient twilioClient;

        public SmsSender(ILog log)
        {
            this.log = log;
            this.twilioClient = new TwilioRestClient(
                Credentials.TwilioAccountSid,
                Credentials.TwilioAuthToken
            );
        }

        public void Send(string to, string message)
        {
            log.Info($"Sending Sms to {to} message: {message}");

            if (string.IsNullOrEmpty(to))
            {
                log.Error($"Sending Sms Fail: to cannot be empty");
                return;

            }

            if (string.IsNullOrEmpty(message))
            {
                log.Error($"Sending Sms Fail: message cannot be empty");
                return;
            }

            
            CreateMessageOptions opts = new CreateMessageOptions(new Twilio.Types.PhoneNumber(to));
  
            var messageResource = MessageResource.CreateAsync(
                to: new Twilio.Types.PhoneNumber(to),
                from: new Twilio.Types.PhoneNumber(Settings.SmsPhoneNumber),
                body: message,
                //mediaUrl: mediaUrl,
                client: this.twilioClient).Result;

            if(!string.IsNullOrEmpty(messageResource.ErrorMessage))
            {
                log.Error($"Error sending Sms to: {to} message: {message}: error:{messageResource.ErrorMessage}");
            }
        }

        
    }

    public class Settings
    {
        public static string SmsPhoneNumber = "3853233115";
        
    }

    public class Credentials
    {
        public static string TwilioAccountSid = "AC026d0ff3171d01e997d03ea2ab9b2e81";
        public static string TwilioAuthToken = "78a2642108956d7e31a8d002b9821783";

    }
}
