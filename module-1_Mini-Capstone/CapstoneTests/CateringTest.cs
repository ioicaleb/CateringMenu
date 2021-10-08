using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class CateringTest
    {

        [TestMethod]
        public void ProductMenuBuilderSetsDictionary()
        {
            //Arrange
            //A populated CateringSystem.csv must exist in C:\Catering
            Catering catering = new Catering();

            //Act
            catering.ProductMenuBuilder();

            //Assert
            Assert.IsNotNull(catering.productMenu);
        }

        [TestMethod]
        [DataRow("B1", 1)]
        [DataRow("B1", 25)]
        public void PlaceOrderCompletesOrder(string id, int quantityToOrder)
        {
            // Arrange 
            Catering catering = new Catering();
            catering.ProductMenuBuilder();
            catering.Money.AddMoney(500);

            // Act
            bool succeeds = catering.PlaceOrder(id, quantityToOrder);

            // Assert
            Assert.IsTrue(succeeds);
        }

        [TestMethod]
        [DataRow("T1", 1, "99.00")]
        [DataRow("T1", 25, "75.00")]
        [DataRow("T1", 26, "100")]
        [DataRow("T1", -5, "100")]
        public void PlaceOrderRemovesMoneyCorrectly(string id, int quantityToOrder, string newBalance)
        {
            // Arrange 
            Catering catering = new Catering();
            catering.productMenu["T1"] = new CateringItem("test", "T1", "test", 1.00M);
            catering.Money.AddMoney(100);
            decimal expectedBalance = decimal.Parse(newBalance);

            // Act
            catering.PlaceOrder(id, quantityToOrder);
            decimal result = catering.Money.CheckBalance();

            // Assert
            Assert.AreEqual(expectedBalance, result);
        }

        [TestMethod]
        [DataRow("T1", 1, 24)]
        [DataRow("T1", 25, 0)]
        [DataRow("T1", 26, 25)]
        [DataRow("T1", -5, 25)]
        public void PlaceOrderReducesQuantityCorrectly(string id, int quantityToOrder, int expected)
        {
            // Arrange 
            Catering catering = new Catering();
            catering.productMenu["T1"] = new CateringItem("test", "T1", "test", 1.00M);
            catering.Money.AddMoney(500);

            // Act
            catering.PlaceOrder(id, quantityToOrder);
            int result = catering.productMenu[id].Quantity;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PlaceOrderPopulatesOrderLog()
        {
            // Arrange 
            Catering catering = new Catering();
            catering.productMenu["T1"] = new CateringItem("test", "T1", "test", 1.00M);
            catering.Money.AddMoney(100);

            // Act
            catering.PlaceOrder("T1", 10);


            // Assert
            Assert.IsTrue(catering.OrderHistory.ContainsKey("T1"));
            Assert.AreEqual(10, catering.OrderHistory["T1"].Quantity);
        }

        [TestMethod]
        public void PlaceOrderPopulatesTransactionLog()
        {
            // Arrange 
            Catering catering = new Catering();
            catering.productMenu["T1"] = new CateringItem("test", "T1", "test", 1.00M);
            catering.Money.AddMoney(100);
            //TxType is "quantityToOrder + item.name + item.type"
            string testTx = $"10 test T1";

            // Act
            catering.PlaceOrder("T1", 10);

            // Assert
            Assert.AreEqual(testTx, catering.TransactionLog[0].TxType); 
            Assert.AreEqual(10.00M, catering.TransactionLog[0].TransactionAmount);
            Assert.AreEqual(90.00M, catering.TransactionLog[0].UpdatedBalance);

        }

        [TestMethod]
        public void AddMoneyPopulatesTransactionLog()
        {
            // Arrange 
            Catering catering = new Catering();
            //TxType is "quantityToOrder + item.name + item.type"
            string testTx = "ADD MONEY";

            // Act
            catering.AddMoneyToAccount(100);

            // Assert
            Assert.AreEqual(testTx, catering.TransactionLog[0].TxType);
            Assert.AreEqual(100.00M, catering.TransactionLog[0].TransactionAmount);
            Assert.AreEqual(100.00M, catering.TransactionLog[0].UpdatedBalance);
        }

        [TestMethod]
        public void GiveChangePopulatesTransactionLog()
        {
            // Arrange 
            Catering catering = new Catering();
            //TxType is "quantityToOrder + item.name + item.type"
            string testTx = "GIVE CHANGE";
            catering.AddMoneyToAccount(100);

            // Act
            catering.GiveChange();

            //Transaction index 0 is AddMoneyToAccount
            //Test against index 1 is GiveChange
            // Assert
            Assert.AreEqual(testTx, catering.TransactionLog[1].TxType);
            Assert.AreEqual(100.00M, catering.TransactionLog[1].TransactionAmount);
            Assert.AreEqual(0.00M, catering.TransactionLog[1].UpdatedBalance);
        }
    }
}
