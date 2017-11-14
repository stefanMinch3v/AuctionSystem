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

        public void createData()
        {
            this.db = new Mock<AuctionContext>(); // creates the virtual database

            this.invoiceController = new InvoiceControllerMock(this.db.Object);

            var invoice = new Invoice
            {
                ProductId = 1,
                UserId = 2
            };


            var data = new List<Invoice> { invoice };

            this.mockSet = new Mock<DbSet<Invoice>>().SetupData(data); // prepare the virtual invoice 

            this.db.Setup(i => i.Invoices).Returns(this.mockSet.Object);

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
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateInvoiceWithNegativeProductIdShouldThrowException()
        {
            // NEEDS TO BE FINISHED
            invoiceController.CreateInvoice(2, -2);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateInvoiceWithNegativeUserIdShouldThrowException()
        {
            // NEEDS TO BE FINISHED
            invoiceController.CreateInvoice(-2, 2);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateInvoiceWithZeroProductIdShouldThrowException()
        {
            // NEEDS TO BE FINISHED
            invoiceController.CreateInvoice(2, 0);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateInvoiceWithZerouserIdShouldThrowException()
        {
            // NEEDS TO BE FINISHED
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetInvoiceByProductIdWithNegativeIntShouldThrowException()
        { 

            var currentInvoice = this.invoiceController.GetInvoiceByProductId(-1);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetInvoiceByProductIdWithZeroIntShouldThrowException()
        {
           
            var currentInvoice = this.invoiceController.GetInvoiceByProductId(0);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetInvoiceByUserIdWithZeroIntShouldThrowException()
        {


            var currentInvoice = this.invoiceController.GetInvoiceByUserId(0);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetInvoiceByUserIdWithNegativeIntShouldThrowException()
        {
         
            var currentInvoice = this.invoiceController.GetInvoiceByUserId(-1);
        }

        

    }
}
