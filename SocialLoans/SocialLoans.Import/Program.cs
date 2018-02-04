using DAL;
using DAL.Models;
using SocialLoans.Logging;
using System;
using System.Collections.Generic;

namespace SocialLoans.Importers
{
    class Program
    {

        static void Main(string[] args)
        {
            DesignTimeDbContextFactory dbFactory = new DesignTimeDbContextFactory();

            RoutingNumberImport import = WireUpRoutingNumberImport();

            import.Import();

            Console.ReadLine();
            
        }

        static RoutingNumberImport WireUpRoutingNumberImport()
        {
            DesignTimeDbContextFactory dbFactory = new DesignTimeDbContextFactory();
            ConsoleLogger log = new ConsoleLogger();

            RoutingNumberImport import = new RoutingNumberImport(
                                            new UnitOfWork(dbFactory.CreateDbContext(new string[] { }), log),
                                            log);
            

            return import;

        }
    }

    class ConsoleLogger : ILog
    {
        public void Debug(string message)
        {
            Console.WriteLine($"DEBUG: {message}");
        }

        public void Error(string message)
        {
            Console.WriteLine($"ERROR: {message}");
        }

        public void Fatal(string message)
        {
            Console.WriteLine($"FATAL: {message}");
        }

        public void Info(string message)
        {
            Console.WriteLine($"INFO: {message}");
        }
    }
}
