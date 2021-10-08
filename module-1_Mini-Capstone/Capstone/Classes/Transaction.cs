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


        //Sets display of Transaction for output file and hidden menu display method
        public override string ToString() 
        {
            return $"{TimeLog} {TxType} {TransactionAmount.ToString("C")} {UpdatedBalance.ToString("C")}";
        }
    }
}
