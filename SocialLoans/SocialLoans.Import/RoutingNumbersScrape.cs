using DAL.Models;
using HtmlAgilityPack;
using SocialLoans.Logging;
using SocialLoans.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialLoans.Importers
{
    public class RoutingNumbersScrape
    {
        //example http://www.gregthatcher.com/Bank/Routing/Numbers/A

        const string BASE_URL = "http://www.gregthatcher.com/Bank/Routing/Numbers";
        const string PAGES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        string csv = string.Empty;
        string sql = string.Empty;

        List<Import_RoutingNumber_DTO> entries = new List<Import_RoutingNumber_DTO>();

        ILogger log;

        public RoutingNumbersScrape(ILogger log)
        {
            this.log = log;
        }

        public void Start()
        {
            entries.Clear();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc;

            foreach (char c in PAGES)
            {
                var urlToScrape = $"{BASE_URL}/{c}";

                log.Info($"Scraping Page {urlToScrape}");

                htmlDoc = web.Load(urlToScrape);
                
                var tablerows = htmlDoc.DocumentNode.SelectNodes("//table/tr");

                if(tablerows == null)
                {
                    log.Info($"No table found at {urlToScrape}");

                    continue;
                }
                
                var tablerowsCount = tablerows.Count;

                log.Info($"{tablerowsCount} table rows found");


                for (int i = 0; i < tablerowsCount;i++)
                {
                    Import_RoutingNumber_DTO imp = new Import_RoutingNumber_DTO();

                    var row = tablerows[i];

                    imp.AchServicesTelephone = row.ChildNodes[WebColumnIndex.AchServicesTelephone].InnerText;
                    imp.Address = row.ChildNodes[WebColumnIndex.Address].InnerText;
                    imp.BankName = row.ChildNodes[WebColumnIndex.BankName].InnerText;
                    imp.City = row.ChildNodes[WebColumnIndex.City].InnerText;
                    imp.DateOfLastRevision = row.ChildNodes[WebColumnIndex.DateOfLastRevision].InnerText;
                    imp.NewRoutingNumbers = row.ChildNodes[WebColumnIndex.NewRoutingNumbers].InnerText;
                    imp.RoutingNumbers = row.ChildNodes[WebColumnIndex.RoutingNumbers].InnerText;
                    imp.Zip = row.ChildNodes[WebColumnIndex.Zip].InnerText;
                    imp.State = row.ChildNodes[WebColumnIndex.State].InnerText;

                    if (imp.RoutingNumbers.IsNumberic())
                    {
                        //log.Info($"Valid Row [{imp.RoutingNumbers}]");

                        entries.Add(imp);
                    }
                    else
                    {
                        //log.Error($"Invalid Row [{imp.RoutingNumbers}]");
                    }
                }

                
            }

        }

        public string BaseUrl => BASE_URL;

        public string ResultCSV => csv;

        public string ResultSQL => sql;

        public List<Import_RoutingNumber_DTO> Entries => entries;

        public static class WebColumnIndex
        {
            public static int RoutingNumbers = 1;
            public static int NewRoutingNumbers = 2;
            public static int BankName = 3;
            public static int Address = 4;
            public static int City = 5;
            public static int State = 6;
            public static int Zip = 7;
            public static int AchServicesTelephone = 8;
            public static int DateOfLastRevision = 9;
        }

        public static class ColumnNames
        {
            public static string RoutingNumbers = "RoutingNumbers";
            public static string NewRoutingNumbers = "NewRoutingNumbers";
            public static string BankName = "BankName";
            public static string Address = "Address";
            public static string city = "city";
            public static string Zip = "Zip";
            public static string AchServicesTelephone = "AchServicesTelephone";
            public static string DateOfLastRevision = "DateOfLastRevision";
        }

    }

}

