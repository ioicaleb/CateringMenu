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

        public Money Money = new Money();

        public List<Transaction> TransactionLog = new List<Transaction>();
        
        /// <summary>
        ///Reads from file and creates a list of products 
        /// </summary>
        public void ProductListBuilder()
        {
            FileInput fileInput = new FileInput();
            fileInput.FileLoader(items); //converts list to products
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
