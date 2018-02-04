using DAL.Models;
using DAL.Repositories.Interfaces;
using SocialLoans.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public interface INullRepository : IRepository<ApplicationUser>
    {

    }

    public class INullRespository : Repository<ApplicationUser>, INullRepository
    {
        ApplicationDbContext context;
        ILog log;

        public INullRespository(ApplicationDbContext context, ILog log) : base(context)
        {
            this.context = context;
            this.log = log;
        }

    }
}
