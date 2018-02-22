using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class PhoneCode : AuditableEntity
    {
        public int Id { get; set; }
        public string Email { get; set;}
        public string Phone { get; set; }
        public string Code { get; set; }
        public DateTime Expires { get; set; }
    }
}
