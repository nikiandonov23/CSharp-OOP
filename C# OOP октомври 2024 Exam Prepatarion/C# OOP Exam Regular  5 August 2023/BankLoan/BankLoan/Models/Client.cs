using System;
using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;

namespace BankLoan.Models;

public abstract class Client:IClient
{
    private string name;
    private string id;
    private int interest;
    private double income;


    public Client(string name, string id, int interest, double income)
    {
        this.Name = name;
        this.Id = id;
        this.Interest = interest;
        this.Income = income;
    }


    public string Name
    {
        get { return name; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.ClientNameNullOrWhitespace);
            }

            name = value;
        }
    }
    public string Id
    {
        get
        {
            return id;
        }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.ClientIdNullOrWhitespace);
            }

            id = value;
        }
    }
    public int Interest       //MOJE DA ISKA PROTECTED
    {
        get
        {
            return interest;
        }
        protected set
        {
            interest = value;
        }
    }
    public double Income
    {
        get
        {
            return income;
        }
        private set
        {
            if (value<=0)
            {
                throw new ArgumentException(ExceptionMessages.ClientIncomeBelowZero);
            }

            income = value;
        }
    }



    public abstract void IncreaseInterest();
   
}