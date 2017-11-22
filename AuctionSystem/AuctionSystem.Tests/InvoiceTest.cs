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
        private List<Invoice> data;

        public List<Invoice> Data { get => data; set => data = value; }

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
                UserId = 2
                
            };

            Data = new List<Invoice> { invoice,invoice2 }; //insert in collection

            this.mockSet = new Mock<DbSet<Invoice>>().SetupData(Data); //creates fake table mockSet of Products and insert the data

            this.db.Setup(i => i.Invoices).Returns(this.mockSet.Object); // attach the table into the database and returns it as an object ready to be used 

        }

        //CREATE INVOICE
        
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
            
            invoiceController.CreateInvoice(-2, 2);

        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvoiceWithZeroProductIdShouldThrowException()
        {
           
            invoiceController.CreateInvoice(2, 0);

        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvoiceWithZeroUserIdShouldThrowException()
        {
            // Act
            invoiceController.CreateInvoice(0, 2);

        }

       
        [TestMethod]
        public void CreateInvoiceWithNonExistingUserShouldThrowException()
        {
            // Act
            var invoice =this.invoiceController.GetInvoiceByUserId(99999);
            //Assert
            Assert.IsNull(invoice);

        }


        
       [TestMethod]
       public void CreateInvoiceWithNonExistingProductShouldThrowException()
       {
           // Act
           var invoice = this.invoiceController.GetInvoiceByProductId(99999);
           //Assert
           Assert.IsNull(invoice);

       }

       //GET INVOICE BY PRODUCT ID

       [TestMethod]
       public void GetInvoiceByProductIdShouldReturnTrue()
       {
           //Act

           var currentInvoice = this.invoiceController.GetInvoiceByProductId(1);
           //Assert

           Assert.AreEqual(1, currentInvoice.ProductId);
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

       //GET INVOICE BY USER ID

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

        
       [TestMethod]
       public void GetInvoiceByUserIdWithNonExsistingUserShouldReturnNull()
       {
           //Act

           var currentInvoice = this.invoiceController.GetInvoiceByUserId(99999);
           //Assert
           Assert.IsNull(currentInvoice);
       }

       //GET ALL INVOICES FOR USER
        
       [TestMethod] 
       public void GetAllInvoicesForUserShouldReturnTrue()
       {
           
           
           var invoices = this.invoiceController.GetAllInvoicesForUser(2).ToList();

           // Assert

           CollectionAssert.AreEqual(Data,invoices);
       }

        
        
       [TestMethod]
       [ExpectedException(typeof(ArgumentException))]
       public void GetAllInvoicesForUserWithNegativeIdShouldThrowException()
       {
           //Act
           var invoices = this.invoiceController.GetAllInvoicesForUser(-1).ToList();
       }
       
        
       [TestMethod]
       [ExpectedException(typeof(ArgumentException))]
       public void GetAllInvoicesForUserWithZeroIdShouldThrowException()
       {
           //Act
           var invoices = this.invoiceController.GetAllInvoicesForUser(0).ToList();
       }
       
        
        [TestMethod]
        
        public void GetAllInvoicesForUserWithNonExistingUserIdShouldReturnNull()
      {
            //Act
          var invoices = invoiceController.GetAllInvoicesForUser(99999).ToList();
            //Assert
            Assert.AreEqual(0, invoices.Count);

        }
      
    }
}
