using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// This class provides all user communications, but not much else.
    /// All the "work" of the application should be done elsewhere
    /// </summary>
    public class UserInterface
    {
        private Catering catering = new Catering();

        /// <summary>
        /// Displays main menu and takes in and handles user input
        /// </summary>
        public void RunMainMenu()
        {
            bool done = false; //Ends program when true

            catering.ProductListBuilder(); //Reads file and creates product list

            while (!done)
            {
                Console.WriteLine("1) Display Catering Items\n2) Order\n3) Quit");

                string input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case "1": //Display available products, price, and quantity
                        DisplayCateringItems();
                        Console.WriteLine();
                        break;
                    case "2": //Displays order menu to add money or place order
                        DisplayOrderMenu();
                        break;
                    case "3": //Quits program
                        done = true;
                        catering.FinalizeTransactions();
                        break;
                    case "4":
                        foreach (Transaction item in catering.TransactionLog)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    default: //Returns to main menu if input is not recognized
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }


        /// <summary>
        /// Displays and directs input for order menu including adding money and ordering products
        /// </summary>
        public void DisplayOrderMenu()
        {
            bool ordering = true;
            catering.OrderLog = new Dictionary<string, int>();
            while (ordering)
            {
                Console.WriteLine("1) Add Money\n2) Select Products\n3) Complete Transaction");
                string input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case "1": //Add Money and create transaction for transaction log
                        Console.Write("How much do you want to add (in $): ");
                        if (int.TryParse(Console.ReadLine(), out int money))
                        {
                            if (!catering.Money.AddMoney(money))
                            {
                                Console.WriteLine($"Deposit exceeds account maximum of {catering.Money.AccountLimit}.");
                                Console.WriteLine();
                            }
                            else
                            {
                                catering.LogTransaction("ADD MONEY", money, catering.Money.CheckBalance());
                            }
                            Console.WriteLine("Account Balance:" + catering.Money.CheckBalance().ToString("C"));
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input. Please input whole dollars only.");
                        }
                        break;
                    case "2": //View and select products to purchase
                        DisplayCateringItems();
                        Console.WriteLine();
                        Console.Write("What do you want to order?: ");
                        input = Console.ReadLine().ToUpper();
                        if (catering.productKey.ContainsKey(input))
                        {
                            int quantityAvailable = catering.itemsList[catering.productKey[input]].Quantity;
                            string name = catering.itemsList[catering.productKey[input]].Name;
                            decimal price = catering.itemsList[catering.productKey[input]].Price;
                            Console.Write($"How many? (Available: {quantityAvailable}): ");
                            if (int.TryParse(Console.ReadLine(), out int quantityToOrder))
                            {
                                catering.PlaceOrder(input, quantityToOrder);
                                Console.WriteLine();
                                string orderPlaced = $"Order placed for {quantityToOrder} {name} Total Cost: {(price * quantityToOrder).ToString("C")}";
                                Console.WriteLine(orderPlaced);
                                Console.WriteLine("Account Balance:" + catering.Money.CheckBalance().ToString("C"));
                                Console.WriteLine();
                            }
                            else 
                            {
                                Console.WriteLine("Invalid input. Please put whole numbers only");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID. Please try again");
                        }
                        break;
                    case "3": //Complete transaction and return to main menu
                        decimal orderTotal = 0;

                        foreach (KeyValuePair<string, int> item in catering.OrderLog)
                        {
                            string name = catering.itemsList[catering.productKey[item.Key]].Name;
                            decimal price = catering.itemsList[catering.productKey[item.Key]].Price;
                            string type = catering.itemsList[catering.productKey[item.Key]].Type;
                            decimal lineTotal = price * item.Value;
                            orderTotal += lineTotal;

                            Console.WriteLine($"{item.Value} {type} {name} {price.ToString("C")} {lineTotal.ToString("C")}");
                            Console.WriteLine();
                        }
                        Console.WriteLine($"Total: {orderTotal.ToString("C")}");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine(catering.Money.ReturnMoney());
                        catering.LogTransaction("GIVE CHANGE", catering.Money.CheckBalance(), 0.00M);
                        catering.Money.RemoveMoney(catering.Money.CheckBalance());
                        Console.WriteLine($"Current balance: {catering.Money.CheckBalance().ToString("C")}");
                        Console.WriteLine();
                        Console.WriteLine();
                        ordering = false;
                        break;
                    case "4":
                        foreach (Transaction item in catering.TransactionLog)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    default: //Returns to order menu if input is not recognized
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }
        /// <summary>
        /// Writes the product list to the console
        /// </summary>
        public void DisplayCateringItems()
        {
            foreach (CateringItem item in catering.itemsList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
