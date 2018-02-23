using System;
using NUnit.Framework;
using SocialLoans.Logging;
using Moq;

namespace SocialLoans.Communications.Test
{
    [TestFixture]
    public class SmsTest
    {
        [Test]
        public void SendSmsTest()
        {
            NullLogger logger = new NullLogger();

            SmsSender sender = new SmsSender(logger);

            sender.Send("9547939679", "testing Message");
        }
    }
}
