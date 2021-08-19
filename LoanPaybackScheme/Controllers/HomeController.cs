using LoanCalculator;
using LoanCalculator.Input;
using LoanCalculator.Models;
using LoanCalculator.Output;
using LoanCalculator.Settings;
using LoanPaybackScheme.Models;
using LoanPaybackScheme.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LoanPaybackScheme.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILogger<LoanCalculator.LoanCalculator> loanLogger;
        public IConfiguration Configuration { get; }
        public HomeController(ILogger<HomeController> logger, ILogger<LoanCalculator.LoanCalculator> logs, IConfiguration configuration)
        {
            _logger = logger;
            loanLogger = logs;
            Configuration = configuration; 
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CalculatePaybackPlan(LoanCalculationInput inputs)
        {
            var interestRateSettings = new FixedInterestRateSettings();
            var interestRate = Configuration["FixedInterestRateSettings:" + inputs.LoanType.ToString()];

            var dict = new Dictionary<LoanType, decimal>();
            dict.Add(inputs.LoanType, Convert.ToDecimal(interestRate));
            interestRateSettings.FixedInterestRates = dict;

            var loanCalculator = new LoanCalculator.LoanCalculator(interestRateSettings, loanLogger);
            var paymentPaybackPlan = loanCalculator.Calculate(inputs);
            
            return View(paymentPaybackPlan);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
