using LoanCalculator.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace LoanCost
{
    class Program
    {      

        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            LoanCalculator.LoanCalculator loanCalculator = serviceProvider.GetService<LoanCalculator.LoanCalculator>();

            //var loanCalculator = new LoanCalculator.LoanCalculator(settings, this.loggerFactory.CreateLogger<LoanCalculator.LoanCalculator>());
            // Method2(loanCalculator);

            Method1();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                .AddTransient<LoanCalculator.LoanCalculator>()
                .AddSingleton(
                    new FixedInterestRateSettings()
                    {
                        FixedInterestRates = new System.Collections.Generic.Dictionary<LoanCalculator.Models.LoanType, decimal>()
                        {
                            { LoanCalculator.Models.LoanType.HomeLoan, 3.5m },
                        }
                    });
        }

        private static void Method2(LoanCalculator.LoanCalculator loanCalculator)
        {            
            var ans = loanCalculator.Calculate(new LoanCalculator.Input.LoanCalculationInput()
            {
                Amount = 30000,
                LoanDurationInYears = 5,
                LoanType = LoanCalculator.Models.LoanType.HomeLoan
            });

            decimal totalPaid = 0;

            foreach (var item in ans)
            {
                totalPaid += item.TotalAmount;
                Console.WriteLine("Date: " + item.InstallmentMonth + " EMI: " + item.TotalAmount);
            }

            Console.WriteLine(totalPaid);
        }

        private static void Method1()
        {
            var loan = new HomeLoan();
            var ans = loan.calculateLoanPaymentPlan(30000, 5);

            decimal totalPaid = 0;
            foreach (var item in ans)
            {
                totalPaid += new decimal(item.Value);
                //Console.WriteLine("Date: " + item.Key + " EMI: " + item.Value);
            }
            Console.WriteLine(totalPaid);
        }
    }
}
