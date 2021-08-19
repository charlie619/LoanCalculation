namespace LoanCalculator.Output
{
    public class LoanCalculationOutput
    {
        /// <summary>
        /// Gets or sets the installment month.
        /// </summary>
        public int InstallmentMonth { get; set; }

        public decimal AmountTowardsPrincipal { get; set; }

        public decimal InterestAmount { get; set; }

        public decimal TotalAmount => AmountTowardsPrincipal + InterestAmount;
    }
}
