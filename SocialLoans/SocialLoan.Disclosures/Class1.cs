using DAL;
using DAL.Core;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialLoan.Disclosures
{
    public interface IDisclosureService
    {
        Disclosure CreateDisclosure(int applicationUserId, DisclosureTypes disclosureType, Dictionary<string, string> valuePairs);
    }

    public class DisclosureService : IDisclosureService
    {
        IUnitOfWork unitOfWork;

        public DisclosureService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        public Disclosure CreateDisclosure(int applicationUserId, DisclosureTypes disclosureType, Dictionary<string, string> valuePairs)
        {
            DisclosureTemplate template = unitOfWork.Disclosures.GetTemplateByTypeId((int)disclosureType);

            Disclosure disclosure = new Disclosure();
            disclosure.Body = Blender.Blend(template.Body, valuePairs);

            unitOfWork.Disclosures.Add(disclosure);
            unitOfWork.SaveChanges();

            return disclosure;
        }
    }
    
    public class Blender
    {
        public static string Blend(string template, Dictionary<string, string> tokens)
        {
            StringBuilder strBlr = new StringBuilder(template);

            string output = strBlr.ToString();

            foreach (var val in tokens)
            {
                output.ToString().Replace("{{" + val.Key + " }}", val.Value);
            }

            return output;
        }
    }
}
