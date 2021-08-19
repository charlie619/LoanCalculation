using LoanCalculator.Models;

namespace LoanCalculator.Input
{
    public class LoanCalculationInput
    {
        public decimal Amount { get; set; }

        public LoanType LoanType { get; set; }

        public int LoanDurationInYears { get; set; }
    }
}
