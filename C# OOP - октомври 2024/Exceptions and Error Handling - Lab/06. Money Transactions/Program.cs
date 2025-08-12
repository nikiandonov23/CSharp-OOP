using System.Runtime.InteropServices;

namespace _06._Money_Transactions
{
    public class Program
    {
        public static void Main()
        {
            Dictionary<int, Account> allAcc = new Dictionary<int, Account>();

            string[] input = Console.ReadLine().Split(",").ToArray();
            foreach (var element in input)
            {
                string[] tockens = element.Split("-");
                int number = int.Parse(tockens[0]);
                decimal balance = decimal.Parse(tockens[1]);

                Account currentAccount = new Account(number, balance);
                allAcc.Add(number, currentAccount);

            }



            string command = "";
            while ((command = Console.ReadLine()) != "End")
            {
                try
                {
                    string[] tockens = command.Split(" ");
                    string action = tockens[0];
                    int number = int.Parse(tockens[1]);
                    decimal sum = decimal.Parse(tockens[2]);

                    Operation(action, number, sum, allAcc);
                    Console.WriteLine(allAcc[number]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
                finally
                {
                    Console.WriteLine("Enter another command");
                }

            }

            Console.WriteLine();


            void Operation(string command, int number, decimal sum, Dictionary<int, Account> allAcc)


            {

                if (allAcc.ContainsKey(number))
                {
                    if (command == "Withdraw" && sum > allAcc[number].Balance)
                    {
                        throw new Exception("Insufficient balance!");
                    }
                    else if (command == "Withdraw")
                    {
                        allAcc[number].Balance -= sum;
                    }

                    else if (command == "Deposit")
                    {
                        allAcc[number].Balance += sum;
                    }

                    else if (command != "Deposit" && command != "Withdraw")
                    {
                        throw new Exception("Invalid command!");

                    }
                }


                else
                {
                    throw new Exception("Invalid account!");
                }
            }

        }

        public class Account
        {
            public int Number { get; set; }
            public decimal Balance { get; set; }

            public Account(int number, decimal balance)
            {
                Number = number;
                Balance = balance;
            }

            public override string ToString()
            {
                return $"Account {this.Number} has new balance: {this.Balance:F2}";
            }
        }

     


    }



}
