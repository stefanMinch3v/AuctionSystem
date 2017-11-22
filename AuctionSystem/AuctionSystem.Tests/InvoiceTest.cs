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

        public List<Invoice> Data
        {
            get => data;
            set => data = value;
        }

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

            Data = new List<Invoice> {invoice, invoice2}; //insert in collection

            this.mockSet =
                new Mock<DbSet<Invoice>>().SetupData(Data); //creates fake table mockSet of Products and insert the data

            this.db.Setup(i => i.Invoices)
                .Returns(this.mockSet
                    .Object); // attach the table into the database and returns it as an object ready to be used 
        }

        //CREATE INVOICE

        [TestMethod]
        public void CreateInvoiceShouldReturnTrue()
        {
            //Act
            var user = new User {Id = 5};
            var product = new Product {Id = 4};

            this.invoiceController.CreateInvoice(user, product);
            var currentInvoice = this.invoiceController.GetInvoiceByProductId(product);

            //Assert

            Assert.AreEqual(4, currentInvoice.ProductId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvoiceWithNegativeProductIdShouldThrowException()
        {
            // Act
            this.invoiceController.CreateInvoice(new User {Id = 2}, new Product {Id = -2});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvoiceWithNegativeUserIdShouldThrowException()
        {
            invoiceController.CreateInvoice(new User {Id = -2}, new Product {Id = 2});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvoiceWithZeroProductIdShouldThrowException()
        {
            invoiceController.CreateInvoice(new User {Id = 2}, new Product {Id = 0});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvoiceWithZeroUserIdShouldThrowException()
        {
            // Act
            invoiceController.CreateInvoice(new User{Id = 0}, new Product{Id =2 });
        }


        [TestMethod]
        public void CreateInvoiceWithNonExistingUserShouldThrowException()
        {
            // Act
            var invoice = this.invoiceController.GetInvoiceByUserId(new User{Id = Int32.MaxValue});
            //Assert
            Assert.IsNull(invoice);
        }


        [TestMethod]
        public void CreateInvoiceWithNonExistingProductShouldThrowException()
        {
            // Act
            var invoice = this.invoiceController.GetInvoiceByProductId(new Product{Id = Int32.MaxValue});
            //Assert
            Assert.IsNull(invoice);
        }

        //GET INVOICE BY PRODUCT ID

        [TestMethod]
        public void GetInvoiceByProductIdShouldReturnTrue()
        {
            //Act

            var currentInvoice = this.invoiceController.GetInvoiceByProductId(new Product{Id = 1});
            //Assert

            Assert.AreEqual(1, currentInvoice.ProductId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInvoiceByProductIdWithNegativeIntShouldThrowException()
        {
            // Act

            var currentInvoice = this.invoiceController.GetInvoiceByProductId(new Product{Id = -1});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInvoiceByProductIdWithZeroIntShouldThrowException()
        {
            // Act

            var currentInvoice = this.invoiceController.GetInvoiceByProductId(new Product{Id = 0});
        }

        //GET INVOICE BY USER ID

        [TestMethod]
        public void GetInvoiceByUserIdShouldReturnTrue()
        {
            //Act

            var currentInvoice = this.invoiceController.GetInvoiceByUserId(new User{Id = 2});
            //Assert

            Assert.AreEqual(2, currentInvoice.UserId);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInvoiceByUserIdWithZeroIntShouldThrowException()
        {
            // Act

            var currentInvoice = this.invoiceController.GetInvoiceByUserId(new User{Id = 0});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInvoiceByUserIdWithNegativeIntShouldThrowException()
        {
            var currentInvoice = this.invoiceController.GetInvoiceByUserId(new User{Id = -1});
        }


        [TestMethod]
        public void GetInvoiceByUserIdWithNonExsistingUserShouldReturnNull()
        {
            //Act

            var currentInvoice = this.invoiceController.GetInvoiceByUserId(new User{Id = Int32.MaxValue});
            //Assert
            Assert.IsNull(currentInvoice);
        }

        //GET ALL INVOICES FOR USER

        [TestMethod]
        public void GetAllInvoicesForUserShouldReturnTrue()
        {
            var invoices = this.invoiceController.GetAllInvoicesForUser(new User{Id = 2}).ToList();

            // Assert

            CollectionAssert.AreEqual(Data, invoices);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAllInvoicesForUserWithNegativeIdShouldThrowException()
        {
            //Act
            var invoices = this.invoiceController.GetAllInvoicesForUser(new User{Id = -1}).ToList();
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAllInvoicesForUserWithZeroIdShouldThrowException()
        {
            //Act
            var invoices = this.invoiceController.GetAllInvoicesForUser(new User{Id = 0}).ToList();
        }


        [TestMethod]
        public void GetAllInvoicesForUserWithNonExistingUserIdShouldReturnNull()
        {
            //Act
            var invoices = invoiceController.GetAllInvoicesForUser(new User{Id = Int32.MaxValue}).ToList();
            //Assert
            Assert.AreEqual(0, invoices.Count);
        }
    }
}