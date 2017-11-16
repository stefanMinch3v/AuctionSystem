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
            var expected = GetExistingPaymentFromDb().PaymentTypeCode;
            var actual = paymetController.GetPayment("1").PaymentTypeCode;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetPaymentByIdShouldThrowNullReferenceException()
        {
            
            var actual = paymetController.GetPayment("5451").PaymentTypeCode;
            
            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPaymentByIdShouldThrowNullIdException()
        {
            
            var actual = paymetController.GetPayment(null).PaymentTypeCode;
            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPaymentByIdShouldThrowEmptyIdException()
        {

            var actual = paymetController.GetPayment("").PaymentTypeCode;

        }



        [TestMethod]
        public void DeletePaymentShouldRerturnTrue()
        {
            var existingpayment = GetExistingPaymentFromDb().PaymentTypeCode;

            var deletedPayment = this.paymetController.DeletePayment(existingpayment);
            Assert.IsTrue(deletedPayment);
        }

        [TestMethod]
        public void DeletePaymentShouldRerturnFalse()
        {

            var deletedPayment = this.paymetController.DeletePayment("12");
            Assert.IsFalse(deletedPayment);

        }

        
                [TestMethod]
                [ExpectedException(typeof(ArgumentException))]
                public void DeletePaymentShouldThrowNullIdException()
                {

                    var deletedPayment = this.paymetController.DeletePayment(null);

                }

                [TestMethod]
                [ExpectedException(typeof(ArgumentException))]
                public void DeletePaymentShouldThrowEmptyIdException()
                {

                    var deletedPayment = this.paymetController.DeletePayment("");

                }
                
        [TestMethod]
        public void GetPaymentsByUserIdShouldPass()
        {
            var expected = data;
            var actual = this.paymetController.GetPaymentsByUser(1).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPaymentsByUserIdShoudReturnEmptyList()
        {
           
            var actual = this.paymetController.GetPaymentsByUser(2).ToList();
            Assert.IsFalse(actual.Any());
        }





        private Payment CreateWorkingPayment()
        {
            return new Payment
            {
                
                Type = PaymentType.CreditCard,
                PaymentTypeCode = "1",
                User = null,
                UserId = 1
            };
        }

        private Payment GetExistingPaymentFromDb()



        {
            return this.db.Object.Payments.First(p => p.PaymentTypeCode == "1");
        }
            
    }
}
    
