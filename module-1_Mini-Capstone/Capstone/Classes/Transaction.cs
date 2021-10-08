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

        public Transaction(string type, decimal amount)
        {
            TxType = type;
            TransactionAmount = amount;
        }
        

        public override string ToString() //Sets display of Transaction for output file and hidden menu display method
        {
            return $"{TimeLog} {TxType} {TransactionAmount.ToString("C")} {UpdatedBalance.ToString("C")}";
        }
    }
}
