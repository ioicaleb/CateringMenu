using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class FileOutput
    {
        public void WriteToLog(List<Transaction> transactions)
        {
            string outputPath = @"C:\Catering\Log.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    foreach (Transaction item in transactions)
                    {
                        writer.WriteLine(item);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Unable to write file");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
