using DAL;
using DAL.Core;
using DAL.Models;
using DAL.Repositories;
using SocialLoan.Utilities;
using System;
using System.Collections.Generic;

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
}
