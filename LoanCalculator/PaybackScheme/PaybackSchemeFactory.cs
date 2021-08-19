using LoanCalculator.Models;
using Microsoft.Extensions.Logging;

namespace LoanCalculator.PaybackScheme
{
    public class PaybackSchemeFactory
    {
        public static IPaybackScheme GetPaybackScheme(LoanType loanType, ILogger logger)
        {
            return new SimplePaybackScheme(logger);
        }
    }
}
