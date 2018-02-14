using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class LogEntry 
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
