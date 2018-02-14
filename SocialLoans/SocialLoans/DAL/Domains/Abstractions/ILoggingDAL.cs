using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domains.Abstractions
{
    public interface ILoggingDAL
    {
        void InsertLog(LogEntry log);
        void InsertLog(string message);
    }
}
