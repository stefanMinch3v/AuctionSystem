using AuctionSystem.Data;
using AuctionSystem.Models;
using AuctionSystem.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    [TestClass]
    public class ZipTest
    {
        private Mock<AuctionContext> db;
        private ZipControllerMock zipController;
        private Mock<DbSet<Zip>> mockSet;

    [TestInitialize]

    public void createData()
    {
        this.db = new Mock<AuctionContext>(); // creates the virtual database

        this.zipController = new ZipControllerMock(this.db.Object);

        var zip = new Zip
        {
            ZipCode = "9",
            Country = "Den",
            City = "Aal"
        };


        var data = new List<Zip> { zip };

        this.mockSet = new Mock<DbSet<Zip>>().SetupData(data); // prepare the virtual zip 

        this.db.Setup(z => z.Zips).Returns(this.mockSet.Object);

    }

    [TestMethod]
        public void CreateZipShouldReturnTrue()
        {

            //Act
            this.zipController.AddZip("9000", "Denmark", "Aalborg");
            var currentZip = this.zipController.GetZipByZipCode("9000");

            //Assert

            Assert.AreEqual("9000", currentZip.ZipCode);
        }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithEmptyZipCodeParametersShouldThrowException()
    {

        this.zipController.AddZip("","Denmark","Aalborg");
        
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithEmptyCountryParametersShouldThrowException()
    {

        this.zipController.AddZip("9000", "", "Aalborg");

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateZipWithEmptyCityParametersShouldThrowException()
    {

        this.zipController.AddZip("9000", "Denmark", "");

    }


}

