using DAL.Models;
using System;

namespace SocialLoans.Communications
{
    public interface ICommunicationService
    {
        void SendWelcomeEmail(ApplicationUser user);
    }

    public class CommunicationService : ICommunicationService
    {
        public CommunicationService()
        {

        }

        public void SendWelcomeEmail(ApplicationUser user)
        {
            
            throw new NotImplementedException();
        }
    }
}
