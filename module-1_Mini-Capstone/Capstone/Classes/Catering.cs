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

        public void ProductListBuilder()
        {
            FileInput fileInput = new FileInput();
            fileInput.FileLoader(items);
        }

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
