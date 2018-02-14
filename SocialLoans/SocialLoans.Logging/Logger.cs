using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using DebugConsole = System.Diagnostics.Debug;
using System.Text;


namespace SocialLoans.Logging
{
    public class Logger : ILog
    {
        public Logger()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();

            builder.UseSqlServer("");
            DbContext context = new DbContext(builder.Options);
        }

        public void Debug(string message)
        {
            DebugConsole.WriteLine($"{DateTime.Now.ToString()} DEBUG: {message}");
        }

        public void Error(string message)
        {
            DebugConsole.WriteLine($"{DateTime.Now.ToString()} ERROR: {message}");


        }

        public void Fatal(string message)
        {
            DebugConsole.WriteLine($"{DateTime.Now.ToString()} FATAL: {message}");


        }

        public void Info(string message)
        {
            DebugConsole.WriteLine($"{DateTime.Now.ToString()} INFO: {message}");
        }
    }
}
