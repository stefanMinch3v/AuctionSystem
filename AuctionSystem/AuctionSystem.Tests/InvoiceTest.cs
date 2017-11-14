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


        [TestMethod]
        public void CreateInvoiceShouldReturnTrue()
        {

            // Arrange
            this.db = new Mock<AuctionContext>(); // creates the virtual database

            this.invoiceController = new InvoiceControllerMock(this.db.Object);

            var invoice = new Invoice
            {
                ProductId = 1,
                UserId = 2
            };


            var data = new List<Invoice> { invoice };

            this.mockSet = new Mock<DbSet<Invoice>>().SetupData(data); // prepare the virtual user 

            this.db.Setup(i => i.Invoices).Returns(this.mockSet.Object);

            //Act

            this.invoiceController.CreateInvoice(5,4);
            var currentInvoice = this.invoiceController.GetInvoiceByProductId(4);

            //Assert

            Assert.AreEqual(4,currentInvoice.ProductId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateInvoiceWithNegativeProductIdShouldThrowException()
        {
            // Arrange
            this.db = new Mock<AuctionContext>(); // creates the virtual database

            this.invoiceController = new InvoiceControllerMock(this.db.Object);


            var invoice = new Invoice
            {
                ProductId = 1,
                UserId = 2
            };

            var data = new List<Invoice> { invoice };

            this.mockSet = new Mock<DbSet<Invoice>>().SetupData(data); // prepare the virtual user 

            this.db.Setup(i => i.Invoices).Returns(this.mockSet.Object);

            invoiceController.CreateInvoice(2,-1);


        }


        [TestMethod]
        public void GetInvoiceByProductIdShouldReturnTrue()
        {
          
            // Arrange
            this.db = new Mock<AuctionContext>(); // creates the virtual database

            this.invoiceController = new InvoiceControllerMock(this.db.Object);


            var invoice = new Invoice
            {
                ProductId = 1,
                UserId = 2
            };

            var data = new List<Invoice> { invoice };

            this.mockSet = new Mock<DbSet<Invoice>>().SetupData(data); // prepare the virtual user 

            this.db.Setup(i => i.Invoices).Returns(this.mockSet.Object);
            
            
            //Act

            var currentInvoice = this.invoiceController.GetInvoiceByProductId(1);
            //Assert

            Assert.AreEqual(1, currentInvoice.ProductId);
        }

        [TestMethod]
        public void GetInvoiceByUserIdShouldReturnTrue()
        {

            // Arrange
            this.db = new Mock<AuctionContext>(); // creates the virtual database

            this.invoiceController = new InvoiceControllerMock(this.db.Object);


            var invoice = new Invoice
            {
                ProductId = 1,
                UserId = 2
            };

            var data = new List<Invoice> { invoice };

            this.mockSet = new Mock<DbSet<Invoice>>().SetupData(data); // prepare the virtual user 

            this.db.Setup(i => i.Invoices).Returns(this.mockSet.Object);


            //Act

            var currentInvoice = this.invoiceController.GetInvoiceByUserId(2);
            //Assert

            Assert.AreEqual(2, currentInvoice.UserId);
        }

       

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetInvoiceByProductIdShouldThrowExceptionNegativeInt()
        {
            // Arrange
            this.db = new Mock<AuctionContext>(); // creates the virtual database

            this.invoiceController = new InvoiceControllerMock(this.db.Object);


            var invoice = new Invoice
            {
                ProductId = 1,
                UserId = 2
            };

            var data = new List<Invoice> { invoice };

            this.mockSet = new Mock<DbSet<Invoice>>().SetupData(data); // prepare the virtual user 

            this.db.Setup(i => i.Invoices).Returns(this.mockSet.Object);

            var currentInvoice = this.invoiceController.GetInvoiceByProductId(-1);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetInvoiceByProductIdShouldThrowExceptionZeroInt()
        {
            // Arrange
            this.db = new Mock<AuctionContext>(); // creates the virtual database

            this.invoiceController = new InvoiceControllerMock(this.db.Object);


            var invoice = new Invoice
            {
                ProductId = 1,
                UserId = 2
            };

            var data = new List<Invoice> { invoice };

            this.mockSet = new Mock<DbSet<Invoice>>().SetupData(data); // prepare the virtual user 

            this.db.Setup(i => i.Invoices).Returns(this.mockSet.Object);

            var currentInvoice = this.invoiceController.GetInvoiceByProductId(0);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetInvoiceByUserIdShouldThrowExceptionZeroInt()
        {
            // Arrange
            this.db = new Mock<AuctionContext>(); // creates the virtual database

            this.invoiceController = new InvoiceControllerMock(this.db.Object);


            var invoice = new Invoice
            {
                ProductId = 1,
                UserId = 2
            };

            var data = new List<Invoice> { invoice };

            this.mockSet = new Mock<DbSet<Invoice>>().SetupData(data); // prepare the virtual user 

            this.db.Setup(i => i.Invoices).Returns(this.mockSet.Object);

            var currentInvoice = this.invoiceController.GetInvoiceByUserId(0);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetInvoiceByUserIdShouldThrowExceptionNegativeInt()
        {
            // Arrange
            this.db = new Mock<AuctionContext>(); // creates the virtual database

            this.invoiceController = new InvoiceControllerMock(this.db.Object);


            var invoice = new Invoice
            {
                ProductId = 1,
                UserId = 2
            };

            var data = new List<Invoice> { invoice };

            this.mockSet = new Mock<DbSet<Invoice>>().SetupData(data); // prepare the virtual user 

            this.db.Setup(i => i.Invoices).Returns(this.mockSet.Object);

            var currentInvoice = this.invoiceController.GetInvoiceByUserId(-1);
        }

        

    }
}
