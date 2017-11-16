namespace AuctionSystem.Tests
{
    using AuctionSystem.Controllers;
    using AuctionSystem.Models.Enums;
    using Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Models;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    [TestClass]
    public class TestPayment
    {
        private Mock<AuctionContext> db;
        private PaymentControllerMock paymetController;
        private Mock<DbSet<Payment>> mockSet;
        private List<Payment> data;

        [TestInitialize]
        public void Initialize()
        {
            this.db = new Mock<AuctionContext>();
            this.paymetController = new PaymentControllerMock(this.db.Object);

            var workingPayment = CreateWorkingPayment();

            this.data = new List<Payment>() { workingPayment };

            this.mockSet = new Mock<DbSet<Payment>>().SetupData(this.data);

            this.db.Setup(m => m.Payments).Returns(mockSet.Object);
        }

        [TestMethod]
        public void CreateWorkingPaymentShouldPass()
        {
            this.paymetController.AddPayment(PaymentType.AmazonPayment, "1", 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowNullPaymentTypeCodeException()
        {
            this.paymetController.AddPayment(PaymentType.AmazonPayment, null, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowEmptyPaymentTypeCodeException()
        {
            this.paymetController.AddPayment(PaymentType.AmazonPayment,"" , 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowZeroUserIdException()
        {
            this.paymetController.AddPayment(PaymentType.AmazonPayment, "1", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowNegativeUserIdException()
        {
            this.paymetController.AddPayment(PaymentType.AmazonPayment, "1", -1);
        }

        [TestMethod]
        public void GetPaymentByIdShouldPass()
        {
            var expected = getExistingPaymentFromDb().Id;
            var actual = paymetController.GetPaymentById(1).Id;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void deletePaymentShouldRerturnTrue()
        {
            var existingpayment = getExistingPaymentFromDb().Id;

            var deletedPayment = this.paymetController.DeletePayment(existingpayment);
            Assert.IsTrue(deletedPayment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
          public void deletePaymentShouldRerturnFalse()
        {
        
            var deletedPayment = this.paymetController.DeletePayment(12);
            
        }
        [TestMethod]
        public void getPaymentsByUserIdShouldPass()
        {
            var expected = data;
            var actual = this.paymetController.GetPaymentsByUser(1).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void getPaymentsByUserIdShoudThrowException()
        {
            var expected = data;
            var actual = this.paymetController.GetPaymentsByUser(2).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }





        private Payment CreateWorkingPayment()
        {
            return new Payment
            {
                Id = 1,
                Type = PaymentType.CreditCard,
                PaymentTypeCode = "bank",
                User = null,
                UserId = 1
            };
        }

        private Payment getExistingPaymentFromDb()



        {
            return this.db.Object.Payments.First(p => p.Id == 1);
        }
            
    }
}
    
