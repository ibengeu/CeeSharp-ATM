using System.IO;
using System;

namespace ATM_software
{
    class DBOperations
    {
        public void WriteTransactionsToFile(string transaction)
        {
        StreamWriter writer = new StreamWriter("Test.txt");
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                writer.WriteLine(transaction);

                //Close the file
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        public void ReadTransactionsFromFile()
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader("Test.txt"))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    
    }
}
