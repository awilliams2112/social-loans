using DAL;
using HtmlAgilityPack;
using System;

namespace SocialLoans.Scrape
{
    public class RoutingNumbersScrape
    {

        const string BASE_URL = "";
        const string PAGES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        string csv = string.Empty;

        public RoutingNumbersScrape()
        {

        }

        public void Start()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc;

            foreach (char c in PAGES)
            {
                htmlDoc = web.Load(BASE_URL + "/" + c);

                var node = htmlDoc.DocumentNode.SelectNodes("//table");
            }

        }

        public string ResultCSV => csv;


    }
}
