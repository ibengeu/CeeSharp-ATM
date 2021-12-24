using System;
using System.IO;

namespace ATM_software
{
    class ATMOperations
    {
        int pin = 1234;
        readonly DBOperations transaction = new DBOperations();


        public void BeginTransaction()
        {
            bool IsVerified = false;

            int pinCode;
            Console.WriteLine("Welcome to GTBank, please enter your pin");
            string pinInput = Console.ReadLine();
            Int32.TryParse(pinInput, out pinCode);

            if (String.IsNullOrEmpty(pinInput))
            {
                Console.WriteLine("Please type a value");
                BeginTransaction();
            }
            else
            {
                IsVerified = VerifyPin(pinCode);
            }

            if (IsVerified)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1).Withdraw \n2).Deposit\n3).Show recent transactions\n4).Show Account Balance");
                string input = Console.ReadLine();


                switch (input)
                {
                    case "1":
                        WithdrawFunds();
                        break;

                    case "2":
                        AddFunds();
                        break;

                    case "3":
                        transaction.ReadTransactions();
                        break;

                    case "4":
                        transaction.ShowAcountBalance();
                        break;

                    default:
                        Console.WriteLine($"Invalid Option");
                        Console.ReadLine();
                        Console.Clear();
                        BeginTransaction();
                        break;

                }
            }
            else
            {
                Console.WriteLine("Wrong Pin");
                Console.ReadLine();
                Console.Clear();
                BeginTransaction();
            }




        }
        public bool VerifyPin(int pinCode)
        {
            if (pin == pinCode)
            {

                return true;
            }
            else
            {
                return false;
            }
        }


        public void WithdrawFunds()
        {
            int AccountBalance = 0;
            string AccountBalancePath = @"c:\temp\Account-balance.txt";

            try
            {
                using (StreamReader sr = new StreamReader(AccountBalancePath))
                {
                    string line = sr.ReadLine();
                    int.TryParse(line, out AccountBalance);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            Console.WriteLine("How much would you like to withdraw?");
            string amtInput = Console.ReadLine();
            //cast or parse the string fromm the command line into an integer for comparison
            Int32.TryParse(amtInput, out int AmtToWithDraw);

            if (AccountBalance >= AmtToWithDraw)
            {
                AccountBalance -= AmtToWithDraw;

                Console.WriteLine($"You withdrew {AmtToWithDraw}, your new Account balance is {AccountBalance}");
                try
                {
                    using (StreamReader reader = new StreamReader(transaction.AccountBalancePath))
                    {
                        Int32.TryParse(reader.ReadLine(), out int balance);
                        int TotalBalance = balance - AmtToWithDraw;
                        transaction.WriteTransactionsToFile($"{TotalBalance}", "withdraw");

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            else
            {
                Console.WriteLine("You cannt withdraw more than your account balance");
                Console.ReadLine();
                Console.Clear();
                BeginTransaction();
            }
        }

        public void AddFunds()
        {
            Console.Clear();
            Console.WriteLine("How much would you like to Deposit?");
            Int32.TryParse(Console.ReadLine(), out int amt);

            try
            {
                using (StreamReader reader = new StreamReader(transaction.AccountBalancePath))
                {
                    Int32.TryParse(reader.ReadLine(), out int balance);
                    int TotalBalance = balance + amt;
            Console.WriteLine($"You Added {amt}, your new Account balance is {TotalBalance}");
                    transaction.WriteTransactionsToFile($"{TotalBalance}", "deposit");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


    }


}
