using System.IO;
using System;

namespace ATM_software
{
    class DBOperations
    {
        private string TransactionRecordPath = @"c:\temp\transaction-record.txt";
        public string AccountBalancePath = @"c:\temp\Account-balance.txt";
        public void WriteTransactionsToFile(string transaction, string TransactionType)
        {
            try
            {
                RecordTransaction(transaction, TransactionType);
                using (StreamWriter sw = new StreamWriter(AccountBalancePath))
                {
                    switch (TransactionType)
                    {
                        case "deposit":
                            sw.WriteLine(transaction);

                            break;
                        case "withdraw":
                            sw.WriteLine(transaction);

                            break;
                        default:
                            throw new Exception("Invalid option");
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void RecordTransaction(string transaction, string TransactionType)
        {
            try
            {
                File.AppendAllText(TransactionRecordPath, $"{TransactionType} - {transaction} {DateTime.Now + Environment.NewLine}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ReadTransactions()
        {
            using (StreamReader reader = new StreamReader(TransactionRecordPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }

            }
        }
        public void ShowAcountBalance()
        {
            using (StreamReader reader = new StreamReader(AccountBalancePath))
            {
                string balance = reader.ReadLine();
                Console.WriteLine($"Your account balance is - {balance}");

            }
        }
    }
}
