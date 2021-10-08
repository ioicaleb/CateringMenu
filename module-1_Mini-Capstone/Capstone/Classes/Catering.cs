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
        //public Dictionary<string, int> OrderLog = new Dictionary<string, int>();

        public Dictionary<string, Order> OrderHistory = new Dictionary<string, Order>();
        
        private FileInput fileInput = new FileInput();

        private FileOutput fileOutput = new FileOutput();

        /// <summary>
        ///Reads from file and creates a list of products 
        /// </summary>
        public void ProductMenuBuilder()
        {
            fileInput.LoadProductMenuFromFile(productMenu); //converts list to products

        }

        public void FinalizeTransactions()
        {
            fileOutput.WriteToLog(TransactionLog);
        }

        public bool PlaceOrder(string input, int quantityToOrder)
        {
            
            CateringItem order = productMenu[input];
            if ((order.Quantity >= quantityToOrder) && quantityToOrder >= 0)
            {
                decimal cost = quantityToOrder * order.Price;
                if (cost <= Money.CheckBalance())
                {
                    Money.RemoveMoney(cost);
                    order.Quantity -= quantityToOrder;
                    LogTransaction($"{quantityToOrder} {productMenu[input].Name} {productMenu[input].Id}", cost);
                    
                    decimal price = productMenu[input].Price;
                    string name = productMenu[input].Name;
                    decimal orderCost = price * quantityToOrder;

                    if (OrderHistory.ContainsKey(input))
                    {
                        OrderHistory[input].Quantity += quantityToOrder;
                        OrderHistory[input].OrderCost += orderCost;
                    }
                    else
                    {
                        OrderHistory[input] = new Order(input, name, quantityToOrder, orderCost);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool AddMoneyToAccount(int money)
        {
            //checks if transaction can be completed, completes transaction, and logs to transaction log
            if (Money.AddMoney(money))
            {
                LogTransaction("ADD MONEY", money);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Creates and adds a transaction to a list of transactions
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="balance"></param>
        public void LogTransaction(string type, decimal amount)
        {
            /*     // Restrict type field to 10 characters in transaction log.
                 if (type.Length > 10)
                 {
                     type = type.Substring(0, 10);
                 } */
            Transaction tx = new Transaction(type, amount);
            tx.UpdatedBalance = Money.CheckBalance();
            TransactionLog.Add(tx);
        }

        public string GiveChange()
        {
            string changeDue = Money.ReturnMoney();
            decimal changePaidOut = Money.CheckBalance();
            Money.RemoveMoney(Money.CheckBalance());
            LogTransaction("GIVE CHANGE", changePaidOut);
            return changeDue;
        }

        public void UpdateOrderHistory()
        {
            fileInput.LoadFromTotalSales(OrderHistory);
            
            fileOutput.WriteToTotalSales(OrderHistory);
        }
    }
}
