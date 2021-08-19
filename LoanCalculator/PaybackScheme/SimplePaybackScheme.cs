using Microsoft.Extensions.Logging;

namespace LoanCalculator.PaybackScheme
{
    public class SimplePaybackScheme : IPaybackScheme
    {
        private readonly ILogger logger;

        public SimplePaybackScheme(ILogger logger)
        {
            this.logger = logger;
        }

        public (decimal amountTowardsPrincipal, decimal interestAmount) Calculate(int month, decimal amount, int loanDurationInMonths, decimal interestRate)
        {
            decimal amountToBePaidPerMonth = amount / loanDurationInMonths;

            decimal amountAlreadyPaid = (amount * (month - 1) / loanDurationInMonths);

            //this.logger.LogInformation("{amountAlreadyPaid}", amountAlreadyPaid);

            decimal remainingAmount = amount - amountAlreadyPaid;

            decimal interestToPayPerMonth = (remainingAmount * interestRate) / (100 * 12);

            //this.logger.LogInformation("{month}, {amountToBePaidPerMonth}, {interestToPayPerMonth}", month, amountToBePaidPerMonth, interestToPayPerMonth);

            return (amountToBePaidPerMonth, interestToPayPerMonth);
        }
    }
}
