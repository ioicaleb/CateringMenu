using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class FileOutput
    {
        public string txLogOutputPath = @"C:\Catering\Log.txt";
        public string salesReportPath = @"C:\Catering\TotalSales.rpt";
        /// <summary>
        /// Writes Transaction Log to file Log.txt
        /// </summary>
        /// <param name="transactions"></param>
        public void GenerateTxLogFile(List<Transaction> transactions)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(txLogOutputPath, false))
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

        /// <summary>
        /// Writes OrderHistory to TotalSales.rpt and removes entry from OrderHistory once written to file
        /// </summary>
        /// <param name="OrderHistory"></param>
        public void WriteToTotalSales(Dictionary<string, Order> OrderHistory)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(salesReportPath, false))
                {
                    decimal totalSalesCost = 0;
                    foreach (KeyValuePair<string, Order> order in OrderHistory)
                    {
                        totalSalesCost += order.Value.OrderCost;
                        string orderOutput = $"{order.Key}|{order.Value.Name}|{order.Value.Quantity}|{order.Value.OrderCost}";
                        writer.WriteLine(orderOutput);
                        OrderHistory.Remove(order.Key);
                    }
                    writer.WriteLine();
                    writer.WriteLine();
                    writer.WriteLine($"**TOTAL SALES** {totalSalesCost.ToString("C")}");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Unable to write file");
            }
        }
    }
}
