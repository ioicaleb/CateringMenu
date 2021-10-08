using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Money
    {
        public Money() { }

        private decimal AccountBalance;

        public decimal AccountLimit { get; } = 4200.00M;

        /// <summary>
        /// Returns the current account balance
        /// </summary>
        /// <returns></returns>
        public decimal CheckBalance()
        {
            return AccountBalance;
        }

        /// <summary>
        /// Checks if transaction will exceed account limit and completes transaction if it does not
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public bool AddMoney(int money)
        {
            if (((AccountBalance + money) <= AccountLimit) && (money >= 0))
            {
                AccountBalance += money;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if transaction will overdraft account and completes transaction if it does not
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public bool RemoveMoney(decimal money)
        {
            if ((AccountBalance - money) >= 0)
            {
                AccountBalance -= money;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calculates and returns string of proper change for remaining account balance
        /// </summary>
        /// <returns></returns>
        public string ReturnMoney()
        {
            int remainingBalance = (int)(AccountBalance * 100);

            string changeDue = "Change due: ";

            if (remainingBalance >= 2000)
            {
                int twentys = remainingBalance / 2000;
                remainingBalance = remainingBalance % 2000;
                changeDue += $"{twentys} Twentys ";
            }
            if (remainingBalance >= 1000)
            {
                int tens = remainingBalance / 1000;
                remainingBalance = remainingBalance % 1000;
                changeDue += $"{tens} Tens ";
            }
            if (remainingBalance >= 500)
            {
                int fives = remainingBalance / 500;
                remainingBalance = remainingBalance % 500;
                changeDue += $"{fives} Fives ";
            }
            if (remainingBalance >= 100)
            {
                int ones = remainingBalance / 100;
                remainingBalance = remainingBalance % 100;
                changeDue += $"{ones} Ones ";
            }
            if (remainingBalance >= 25)
            {
                int quarters = remainingBalance / 25;
                remainingBalance = remainingBalance % 25;
                changeDue += $"{quarters} Quarters ";
            }
            if (remainingBalance >= 10)
            {
                int dimes = remainingBalance / 10;
                remainingBalance = remainingBalance % 10;
                changeDue += $"{dimes} Dimes ";
            }
            if (remainingBalance >= 5)
            {
                int nickels = remainingBalance / 5;
                remainingBalance = remainingBalance % 5;
                changeDue += $"{nickels} Nickels";
            }
            
            return changeDue;
        }
    }
}
