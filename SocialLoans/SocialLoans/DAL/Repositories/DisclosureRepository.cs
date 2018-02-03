using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;

namespace DAL.Repositories
{
    public interface IDisclosureRepository : IRepository<Disclosure>
    {
        DisclosureTemplate GetTemplateByTypeId(int disclosureTypeId);
    }

    public class DisclosureRepository : Repository<Disclosure>, IDisclosureRepository
    {
        ApplicationDbContext context;

        public DisclosureRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public DisclosureTemplate GetTemplateByTypeId(int disclosureTypeId)
        {
            return this.context.DisclosureTemplates.FirstOrDefault(t => t.DisclosureTemplateTypeId == disclosureTypeId && t.IsCurrent);
        }

    }
}
