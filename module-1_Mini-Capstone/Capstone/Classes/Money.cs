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


        public decimal CheckBalance()
        {
            return AccountBalance;
        }

        public bool AddMoney(int money)
        {
            if ((AccountBalance + money) <= AccountLimit)
            {
                AccountBalance += money;
                return true;
            }
            return false;
        }


        public bool RemoveMoney(decimal money)
        {
            if ((AccountBalance - money) >= 0)
            {
                AccountBalance -= money;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{AccountBalance.ToString("C")}";
        }
    }
}
