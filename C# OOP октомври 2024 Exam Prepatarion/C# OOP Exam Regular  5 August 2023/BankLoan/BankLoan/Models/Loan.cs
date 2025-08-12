using BankLoan.Models.Contracts;

namespace BankLoan.Models;

public abstract class Loan:ILoan
{
    private int interestRate;
    private double amount;


    public Loan(int interestRate, double amount)
    {
        this.InterestRate = interestRate;
        this.Amount = amount;
    }


    public int InterestRate
    {
        get
        {
            return interestRate;
        }
        private set
        {
            interestRate = value;
        }
    }
    public double Amount
    {
        get
        {
            return amount;
        }
        private set
        {
            amount = value;
        }
    }
}