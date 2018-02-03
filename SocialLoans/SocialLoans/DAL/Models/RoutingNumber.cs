using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class RoutingNumber
    {
        public int Id { get; set; }
        public string AbaNumber { get; set; }
        public string BankName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime DateOfLastRevision { get; set; }
        public string Zip { get; set; }
        public string BankPhone { get; set; }
}
}
