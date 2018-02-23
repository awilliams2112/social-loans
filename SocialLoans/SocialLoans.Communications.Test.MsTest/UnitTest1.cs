using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialLoans.Logging;
using DAL.Models;

//Had to set this property so that i can unit core standard projects
//https://stackoverflow.com/questions/44284170/pdb-format-is-not-supported-with-net-portable-debugging-information
//https://stackoverflow.com/questions/2155930/how-do-i-remedy-the-the-breakpoint-will-not-currently-be-hit-no-symbols-have-b

namespace SocialLoans.Communications.Test.MsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            NullSsmsSender sender = new NullSsmsSender();

            CommunicationService comm = new CommunicationService(sender);

            comm.SendPhoneCodeSms(new PhoneCode { Phone = "9547939679", Code = "12345" });
            
        }
    }

    public class NullSsmsSender : ISmsSender
    {
        public void Send(string to, string message)
        {
            To = to;
            Message = message;
        }

        public string To { get; set; }
        public string Message { get; set; }
    }
}
