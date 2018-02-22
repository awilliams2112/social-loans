﻿using DAL.Models;
using DAL.Repositories.Interfaces;
using SocialLoans.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public interface IAccountRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetFullyLoaded(string userId);
        void InsertPhoneCode(PhoneCode phonecode);
        PhoneCode GetPhoneCode(string email, string code);


    }

    public class AccountRespository : Repository<ApplicationUser>, IAccountRepository
    {
        ApplicationDbContext context;
        ILog log;
        
        public AccountRespository(ApplicationDbContext context, ILog log) : base(context)
        {
            this.context = context;
            this.log = log;
        }

        public ApplicationUser GetFullyLoaded(string userId)
        {
            throw new NotImplementedException();
        }

        public void InsertPhoneCode(PhoneCode phonecode)
        {
            context.PhoneCodes.Add(phonecode);
            context.SaveChanges();
        }

        public PhoneCode GetPhoneCode(string email, string code)
        {
            return context.PhoneCodes.FirstOrDefault(c => c.Email == email && c.Code == code);
        }

    }

}
