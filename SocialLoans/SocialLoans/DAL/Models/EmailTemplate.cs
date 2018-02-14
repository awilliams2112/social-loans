using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class EmailTemplate : AuditableEntity
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Version { get; set; }

        public bool IsLatest { get; set; }
    }
}
