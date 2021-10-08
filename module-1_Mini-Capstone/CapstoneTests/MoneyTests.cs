using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class MoneyTests
    {
        

        [TestMethod]
        public void CheckBalanceIsNotNull()
        {
            //Arrange
            Money money = new Money();

            //Act
            decimal result = money.CheckBalance();

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void AddMoneyChangesAccountBalance()
        {
            //Arrange
            Money money = new Money();

            //Act
            bool moneyAdds = money.AddMoney(500);
            decimal result = money.CheckBalance();

            //Assert
            Assert.IsTrue(moneyAdds);
            Assert.AreEqual(500.00M, result);
        }

        [TestMethod]
        [DataRow(4201, "0", false)]
        [DataRow(-200, "0", false)]
        [DataRow(0, "0", true)]
        public void AddMoneyEdgeCases(int moneyToAdd, string newBalance, bool expectedBool)
        {
            //Arrange
            Money money = new Money();
            decimal expectedBalance = decimal.Parse(newBalance);

            //Act
            bool succeeded = money.AddMoney(moneyToAdd);
            decimal result = money.CheckBalance();

            //Assert
            Assert.AreEqual(expectedBool, succeeded);
            Assert.AreEqual(expectedBalance, result);
        }

        [TestMethod]
        public void AddMoneyTwiceSucceeds()
        {
            //Arrange
            Money money = new Money();
            money.AddMoney(100);

            //Act
            money.AddMoney(100);
            decimal result = money.CheckBalance();

            //Assert
            Assert.AreEqual(200.00M, result);
        }

        

        [TestMethod]
        public void RemoveMoneyDecreasesBalance()
        {
            //Arrange
            Money money = new Money();
            money.AddMoney(200);

            //Act
            bool succeeds = money.RemoveMoney(100);
            decimal result = money.CheckBalance();

            //Assert
            Assert.IsTrue(succeeds);
            Assert.AreEqual(100.00M, result);
        }

        [TestMethod]
        [DataRow(100, "0", true)]
        [DataRow(101, "100", false)]
        public void RemoveMoneyEdgeCases(int moneyToRemove, string newBalance, bool expectedBool)
        {
            //Arrange
            Money money = new Money();
            money.AddMoney(100);
            decimal expectedBalance = decimal.Parse(newBalance);

            //Act
            bool succeeds = money.RemoveMoney(moneyToRemove);
            decimal result = money.CheckBalance();

            //Assert
            Assert.AreEqual(expectedBool, succeeds);
            Assert.AreEqual(expectedBalance, result);
        }

        [TestMethod]
        [DataRow(150, "0.00", "Change due: 7 Twentys 1 Tens ")]
        [DataRow(40, "3.60", "Change due: 1 Twentys 1 Tens 1 Fives 1 Ones 1 Quarters 1 Dimes 1 Nickels")]
        public void ReturnMoneyMakesCorrectChange(int moneyInAccount, string accountAdjust, string expected)
        {
            //Arrange
            Money money = new Money();
            decimal moneyToAdjust = decimal.Parse(accountAdjust);

            //Act
            money.AddMoney(moneyInAccount);
            money.RemoveMoney(moneyToAdjust);
            string result = money.ReturnMoney();

            //Assert
            Assert.AreEqual(expected, result);

        }
    }
}
