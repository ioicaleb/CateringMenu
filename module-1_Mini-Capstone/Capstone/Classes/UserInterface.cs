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

            catering.ProductMenuBuilder(); //Reads file and creates product list

            while (!done)
            {
                DisplayHeader("Main Menu", "1) Display Catering Items\n2) Order\n3) Quit");
                Console.WriteLine();
                Console.Write("What would you like to do?  ");
                string input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case "1": //Display available products, price, and quantity
                        DisplayCateringItems();
                        break;
                    case "2": //Displays order menu to add money or place order
                        DisplayOrderMenu();
                        break;
                    case "3": //Quits program
                        catering.FinalizeTransactions();
                        done = true;
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

        public void DisplayHeader(string header, string options)
        {
                Console.WriteLine("Weyland, Inc.  Catering System");
            if (header.Length < 30)
            {
                int length = 15 - (header.Length/2);
                for(int i=0; i < length; i++)
                {
                    header = " " + header;
                }
            }
                Console.WriteLine(header);
                Console.WriteLine("******************************");
            if (options != "")
            {
                Console.WriteLine(options);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Writes the product list to the console
        /// </summary>
        public void DisplayCateringItems()
        {
            DisplayHeader("Current Product Availability", "");
            foreach (KeyValuePair<string, CateringItem> menuItem in catering.productMenu)
            {
                Console.WriteLine(menuItem.Value);
            }
            Console.WriteLine(); Console.WriteLine();
        }

        /// <summary>
        /// Displays and directs input for order menu including adding money and ordering products
        /// </summary>
        public void DisplayOrderMenu()
        {
            bool ordering = true;
            while (ordering)
            {
                DisplayHeader("Order Menu", "1) Add Money\n2) Select Products\n3) Complete Transaction");

                Console.Write("What would you like to do?  ");
                string input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case "1": //Add Money and create transaction for transaction log
                        AddMoneyToAccount();
                        break;
                    case "2": //View and select products to purchase
                        DisplayCateringItems();
                        MakePurchase();
                        break;
                    case "3": //Complete transaction and return to main menu
                        CompleteTransaction();
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

        public void AddMoneyToAccount()
        {
            Console.Write("How much do you want to add (in $): ");
            if (int.TryParse(Console.ReadLine(), out int money))
            {
                // AddMoney returns false if the deposit would exceed maximumlimit.
                if (!catering.AddMoneyToAccount(money))
                {
                    Console.WriteLine($"Deposit amount cannot be negative.");
                    Console.WriteLine($"Deposit cannot exceed account maximum of {catering.Money.AccountLimit}.");
                    Console.WriteLine();
                }
                Console.WriteLine("Account Balance:" + catering.Money.CheckBalance().ToString("C"));
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Invalid Input. Please input whole dollars only.");
            }
        }

        public void MakePurchase()
        {
            Console.WriteLine();
            Console.Write("What do you want to order?: ");
            string input = Console.ReadLine().ToUpper();
            // if (catering.productKey.ContainsKey(input))
            if (catering.productMenu.ContainsKey(input))
            {
                int quantityAvailable = catering.productMenu[input].Quantity;
                string name = catering.productMenu[input].Name;
                decimal price = catering.productMenu[input].Price;

                Console.Write($"How many? (Available: {quantityAvailable}): ");
                if (int.TryParse(Console.ReadLine(), out int quantityToOrder))
                {
                    bool orderPlaced = catering.PlaceOrder(input, quantityToOrder);
                    Console.WriteLine();
                    if (orderPlaced)
                    {
                        string orderString = $"Order placed for {quantityToOrder} {name} Total Cost: {(price * quantityToOrder).ToString("C")}";
                        Console.WriteLine(orderString);
                    }
                    else
                    {
                        Console.WriteLine("Order not placed. Check account balance and available quantity");
                    }
                    Console.WriteLine("Account Balance:" + catering.Money.CheckBalance().ToString("C"));
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please put whole positive numbers only");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID. Please try again");
            }
        }

        public void CompleteTransaction()
        {
            decimal orderTotal = 0;
            DisplayHeader("You Purchased", "");

            // Display transaction detail
            foreach (KeyValuePair<string, Order> item in catering.OrderHistory)
            {
                string name = catering.productMenu[item.Key].Name;
                decimal price = catering.productMenu[item.Key].Price;
                string type = catering.productMenu[item.Key].Type;
                decimal orderCost = item.Value.OrderCost;
                orderTotal += orderCost;

                Console.WriteLine($"{item.Key.ToString()} {type} {name} {price.ToString("C")} {orderCost.ToString("C")}");
            }
                Console.WriteLine();

            // Display Transaction Summary
            Console.WriteLine($"Total: {orderTotal.ToString("C")}");
            Console.WriteLine(); Console.WriteLine();

            // Return money to customer and clear account balance
            Console.WriteLine($"Balance to be returned: {catering.Money.CheckBalance()}");
            Console.WriteLine(catering.GiveChange());
            
            Console.WriteLine($"Current balance: {catering.Money.CheckBalance().ToString("C")}");
            Console.WriteLine(); Console.WriteLine();

            catering.UpdateOrderHistory();
        }


    }
}
