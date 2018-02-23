using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialLoans.PaymentApi;

namespace SocialLoans.Controllers
{
    public class CommonController : Controller
    {
        IBankService bankService;

        public CommonController(IBankService bankService)
        {
            this.bankService = bankService;
        }

        [HttpGet("~/api/common/bankNames")]
        public IActionResult GetBankName(string routingNumber)
        {
            string bankName = this.bankService.GetBankName(routingNumber);

            if(string.IsNullOrEmpty(bankName))
            {
                return Ok();
            }

            return Ok(new
                {
                    success =  true,
                    bankname = bankName
                });
        }
    }
}