using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// This class should contain all the "work" for catering
    /// </summary>
    public class Catering
    {
        public Dictionary<string, CateringItem> productMenu = new Dictionary<string, CateringItem>();

        public Money Money = new Money();

        // Log of all transactions. Add$, order item, & return$
        public List<Transaction> TransactionLog = new List<Transaction>();

        // Track orders made in a single transaction
        public Dictionary<string, int> OrderLog;

        /// <summary>
        ///Reads from file and creates a list of products 
        /// </summary>
        public void ProductMenuBuilder()
        {
            FileInput fileInput = new FileInput();
            fileInput.LoadProductMenuFromFile(productMenu); //converts list to products

        }

        public void FinalizeTransactions()
        {
            FileOutput fileOutput = new FileOutput();
            fileOutput.WriteToLog(TransactionLog);
        }
        public bool PlaceOrder(string input, int quantityToOrder)
        {
            //int index = productKey[input];
            CateringItem order = productMenu[input];
            if (order.Quantity >= quantityToOrder)
            {
                decimal cost = quantityToOrder * order.Price;
                if (cost <= Money.CheckBalance())
                {
                    Money.RemoveMoney(cost);
                    order.Quantity -= quantityToOrder;
                    LogTransaction($"{quantityToOrder} {productMenu[input].Name} {productMenu[input].Id}", cost, Money.CheckBalance());
                    if (!OrderLog.ContainsKey(order.Id))
                    {
                        OrderLog[order.Id] = quantityToOrder;
                    }
                    else
                    {
                        OrderLog[order.Id] += quantityToOrder;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Creates and adds a transaction to a list of transactions
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="balance"></param>
        public void LogTransaction(string type, decimal amount, decimal balance)
        {
       /*     // Restrict type field to 10 characters in transaction log.
            if (type.Length > 10)
            {
                type = type.Substring(0, 10);
            } */
            Transaction tx = new Transaction(type, amount, balance);
            TransactionLog.Add(tx);
        }

    }
}
