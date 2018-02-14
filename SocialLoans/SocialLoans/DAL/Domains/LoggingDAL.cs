using DAL.Domains.Abstractions;
using DAL.Models;

namespace DAL.Domains
{
    public class LoggingDAL : ILoggingDAL
    {
        ApplicationDbContext context;

        public LoggingDAL(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void InsertLog(LogEntry log)
        {
            context.Add(log);

            context.SaveChangesAsync().Start();
        }

        public void InsertLog(string message)
        {
            LogEntry log = new LogEntry();
            log.Message = message;

            InsertLog(log);
        }
    }
}
