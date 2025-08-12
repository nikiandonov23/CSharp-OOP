using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;

namespace BankLoan.Models;

public abstract class Bank:IBank
{
    private string name;
    private int capacity;
    private List<ILoan> loans;
    private List<IClient> clients;


    public Bank(string name, int capacity)
    {
        this.Name = name;
        this.Capacity = capacity;
        this.clients = new List<IClient>();
        this.loans = new List<ILoan>();
    }


    public string Name
    {
        get
        {
            return name;
        }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
            }

            name = value;
        }
    }
    public int Capacity
    {
        get
        {
            return capacity;
        }
        private set
        {
            capacity = value;
        }
    }
    public IReadOnlyCollection<ILoan> Loans
    {
        get
        {
            return loans.AsReadOnly();

        }
    }
    public IReadOnlyCollection<IClient> Clients
    {
        get
        {
            return clients.AsReadOnly();
        }
    }





    public double SumRates()
    {
        return loans.Sum(x => x.InterestRate);
    }
    public void AddClient(IClient Client)
    {
        if (clients.Count>=Capacity)
        {
            throw new ArgumentException(ExceptionMessages.NotEnoughCapacity);
        }
        clients.Add(Client);
    }
    public void RemoveClient(IClient Client)
    {
        clients.Remove(Client);
    }
    public void AddLoan(ILoan loan)
    {
       loans.Add(loan);
    }



    public string GetStatistics()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"Name: {Name}, Type: {this.GetType().Name}");

        if (clients.Count==0)
        {
            result.AppendLine("Clients: none");
        }
        else
        {
            string clientNames = string.Join(", ", clients.Select(cl => cl.Name));
            result.AppendLine($"Clients: {clientNames}");
        }

        result.AppendLine($"Loans: {Loans.Count}, Sum of Rates: {SumRates()}");
        return result.ToString().Trim();
    }
}