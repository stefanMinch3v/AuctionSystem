namespace AuctionSystem.Tests
{
    using AuctionSystem.Tests.Mocks;
    using Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    [TestClass]
    public class InvoiceTest
    {
        private Mock<AuctionContext> db;
        private InvoiceControllerMock invoiceController;
        private Mock<DbSet<Invoice>> mockSet;

        [TestInitialize]

        public void Initialize()
        {
            this.db = new Mock<AuctionContext>(); // creates the virtual database

            this.invoiceController = new InvoiceControllerMock(this.db.Object); //creates controller to test

            var invoice = new Invoice
            {
                ProductId = 1,
                UserId = 2
            };

            var invoice2 = new Invoice
            {
                ProductId = 5,
                UserId = 6
            };

            var data = new List<Invoice> { invoice,invoice2 }; //insert in collection

            this.mockSet = new Mock<DbSet<Invoice>>().SetupData(data); //creates fake table mockSet of Products and insert the data

            this.db.Setup(i => i.Invoices).Returns(this.mockSet.Object); // attach the table into the database and returns it as an object ready to be used 

        }

        [TestMethod]
        public void CreateInvoiceShouldReturnTrue()
        { 

            //Act

            this.invoiceController.CreateInvoice(5,4);
            var currentInvoice = this.invoiceController.GetInvoiceByProductId(4);

            //Assert

            Assert.AreEqual(4,currentInvoice.ProductId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvoiceWithNegativeProductIdShouldThrowException()
        {
            // Act
            this.invoiceController.CreateInvoice(2, -2);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvoiceWithNegativeUserIdShouldThrowException()
        {
            // NEEDS TO BE FINISHED
            invoiceController.CreateInvoice(-2, 2);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvoiceWithZeroProductIdShouldThrowException()
        {
            // NEEDS TO BE FINISHED
            invoiceController.CreateInvoice(2, 0);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvoiceWithZerouserIdShouldThrowException()
        {
            // Act
            invoiceController.CreateInvoice(0, 2);

        }
        

        [TestMethod]
        public void GetInvoiceByProductIdShouldReturnTrue()
        {
            //Act

            var currentInvoice = this.invoiceController.GetInvoiceByProductId(1);
            //Assert

            Assert.AreEqual(1, currentInvoice.ProductId);
        }

        [TestMethod]
        public void GetInvoiceByUserIdShouldReturnTrue()
        {

            //Act

            var currentInvoice = this.invoiceController.GetInvoiceByUserId(2);
            //Assert

            Assert.AreEqual(2, currentInvoice.UserId);
        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInvoiceByProductIdWithNegativeIntShouldThrowException()
        { 
            // Act

            var currentInvoice = this.invoiceController.GetInvoiceByProductId(-1);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInvoiceByProductIdWithZeroIntShouldThrowException()
        {
           // Act

            var currentInvoice = this.invoiceController.GetInvoiceByProductId(0);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInvoiceByUserIdWithZeroIntShouldThrowException()
        {

            // Act

            var currentInvoice = this.invoiceController.GetInvoiceByUserId(0);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInvoiceByUserIdWithNegativeIntShouldThrowException()
        {
         
            var currentInvoice = this.invoiceController.GetInvoiceByUserId(-1);
        }

        public void GetAllInvoicesForUserShouldReturnTrue()
        {
            var invoices = this.invoiceController.GetAllInvoicesForUser(data);
        }

    }
}
