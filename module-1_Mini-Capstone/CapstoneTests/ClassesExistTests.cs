using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class ClassesExistTests
    {
        [TestMethod]
        public void ClassesBuild()
        {
            //Arrange
            Catering catering = new Catering();
            CateringItem cateringItem = new CateringItem("type", "id", "name", 0.00M);
            FileInput fileInput = new FileInput();
            FileOutput fileOutput = new FileOutput();
            Money money = new Money();
            Transaction transaction = new Transaction("type", 0.00M);
            UserInterface userInterface = new UserInterface();

            //Act

            //Assert
            Assert.IsNotNull(catering);
            Assert.IsNotNull(cateringItem);
            Assert.IsNotNull(fileInput);
            Assert.IsNotNull(fileOutput);
            Assert.IsNotNull(money);
            Assert.IsNotNull(transaction);
            Assert.IsNotNull(userInterface);
        }
    }
}
