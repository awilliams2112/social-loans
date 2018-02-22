using SocialLoans.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialLoans.Domain
{
    public interface IEmailSender
    {
        //make async
        void Send(string to, string from, string subject, string body, string attachments = null);
    }

    public class EmailSender : IEmailSender
    {
        ILog log;

        public EmailSender(ILog log)
        {
            
        }

        public void Send(string to, string from, string subject, string body, string attachments = null)
        {
            log.Info($"Sending email to {to} subject: {subject}");
            
        }
    }

}
