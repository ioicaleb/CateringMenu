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
        private List<CateringItem> items = new List<CateringItem>();

        public Dictionary<string, int> productKey = new Dictionary<string, int>();

        public Money Money = new Money();

        public List<Transaction> TransactionLog = new List<Transaction>();

        public Dictionary<string, CateringItem> productDictionary = new Dictionary<string, CateringItem>();

        public Dictionary<string, int> OrderLog;

        /// <summary>
        ///Reads from file and creates a list of products 
        /// </summary>
        public void ProductListBuilder()
        {
            FileInput fileInput = new FileInput();
            fileInput.FileLoader(items, productKey, productDictionary); //converts list to products

        }

        public void FinalizeTransactions()
        {
            FileOutput fileOutput = new FileOutput();
            fileOutput.WriteToLog(TransactionLog);
        }
        public bool PlaceOrder(string input, int quantityToOrder)
        {
            int index = productKey[input];
            CateringItem order = items[index];
            if (order.Quantity >= quantityToOrder)
            {
                decimal cost = quantityToOrder * order.Price;
                if (cost <= Money.CheckBalance())
                {
                    Money.RemoveMoney(cost);
                    order.Quantity -= quantityToOrder;
                    LogTransaction($"{quantityToOrder} {items[index].Name} {items[index].Id}", cost, Money.CheckBalance());
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
            Transaction tx = new Transaction(type, amount, balance);
            TransactionLog.Add(tx);
        }

        public CateringItem[] itemsList
        {
            get { return items.ToArray(); }
        }
    }
}
