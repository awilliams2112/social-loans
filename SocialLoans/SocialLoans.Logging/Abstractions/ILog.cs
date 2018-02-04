using System;

namespace SocialLoans.Logging
{
    public interface ILog
    {
        void Info(string message);
        void Debug(string message);
        void Error(string message);
        void Fatal(string message);
    }
}
