using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class FileInputTests
    {
        [TestMethod]
        public void LoadProductFromFileSetsDictionary()
        {
            //Arrange
            //A populated CateringSystem.csv must exist in C:\Catering
            Catering catering = new Catering();
            FileInput fileInput = new FileInput();

            //Act
            fileInput.LoadProductMenuFromFile(catering.productMenu);

            //Assert
            Assert.IsNotNull(catering.productMenu);
        }
        //If we couldn't test the files, we would change the input and output paths
        //to take in a parameter that we would provide to create a test environment
        //Also applies to Class FileOutput
    }
}
