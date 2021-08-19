using LoanCalculator.Input;
using LoanCalculator.Output;
using LoanCalculator.PaybackScheme;
using LoanCalculator.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using static System.FormattableString;

namespace LoanCalculator
{
    public class LoanCalculator
    {
        private readonly FixedInterestRateSettings fixedInterestRateSettings;
        private readonly ILogger<LoanCalculator> logger;

        public LoanCalculator(FixedInterestRateSettings fixedInterestRateSettings, ILogger<LoanCalculator> logger)
        {
            this.fixedInterestRateSettings = fixedInterestRateSettings;
            this.logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loanCalculationInput"></param>
        /// <returns></returns>
        public List<LoanCalculationOutput> Calculate(LoanCalculationInput loanCalculationInput)
        {
            this.ValidateInput(loanCalculationInput);

            int loanDurationInMonths = loanCalculationInput.LoanDurationInYears * 12;

            List<LoanCalculationOutput> loanCalculationOutputs = new List<LoanCalculationOutput>();

            for (int month = 1; month <= loanDurationInMonths; month++)
            {
                var paybackScheme = PaybackSchemeFactory.GetPaybackScheme(loanCalculationInput.LoanType, this.logger);

                if (!fixedInterestRateSettings.FixedInterestRates.TryGetValue(loanCalculationInput.LoanType, out var interestRate))
                {
                    throw new Exception(Invariant($"Interest rate for loan type {loanCalculationInput.LoanType} not found."));
                }

                (decimal amountTowardsPrincipal, decimal interestAmount) = paybackScheme.Calculate(month, loanCalculationInput.Amount, loanDurationInMonths, interestRate);

                loanCalculationOutputs.Add(new LoanCalculationOutput()
                {
                    InstallmentMonth = month,
                    AmountTowardsPrincipal = amountTowardsPrincipal,
                    InterestAmount = interestAmount
                });
            }

            return loanCalculationOutputs;
        }

        private void ValidateInput(LoanCalculationInput loanCalculationInput)
        {
            if(loanCalculationInput == null)
            {
                throw new ArgumentNullException(nameof(loanCalculationInput));
            }
            if (loanCalculationInput.Amount <= 0)
            {
                throw new ArgumentException("Loan amount should be greater than zero.");
            }
            if (loanCalculationInput.LoanDurationInYears <= 0)
            {
                throw new ArgumentException("Loan duration in years should be greater than zero.");
            }
            if (loanCalculationInput.LoanDurationInYears > 30)
            {
                throw new ArgumentException("Loan duration in years should not be greater than thirty.");
            }
        }
    }
}
