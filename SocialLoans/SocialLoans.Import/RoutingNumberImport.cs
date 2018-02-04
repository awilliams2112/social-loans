using DAL;
using DAL.Core;
using DAL.Models;
using DAL.Repositories;
using SocialLoans.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialLoans.Importers
{
    public class RoutingNumberImport
    {
        IUnitOfWork unitOfWork;
        RoutingNumbersScrape scrape;
        ILog log;

        public RoutingNumberImport(IUnitOfWork unitOfWork, ILog log)
        {
            this.unitOfWork = unitOfWork;
            this.log = log;

            //TODO should independentaly inject
            scrape = new RoutingNumbersScrape(log);
        }

        public void Import()
        {
            log.Info("Starting Routing Numbers Import");

            Import imp = unitOfWork.Imports.GetLastestOfType((int)ImportTypes.RoutingNumbers);

            List<Import_RoutingNumber_DTO> dtoEntries = null;

            if(imp == null)
            {
                scrape.Start();

                imp = CreateImport(scrape.Entries);

                dtoEntries = scrape.Entries;
            }

            if (dtoEntries != null)
                CommitTo(dtoEntries);
            else if (imp != null)
                CommitTo(imp);
        }

        public Import CreateImport(List<Import_RoutingNumber_DTO> entries)
        {
            log.Info($"Creating Import for {entries.Count} entries");

            var transaction = unitOfWork.BeginTransaction();

            try
            {
                Import latestImport = unitOfWork.Imports.GetLastestOfType((int)ImportTypes.RoutingNumbers);

                if(latestImport != null)
                {
                    log.Info($"Set latest Import {latestImport.Id} to IsLatest = false");
                    latestImport.IsLatest = false;
                    unitOfWork.Imports.Update(latestImport);
                    unitOfWork.SaveChanges();
                }

                log.Info($"Creating Import for {entries.Count} entries...");

                Import import = new Import();
                import.Name = string.Format($"RoutingNumbers - {DateTime.Now.ToString()}");
                import.ImporTypeId = (int)ImportTypes.RoutingNumbers;
                import.IsLatest = true;

                unitOfWork.Imports.Add(import);
                unitOfWork.SaveChanges();

                log.Info($"Inserting {entries.Count} entries for import {import.Id}...");
                
                BulkInsertResult result = unitOfWork.Imports.RoutingNumberBulkInsert(import.Id, entries);

                import.InsertSql = result.InsertSQL;
                import.RollBackSql = result.RollbackSQL;

                unitOfWork.Imports.Update(import);
                unitOfWork.SaveChanges();

                transaction.Commit();

                log.Info($"Commit Transaction for Import {import.Id}");


                return import;
            }
            catch(Exception ex)
            {
                //TODO exception handling
                log.Error(ex.Message);
                log.Info("Fatal error occured attempting to rollback");
                transaction.Rollback();
            }

            return null;
        }

        public bool CommitTo(Import import)
        {
            List<Import_RoutingNumber_DTO> entries = unitOfWork.Imports.GetImportRoutingNumbersByImportId(import.Id);

            return CommitTo(entries);
            
        }

        public bool CommitTo(List<Import_RoutingNumber_DTO> entries)
        {
            var transaction = unitOfWork.BeginTransaction();

            try
            {
                List<RoutingNumber> routingNumbers = new List<RoutingNumber>();

                foreach (var e in entries)
                {
                    routingNumbers.Add(DataMassage.RoutingNumber(e));
                }

                log.Info($"Inserting {routingNumbers.Count} records into dbo.RoutingNumbers ");

                unitOfWork.Imports.RoutingNumberBulkInsert(routingNumbers);

                log.Info($"Committing...");

                transaction.Commit();
                
                log.Info($"Commit Complete ");


                return true;
            }
            catch (Exception ex)
            {
                //TODO exception handling
                log.Error(ex.Message);
                log.Info("Fatal error occured attempting to rollback");
                transaction.Rollback();
            }

            return false;
        }
    }
}
