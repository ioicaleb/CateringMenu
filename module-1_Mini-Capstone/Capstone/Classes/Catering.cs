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

        // Track orders made across all transactions
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
            fileOutput.WriteToLog(TransactionLog); //Writes Transaction Log output file
        }

        /// <summary>
        /// Handles modification of account balance nad item quantity if order is possible
        /// </summary>
        /// <param name="input"></param>
        /// <param name="quantityToOrder"></param>
        /// <returns></returns>
        public bool PlaceOrder(string input, int quantityToOrder)
        {
            
            CateringItem order = productMenu[input];
            if ((order.Quantity >= quantityToOrder) && quantityToOrder >= 0) //Checks if the item is available and the amount requested is not negative
            {
                decimal cost = quantityToOrder * order.Price;
                if (cost <= Money.CheckBalance())
                {
                    Money.RemoveMoney(cost); //Removes cost from account balance
                    order.Quantity -= quantityToOrder; //Removes ordered items from item quantity 
                    LogTransaction($"{quantityToOrder} {productMenu[input].Name} {productMenu[input].Id}", cost); //Adds transaction to Transaction Log
                    
                    decimal price = productMenu[input].Price;
                    string name = productMenu[input].Name;
                    decimal orderCost = price * quantityToOrder;

                    if (OrderHistory.ContainsKey(input)) //If product has been ordered before, modifies the amount that has been ordered and total cost for that product
                    {
                        OrderHistory[input].Quantity += quantityToOrder;
                        OrderHistory[input].OrderCost += orderCost;
                    }
                    else //If product has not been ordered before, entry in dictionary is created and given a value of a new order created from the order details
                    {
                        OrderHistory[input] = new Order(input, name, quantityToOrder, orderCost);
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if money can be added and logs transaction to transaction log if successful
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
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
            string changeDue = Money.ReturnMoney(); //Stores string output of ReturnMoney
            decimal changePaidOut = Money.CheckBalance(); //Stores the value of account balance that needs to be returned
            Money.RemoveMoney(Money.CheckBalance());
            LogTransaction("GIVE CHANGE", changePaidOut);
            return changeDue;
        }

        /// <summary>
        /// Reads current TotalSales.rpt, adds transactions to current OrderHistory, then writes OrderHistory to TotalSales.rpt
        /// </summary>
        public void UpdateOrderHistory()
        {
            fileInput.LoadFromTotalSales(OrderHistory);
            
            fileOutput.WriteToTotalSales(OrderHistory);
        }
    }
}
