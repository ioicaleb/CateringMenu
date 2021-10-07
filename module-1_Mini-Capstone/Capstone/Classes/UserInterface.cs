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
                        break;
                    default: //Returns to main menu if input is not recognized
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
                //done = catering.MainMenuHandler(input);
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

        /// <summary>
        /// Displays and directs input for order menu including adding money and ordering products
        /// </summary>
        public void DisplayOrderMenu()
        {
            bool ordering = true;
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
                            }
                            else
                            {
                                catering.LogTransaction("ADD MONEY", money, catering.Money.CheckBalance());
                            }
                            Console.WriteLine("Account Balance:" +catering.Money.CheckBalance().ToString("C"));
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input. Please input whole dollars only.");
                        }
                        break;
                    case "2": //View and select products to purchase
                        break;
                    case "3": //Complete transaction and return to main menu
                        ordering = false;
                        break;
                    default: //Returns to order menu if input is not recognized
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }
    }
}
