using System;


namespace ATM_software
{
    class ATMOperations
    {
        int pin = 1234;
        double AccountBalance;


        public void BeginTransaction()
        {
            Console.Clear();

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1).Withdraw \n2).Deposit");
            string input = Console.ReadLine();

          
                switch (input)
                {
                    case "1":
                        WithdrawFunds();
                        break;

                    case "2":
                        AddFunds();
                        break;

                    default:
                        Console.WriteLine($"Invalid Option");
                        BeginTransaction();
                        Console.Clear();
                        break;
                
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
            Console.Clear();
            bool IsVerified= false;

            int pinCode;

            Console.WriteLine("Enter Your Pin");
            string pinInput = Console.ReadLine();
            Int32.TryParse(pinInput, out pinCode);


            if (String.IsNullOrEmpty(pinInput))
            {
                Console.WriteLine("Please type a value");
                WithdrawFunds();
            }
            else
            {
                IsVerified = VerifyPin(pinCode);
            }


            if (IsVerified)
            {

                Console.WriteLine("How much would you like to withdraw?");
                string amtInput = Console.ReadLine();
                int AmtToWithDraw;
                Int32.TryParse(amtInput, out AmtToWithDraw);

                if (AccountBalance >= AmtToWithDraw)
                {
                    AccountBalance -= AmtToWithDraw;
                    Console.WriteLine($"You withdrew {AmtToWithDraw}, your new Account balance is {AccountBalance}");
                }
                else
                {
                    Console.WriteLine("You cannt withdraw more than your account balance");
                    BeginTransaction();
                }
            }
            else
            {
                Console.WriteLine("Wrong Pin");
                WithdrawFunds();
                Console.Clear();
            }

            
        }

        public void AddFunds()
        {

            Console.WriteLine("How much would you like to Deposit?");
            Int32.TryParse(Console.ReadLine(), out int amt);

            AccountBalance += amt;
            Console.WriteLine($"You Added {amt}, your new Account balance is {AccountBalance}");

            DBOperations transaction = new DBOperations();
            transaction.WriteTransactionsToFile($"Amount Added - {amt}"); 

            transaction.ReadTransactionsFromFile();
        }

       
    }
}
