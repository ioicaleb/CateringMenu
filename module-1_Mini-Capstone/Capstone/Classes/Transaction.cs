using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Transaction
    {
        public DateTime TimeLog = DateTime.Now;

        public string TxType;

        public decimal TransactionAmount;

        public decimal UpdatedBalance;

        public Transaction(string type, decimal amount, decimal balance)
        {
            TxType = type;
            TransactionAmount = amount;
            UpdatedBalance = balance;
        }
        

        public override string ToString()
        {
            return $"{TimeLog} {TxType} {TransactionAmount.ToString("C")} {UpdatedBalance.ToString("C")}";
        }
    }
}
