using LoanCalculator.Settings;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LoanCalculatorTests
{
    public class LoanCalculatorTest
    {
        private LoanCalculator.LoanCalculator loanCalculator;
        private Mock<ILogger<LoanCalculator.LoanCalculator>> logger;

        public LoanCalculatorTest()
        {
            this.logger = new Mock<ILogger<LoanCalculator.LoanCalculator>>();

            FixedInterestRateSettings settings = new FixedInterestRateSettings()
            {
                FixedInterestRates = new System.Collections.Generic.Dictionary<LoanCalculator.Models.LoanType, decimal>()
                {
                    { LoanCalculator.Models.LoanType.HomeLoan, 3.5m },
                }
            };
            this.loanCalculator = new LoanCalculator.LoanCalculator(settings, logger.Object);
        }

        [Fact]
        public void CalculateLoan_WhenInputIsValid_ReturnsCorrectAmount()
        {
            var output = this.loanCalculator.Calculate(new LoanCalculator.Input.LoanCalculationInput()
            {
                Amount = 1000,
                LoanDurationInYears = 5,
                LoanType = LoanCalculator.Models.LoanType.HomeLoan
            });
        }
    }
}
