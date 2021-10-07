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
            if ((AccountBalance + money) <= AccountLimit)
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

        public string ReturnMoney()
        {
            int accountBalance = (int)(AccountBalance * 100);

            string changeDue = "Change due: ";

            if (accountBalance >= 2000)
            {
                int bills20s = accountBalance / 2000;
                accountBalance = accountBalance % 2000;
                changeDue += $"{bills20s} 20s ";
            }
            if (accountBalance >= 1000)
            {
                int bills10s = accountBalance / 1000;
                accountBalance = accountBalance % 1000;
                changeDue += $"{bills10s} 10s ";
            }
            if (accountBalance >= 500)
            {
                int bills5s = accountBalance / 500;
                accountBalance = accountBalance % 500;
                changeDue += $"{bills5s} 5s ";
            }
            if (accountBalance >= 100)
            {
                int bills1s = accountBalance / 100;
                accountBalance = accountBalance % 100;
                changeDue += $"{bills1s} 1s ";
            }
            if (accountBalance >= 25)
            {
                int coinsQuarters = accountBalance / 25;
                accountBalance = accountBalance % 25;
                changeDue += $"{coinsQuarters} quarters ";
            }
            if (accountBalance >= 10)
            {
                int coinsDimes = accountBalance / 10;
                accountBalance = accountBalance % 10;
                changeDue += $"{coinsDimes} dimes ";
            }
            if (accountBalance >= 5)
            {
                int coinsNickels = accountBalance / 5;
                accountBalance = accountBalance % 5;
                changeDue += $"{coinsNickels} nickels";
            }
            
            return changeDue;
        }
    }
}
