using System;
using NUnit.Framework;
using SocialLoans.Logging;
using Moq;
using DAL.Models;

//Had to set this property so that i can unit core standard projects
//https://stackoverflow.com/questions/44284170/pdb-format-is-not-supported-with-net-portable-debugging-information
//https://stackoverflow.com/questions/2155930/how-do-i-remedy-the-the-breakpoint-will-not-currently-be-hit-no-symbols-have-b

namespace SocialLoans.Communications.Test
{
    [TestFixture]
    public class CommunicationServiceTests
    {
        [Test]
        public void SendPhoneCodeSms()
        {
            NullLogger logger = new NullLogger();

            SmsSender sender = new SmsSender(logger);

            CommunicationService comm = new CommunicationService(sender);

            comm.SendPhoneCodeSms(new PhoneCode { Phone = "9547939679", Code = "12345"});
        }
    }
}
