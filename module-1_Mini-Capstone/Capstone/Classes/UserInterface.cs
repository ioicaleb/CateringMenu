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
        private Catering catering = new Catering(); //Everything runs through catering

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
                string mainMenuChoice = Console.ReadLine();
                Console.WriteLine();
                switch (mainMenuChoice)
                {
                    case "1": //Display available products, price, and quantity
                        DisplayCateringItems();
                        break;
                    case "2": //Displays order menu to add money or place order
                        DisplayOrderMenu();
                        break;
                    case "3": //Quits program
                        catering.FinalizeTxLog();
                        done = true;
                        break;
                    case "4": //Displays Transaction Log in console (DOES NOT APPEAR IN LIST TO USER)
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
        /// Displays and maintains a header over each menu and displays menu lists
        /// </summary>
        /// <param name="header"></param>
        /// <param name="options"></param>
        public void DisplayHeader(string header, string options)
        {
            Console.WriteLine("Weyland, Inc.  Catering System");
            if (header.Length < 30)
            {
                int length = 15 - (header.Length / 2);
                for (int i = 0; i < length; i++)
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
            foreach (KeyValuePair<string, CateringItem> menuItem in catering.ProductMenu)
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
            bool continueOrdering = true;
            while (continueOrdering)
            {
                DisplayHeader("Order Menu", "1) Add Money\n2) Select Products\n3) Complete Transaction");

                Console.Write("What would you like to do?  ");
                string orderMenuChoice = Console.ReadLine();
                Console.WriteLine();
                switch (orderMenuChoice)
                {
                    case "1": //Add Money and create transaction for transaction log
                        AddMoneyToAccount();
                        break;
                    case "2": //View and select products to purchase and process purchase
                        DisplayCateringItems();
                        MakePurchase();
                        break;
                    case "3": //Complete transaction, output total sales report, and return to main menu
                        CompleteTransaction();
                        continueOrdering = false;
                        break;
                    case "4": //Displays Transaction Log in console (DOES NOT APPEAR IN LIST TO USER)
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
        /// Gets user input of how much money to add to account and processes the method through Catering
        /// </summary>
        public void AddMoneyToAccount()
        {
            Console.Write("How much do you want to add (in $): ");
            if (int.TryParse(Console.ReadLine(), out int money)) //Displays message if input is not a whole number
            {
                // AddMoney returns false if the deposit would exceed maximum limit ($4,200.00) or if input is negative
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

        /// <summary>
        /// Gets user input of item Id and checks it against the product list. Will complete order if item is in stock and account balance has enough money
        /// </summary>
        public void MakePurchase()
        {
            Console.WriteLine();
            Console.Write("What do you want to order?: ");
            string idFromUser = Console.ReadLine().ToUpper();
            if (catering.ProductMenu.ContainsKey(idFromUser))
            {
                int quantityAvailable = catering.ProductMenu[idFromUser].Quantity;
                string itemName = catering.ProductMenu[idFromUser].Name;
                decimal price = catering.ProductMenu[idFromUser].Price;

                Console.Write($"How many? (Available: {quantityAvailable}): ");
                if (int.TryParse(Console.ReadLine(), out int qtyToOrder))
                {
                    bool wasOrderPlaced = catering.PlaceOrder(idFromUser, qtyToOrder);
                    Console.WriteLine();
                    if (wasOrderPlaced)
                    {
                        string orderString = $"Order placed for {qtyToOrder} {itemName} Total Cost: {(price * qtyToOrder).ToString("C")}";
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

        /// <summary>
        /// Displays order history and writes order history to output file C:\Catering\TotalSales.rpt
        /// </summary>
        public void CompleteTransaction()
        {
            decimal accumOrderTotal = 0.0M;
            DisplayHeader("You Purchased", "");

            // Display transaction detail
            foreach (KeyValuePair<string, Order> item in catering.OrderHistory)
            {
                string itemName = catering.ProductMenu[item.Key].Name;
                decimal itemPrice = catering.ProductMenu[item.Key].Price;
                string itemType = catering.ProductMenu[item.Key].Type;
                decimal orderLineCost = item.Value.OrderCost;
                accumOrderTotal += orderLineCost;

                Console.WriteLine($"{item.Key.ToString()} {itemType} {itemName} {itemPrice.ToString("C")} {orderLineCost.ToString("C")}");
            }

            // Display Transaction Summary
            Console.WriteLine();
            Console.WriteLine($"Total: {accumOrderTotal.ToString("C")}");
            Console.WriteLine(); Console.WriteLine();

            // Return money to customer, display return in largest possible money increments.
            // Then clear account balance
            Console.WriteLine($"Balance to be returned: {catering.Money.CheckBalance()}");
            Console.WriteLine(catering.GiveChange());

            Console.WriteLine($"Current balance: {catering.Money.CheckBalance().ToString("C")}");
            Console.WriteLine(); Console.WriteLine();
            
            //Merge order history with sales history file.  Also clears session order history
            catering.UpdateOrderHistory();
        }
    }
}
