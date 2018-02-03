using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Import : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ImporTypeId { get; set; }

        public ImportType ImportType { get; set; }

        public bool IsLatest { get; set; }

        public string InsertSql { get; set; }

        public string RollBackSql { get; set; }
    }

    public class ImportType : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
