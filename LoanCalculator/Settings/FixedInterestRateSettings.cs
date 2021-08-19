using LoanCalculator.Models;
using System.Collections.Generic;

namespace LoanCalculator.Settings
{
    public class FixedInterestRateSettings
    {
        public Dictionary<LoanType, decimal> FixedInterestRates { get; set; }
    }
}
