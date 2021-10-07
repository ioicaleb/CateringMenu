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

        public void RunMainMenu()
        {
            bool done = false;

            catering.ProductListBuilder();

            while (!done)
            {
                Console.WriteLine("1) Display Catering Items\n2) Order\n3) Quit");

                string input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case "1":
                        DisplayCateringItems();
                        Console.WriteLine();
                        break;
                    case "2":
                        DisplayOrderMenu();
                        break;
                    case "3":
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
                //done = catering.MainMenuHandler(input);
            }
        }

        public void DisplayCateringItems()
        {
            foreach (CateringItem item in catering.itemsList)
            {
                Console.WriteLine(item);
            }
        }

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
                    case "1":
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
                    case "2":
                        break;
                    case "3":
                        ordering = false;
                        break;
                }
            }
        }
    }
}
