using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Import_RoutingNumber_DTO
    {
        public int Id { get; set; }
        public string AchServicesTelephone { get; set; }
        public string Address { get; set; }
        public string BankName { get; set; }
        public string City { get; set; }
        public string DateOfLastRevision { get; set; }
        public string NewRoutingNumbers { get; set; }
        public string RoutingNumbers { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }

        public int ImportId { get; set; }
        public Import Import { get; set; }
    }
}
