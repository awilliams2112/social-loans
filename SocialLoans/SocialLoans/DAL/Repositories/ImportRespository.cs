using DAL.Models;
using DAL.Repositories.Interfaces;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SocialLoans.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Core;
using System.Linq;

namespace DAL.Repositories
{
    public interface IImportRepository : IRepository<Import>
    {
        BulkInsertResult RoutingNumberBulkInsert(int id, List<Import_RoutingNumber_DTO> entries);
        void RoutingNumberBulkInsert(List<RoutingNumber> routingNumbers);
        Import GetLastestOfType(int importTypeId);
        List<Import_RoutingNumber_DTO> GetImportRoutingNumbersByImportId(int id);
    }

    public class ImportRespository : Repository<Import>, IImportRepository
    {
        ApplicationDbContext context;
        ILogger log;

        public class TableNames
        {
            public static string ImportRoutingNumbers = "[SocialLoans].[dbo].[Import_RoutingNumbers]";
            public static string RoutingNumbers = "[SocialLoans].[dbo].[RoutingNumbers]";
        }

        public ImportRespository(ApplicationDbContext context, ILogger log): base(context)
        {
            this.context = context;
            this.log = log;
        }

        public BulkInsertResult _RoutingNumberBulkInsert(int importId, List<Import_RoutingNumber_DTO> entries)
        {
            StringBuilder strBldr = new StringBuilder();
            
            strBldr.AppendLine(@"INSERT INTO dbo.Import_RoutingNumbers
                (AchServicesTelephone, 
                    Address, 
                    BankName,
                    City,
                    DateOfLastRevision, 
                    ImportId, 
                    NewRoutingNumbers, 
                    RoutingNumbers, 
                    Zip) 
                VALUES");

            int entryCount = entries.Count;
            string comma;

            for (int i = 0; i < entryCount; i++)
            {
                var entry = entries[i];

                if (i == (entryCount - 1))
                    comma = "";
                else
                    comma = ",";

                strBldr.AppendLine($"(\"{entry.AchServicesTelephone}\", \"{entry.Address}\", \"{entry.BankName}\", \"{entry.City}\", \"{entry.DateOfLastRevision}\", {importId}, \"{entry.NewRoutingNumbers}\", \"{entry.RoutingNumbers}\", \"{entry.Zip}\"){comma}");
            }
            
            var sqlString = strBldr.ToString();
            RawSqlString sql = new RawSqlString(sqlString);

            log.Info("Executing Bulk Insert command");

            context.Database.ExecuteSqlCommand(sql);
            
            return new BulkInsertResult
            {
                InsertSQL = sql.Format,
                RollbackSQL = ""
            };
        }

        public BulkInsertResult RoutingNumberBulkInsert(int importId, List<Import_RoutingNumber_DTO> entries)
        {
            entries.ForEach(e => e.ImportId = importId);

            log.Info($"Truncate Table {TableNames.ImportRoutingNumbers}");

            context.Database.ExecuteSqlCommand(new RawSqlString($"TRUNCATE Table {TableNames.ImportRoutingNumbers}"));

            log.Info($"Bulk Insert ({entries.Count}) into {TableNames.ImportRoutingNumbers}");

            context.BulkInsert(entries);

            return new BulkInsertResult
            {

            };
        }

        public void RoutingNumberBulkInsert(List<RoutingNumber> routingNumbers)
        {
            log.Info($"Truncate Table {TableNames.RoutingNumbers}");

            context.Database.ExecuteSqlCommand(new RawSqlString($"TRUNCATE Table {TableNames.RoutingNumbers}"));

            log.Info($"Bulk Insert ({routingNumbers.Count}) into {TableNames.RoutingNumbers}");

            context.BulkInsert(routingNumbers);
        }

        public Import GetLastestOfType(int importTypeId)
        {
            return context.Imports.FirstOrDefault(i => i.ImporTypeId == importTypeId && i.IsLatest);
        }

        public List<Import_RoutingNumber_DTO> GetImportRoutingNumbersByImportId(int importId)
        {
            return context.Import_RoutingNumbers.Where(i => i.ImportId == importId).ToList();
        }
    }

    public class BulkInsertResult
    {
        public string Success { get; set; }
        public string InsertSQL { get; set; }
        public string RollbackSQL { get; set; }
    }
}
