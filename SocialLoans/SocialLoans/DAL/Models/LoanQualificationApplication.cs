using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class LoanQualificationApplication : AuditableEntity
    {
        /// <summary>
        /// Id of the application
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Is owner of their home
        /// </summary>
        public bool IsHomeOwn { get; set; }

        /// <summary>
        /// mMrtage + taxes + insrance or rent parking fees
        /// </summary>
        public decimal ExpenseHome { get; set; }

        /// <summary>
        /// cellphone bill
        /// </summary>
        public decimal ExpenseCellPhone { get; set; }

        /// <summary>
        /// water, gas, eletric
        /// </summary>
        public decimal ExpenseUtilties { get; set; }

        /// <summary>
        /// car payment 
        /// </summary>
        public decimal ExpenseCar { get; set; }

        /// <summary>
        /// car year
        /// </summary>
        public string CarYear { get; set; }

        /// <summary>
        /// car insurance
        /// </summary>
        public decimal ExpenseCarInsurance { get; set; }

        
        public string ExpenseOther1Name { get; set; }
        public decimal ExpenseOther1Amount { get; set; }

        public string ExpenseOther2Name { get; set; }
        public decimal ExpenseOther2Amount { get; set; }

        public string ExpenseOther3Name { get; set; }
        public decimal ExpenseOther3Amount { get; set; }

        public decimal IncomeJob { get; set; }
        public string Jobtitle { get; set; }
        public DateTime JobStartdate { get; set; }
        public bool IsSelfEmployed { get; set; }

        public decimal IncomeOther1 { get; set; }
        public string IncomeOther1Name { get; set; }
        public DateTime IncomeOther1Startdate { get; set; }
        public bool IncomeOther1IsSelfEmployed { get; set; }

        public decimal IncomeOther2 { get; set; }
        public string IncomeOther2Name { get; set; }
        public DateTime IncomeOther2Startdate { get; set; }
        public bool IncomeOther2IsSelfEmployed { get; set; }

        public decimal NetIncome
        {
            get
            {
                var totalExp = (ExpenseCar 
                    + ExpenseCarInsurance 
                    + ExpenseCellPhone 
                    + ExpenseHome 
                    + ExpenseOther1Amount
                    + ExpenseOther2Amount 
                    + ExpenseOther3Amount); 

                var totalinc = (IncomeJob + IncomeOther1 + IncomeOther2); 

                return totalinc - totalExp;
            }
        }

        public string Zipcode { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
