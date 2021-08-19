namespace LoanCalculator.PaybackScheme
{
    public interface IPaybackScheme
    {
        (decimal amountTowardsPrincipal, decimal interestAmount) Calculate(int month, decimal amount, int loanDurationInMonths, decimal interestRate);
    }
}
