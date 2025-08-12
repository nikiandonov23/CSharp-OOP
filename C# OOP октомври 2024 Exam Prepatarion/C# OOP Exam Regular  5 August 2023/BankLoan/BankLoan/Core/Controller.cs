using System;
using System.Linq;
using System.Text;
using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Utilities.Messages;

namespace BankLoan.Core;

public class Controller:IController
{
    private LoanRepository loans = new LoanRepository();
    private BankRepository banks  = new BankRepository();
    private string[] validBankTypes = new[] { nameof(BranchBank), nameof(CentralBank) };
    private string[] validLoanTypes = new[] { nameof(MortgageLoan), nameof(StudentLoan) };
    private string[] validClientTypes = new[] { nameof(Student), nameof(Adult) };

    public string AddBank(string bankTypeName, string name)
    {
        if (!validBankTypes.Contains(bankTypeName))
        {
            throw new ArgumentException(ExceptionMessages.BankTypeInvalid);

        }
        else
        {
            IBank newBank = null;
            switch (bankTypeName)
            {
                case $"{nameof(BranchBank)}":
                    newBank = new BranchBank(name);
                    break;

                case $"{nameof(CentralBank)}":
                    newBank = new CentralBank(name);
                    break;
            }

            banks.AddModel(newBank);

            return String.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
        }
        
    }    //дава 23
    public string AddLoan(string loanTypeName)
    {
        if (!validLoanTypes.Contains(loanTypeName))
        {
            throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);
        }

        ILoan newLoan = null;
        if (loanTypeName==nameof(MortgageLoan))
        {
            newLoan = new MortgageLoan();
        }

        if (loanTypeName==nameof(StudentLoan))
        {
            newLoan = new StudentLoan();
        }
        loans.AddModel(newLoan);

        return String.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
    }    //dava 23
    public string ReturnLoan(string bankName, string loanTypeName)
    {
       
        var currentBank = banks.FirstModel(bankName);

        var currentLoan = loans.FirstModel(loanTypeName);

        if (currentLoan == null)
        {
            throw new ArgumentException(string.Format(ExceptionMessages.MissingLoanFromType, loanTypeName));
        }

        currentBank.AddLoan(currentLoan);

        loans.RemoveModel(currentLoan);



        return string.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
    }     //дава 23 или 11
    public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
    {
        if (!validClientTypes.Contains(clientTypeName))
        {
            throw new ArgumentException(ExceptionMessages.ClientTypeInvalid);
        }


        var currentBank=banks.FirstModel(bankName);
        if (currentBank.GetType().Name==nameof(BranchBank)&&clientTypeName!=nameof(Student))
        {
            return String.Format(OutputMessages.UnsuitableBank);
        }

        if (currentBank.GetType().Name==nameof(CentralBank)&&clientTypeName!=nameof(Adult))
        {
            return String.Format(OutputMessages.UnsuitableBank);
        }

        IClient currentClient = null;
        if (clientTypeName==nameof(Student))
        {
            currentClient = new Student(clientName, id, income);
        }

        if (clientTypeName==nameof(Adult))
        {
            currentClient = new Adult(clientName, id, income);
        }
        currentBank.AddClient(currentClient);

        return String.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);

    }  //даде 34
    public string FinalCalculation(string bankName)
    {
        var currentBank = banks.FirstModel(bankName);

       
      

        double clientsIncomeSum = currentBank.Clients.Sum(client => client.Income);
        double loansAmountSum = currentBank.Loans.Sum(loan => loan.Amount);



        double totalFunds = clientsIncomeSum + loansAmountSum;

        
        return string.Format("The funds of bank {0} are {1:F2}.", bankName, totalFunds);
    }




    public string Statistics()
    {
        StringBuilder result = new StringBuilder();
        foreach (var bank in banks.Models)
        {
            result.AppendLine(bank.GetStatistics());
        }
        return result.ToString().Trim();
    }
}