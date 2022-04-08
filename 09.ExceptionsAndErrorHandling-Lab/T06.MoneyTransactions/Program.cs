using System;
using System.Collections.Generic;

namespace T06.MoneyTransactions
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, decimal> bankAccounts = new Dictionary<int, decimal>();
            string[] inputAccounts = Console.ReadLine().Split(',');
            foreach (var account in inputAccounts)
            {
                string[] accountInfo = account.Split('-');
                int number = int.Parse(accountInfo[0]);
                decimal balance = decimal.Parse(accountInfo[1]);
                bankAccounts[number] = balance;
            }

            string command = Console.ReadLine();
            while (command != "End")
            {
                string[] cmdArgs = command.Split();
                string type = cmdArgs[0];
                int accountNumber = int.Parse(cmdArgs[1]);
                decimal sum = decimal.Parse(cmdArgs[2]);
                try
                {
                    if (!bankAccounts.ContainsKey(accountNumber))
                    {
                        throw new ArgumentException("Invalid account!");
                    }

                    if (type == "Deposit")
                    {
                        bankAccounts[accountNumber] += sum;
                    }
                    else if (type == "Withdraw")
                    {
                        if (sum > bankAccounts[accountNumber])
                        {
                            throw new ArgumentException("Insufficient balance!");
                        }

                        bankAccounts[accountNumber] -= sum;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid command!");
                    }

                    Console.WriteLine($"Account {accountNumber} has new balance: {bankAccounts[accountNumber]:f2}");
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                finally
                {
                    Console.WriteLine("Enter another command");
                    command = Console.ReadLine();
                }
            }
        }
    }
}
