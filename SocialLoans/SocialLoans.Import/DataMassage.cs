using DAL.Models;
using SocialLoans.Util;
using System;

namespace SocialLoans.Importers
{
    public class DataMassage
    {
        public static RoutingNumber RoutingNumber(Import_RoutingNumber_DTO dto)
        {
            RoutingNumber routingNumber = new RoutingNumber();

            routingNumber.AbaNumber = dto.RoutingNumbers.RemoveSpecailCharacters().RemoveWhiteSpaceCharacters().Trim();
            routingNumber.BankName = dto.BankName.ReplaceHtmlEscapeTags().Trim();
            routingNumber.Address = dto.Address.ReplaceHtmlEscapeTags().Trim();
            routingNumber.City = dto.City.ReplaceHtmlEscapeTags().Trim();
            routingNumber.State = dto.State;
            string dateStr = dto.DateOfLastRevision.Insert(2, "/").Insert(5, "/");
            
            routingNumber.DateOfLastRevision = Convert.ToDateTime(dateStr);
            routingNumber.Zip = dto.Zip.Substring(0,5);
            routingNumber.BankPhone = dto.AchServicesTelephone.RemoveSpecailCharacters().RemoveWhiteSpaceCharacters().Trim();


            return routingNumber;
        }
        
    }
}
