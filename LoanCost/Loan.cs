using System;
using System.Collections.Generic;
using System.Text;

namespace LoanCost
{
    public abstract class Loan
    {
        protected double InterestRate;
        public virtual Dictionary<string,double> calculateLoanPaymentPlan(double principalAmt, int years)
        {
            var paymentPlan = new Dictionary<string, double>();
            var eachMonthAmount = (principalAmt / (years * 12));
            double monthlyInterest;
            var dateTime = DateTime.Now;

            for (int i = 1; i <= years * 12; i++)
            {
                monthlyInterest = (principalAmt * InterestRate) / (100 * 12);
                principalAmt -= eachMonthAmount;

                var month = dateTime.ToString("MM/yyyy");  

                paymentPlan.Add(month, Math.Round(eachMonthAmount + monthlyInterest, 2));

                dateTime = dateTime.AddMonths(1);
            }
            return paymentPlan;
        }
    }
}
