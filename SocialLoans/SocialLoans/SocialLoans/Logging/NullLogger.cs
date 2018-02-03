// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

namespace SocialLoans.Logging
{
    public class NullLogger : ILogger
    {
        public void Debug(string message)
        {
            //Do Nothing
        }

        public void Error(string message)
        {
            //Do Nothing
        }

        public void Fatal(string message)
        {
            //Do Nothing
        }

        public void Info(string message)
        {
            //Do Nothing
        }
    }
}