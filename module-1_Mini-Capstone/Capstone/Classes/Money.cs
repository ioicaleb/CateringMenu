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
    }
}
