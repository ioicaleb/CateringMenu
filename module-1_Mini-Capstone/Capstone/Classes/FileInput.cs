using System;
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
        private string filePath = @"C:\Catering\CateringSystem.csv";

        public void FileLoader(List<CateringItem> list, Dictionary<string, int> productKey, Dictionary<string, CateringItem> productDictionary)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    int index = 0;
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        string[] lineSplit = line.Split("|");

                        string type = lineSplit[0]; //Saves type as full name instead of initial
                        switch (type)
                        {
                            case "B":
                                type = "Beverage";
                                break;
                            case "A":
                                type = "Appetizer";
                                break;
                            case "E":
                                type = "Entree";
                                break;
                            case "D":
                                type = "Dessert";
                                break;
                        }

                        string id = lineSplit[1];
                        string name = lineSplit[2];
                        decimal price = decimal.Parse(lineSplit[3]);
                        CateringItem productForDictionary = new CateringItem(type, id, name, price);
                        //type, id, name, quantity, price
                        list.Add(productForDictionary);
                        productKey[id] = index;
                        productDictionary[id] = productForDictionary;
                        index++;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Unable to read file");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
