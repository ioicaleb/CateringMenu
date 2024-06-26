﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// This class should contain any and all details of access to files
    /// </summary>
    public class FileInput
    {
        // All external data files for this application should live in this directory.
        // You will likely need to create this directory and copy / paste any needed files.
        private string csvMenuInputPath = @"C:\Catering\CateringSystem.csv";
        private string salesReportPath = @"C:\Catering\TotalSales.rpt";

        public void LoadProductMenuFromFile(Dictionary<string, CateringItem> productDictionary)
        {
            try
            {
                using (StreamReader reader = new StreamReader(csvMenuInputPath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        // Parse | seperated line of text
                        string[] lineSplit = line.Split("|");
                        string type = lineSplit[0];
                        string id = lineSplit[1];
                        string name = lineSplit[2];
                        decimal price = decimal.Parse(lineSplit[3]);

                        // Reformat type & name for screan readability
                        type = TypeExpander(type);
                        name = NameExpander(name);

                        // Create object and save to dictionary
                        CateringItem productForDictionary = new CateringItem(type, id, name, price);
                        productDictionary[id] = productForDictionary;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Unable to read file");
                Console.WriteLine(ex.Message);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("File entry is not formatted properly");
                Console.WriteLine(ex.Message);
            }
        }

        // Expand item type to a full word for better screen readability.
        private string TypeExpander(string type)
        {
            switch (type)
            {
                case "B":
                    type = "Beverage ";
                    break;
                case "A":
                    type = "Appetizer";
                    break;
                case "E":
                    type = "Entree   ";
                    break;
                case "D":
                    type = "Dessert  ";
                    break;
            }
            return type;
        }

        // Pad item descriptions to 20 characters for screen formating.
        public string NameExpander(string name)
        {
            if (name.Length < 20)
            {
                int padding = (20 - name.Length);
                for (int i = 0; i < padding; i++)
                {
                    name += " ";
                }
            }
            return name;
        }

        /// <summary>
        /// Reads TotalSales.rpt file and merges past sales data with current session of OrderHistory
        /// </summary>
        /// <param name="OrderHistory"></param>
        public void LoadFromTotalSales(Dictionary<string, Order> OrderHistory)
        {
            try
            {
                using (StreamReader reader = new StreamReader(salesReportPath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        // Parse | seperated line of text.  Stop at first blank line.
                        if (line == "")
                        {
                            break;
                        }
                        string[] lineSplit = line.Split("|");
                        string id = lineSplit[0];
                        string name = lineSplit[1];
                        int quantity = int.Parse(lineSplit[2]);
                        decimal orderCost = decimal.Parse(lineSplit[3]);


                        // we shouldn't need this.  Name should already be expanded.
                        // Reformat type & name for screan readability
                        //  name = NameExpander(name);

                        // If product has been purchased this session, merge current order with order history.
                        // If product was not purchased this session, just preserve order history.
                        if (OrderHistory.ContainsKey(id))
                        {
                            OrderHistory[id].Quantity += quantity;
                            OrderHistory[id].OrderCost += orderCost;
                        }
                        else
                        {
                            OrderHistory[id] = new Order(id, name, quantity, orderCost);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Unable to read file");
                Console.WriteLine(ex.Message);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("File is improperly formatted");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
