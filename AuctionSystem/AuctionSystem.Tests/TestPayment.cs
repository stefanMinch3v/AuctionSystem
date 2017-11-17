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
#region CreateTests
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
        #endregion
#region GetTests
        [TestMethod]
        public void GetPaymentByIdShouldPass()
        {
            var actual = this.paymetController.GetPayment(1);
            var expected = this.db.Object.Payments.FirstOrDefault(p => p.Id == 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        public void GetPaymentByIdShouldThrowNullReferenceException()
        {
            
            var actual = paymetController.GetPayment(5454);
            Assert.IsNull(actual);
 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPaymentByIdShouldThrowZeroIdException()
        {
            
            var actual = paymetController.GetPayment(0).PaymentTypeCode;
            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPaymentByIdShouldThrowNegativeIdException()
        {

            var actual = paymetController.GetPayment(-1).PaymentTypeCode;

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
        #endregion
#region DeleteTests
        [TestMethod]
        public void DeletePaymentShouldRerturnTrue()
        {
            var existingpayment = GetExistingPaymentFromDb().Id;

            var deletedPayment = this.paymetController.DeletePayment(existingpayment);
            Assert.IsTrue(deletedPayment);
        }

        [TestMethod]
        public void DeletePaymentShouldRerturnFalse()
        {

            var deletedPayment = this.paymetController.DeletePayment(15);
            Assert.IsFalse(deletedPayment);

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeletePaymentShouldThrowZeroIdException()
        {

            var deletedPayment = this.paymetController.DeletePayment(0);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeletePaymentShouldThrowNegativeIdException()
        {

            var deletedPayment = this.paymetController.DeletePayment(-15);

        }
        #endregion
#region UpdateTests
        [TestMethod]
        public void UpdatePaymentTypeCodeShouldReturnTrue()
        {
            // Act

            var currentPaymentId = GetExistingPaymentFromDb().Id;

            var successUpdating = this.paymetController.UpdatePayment(currentPaymentId, "PaymentTypeCode", "New Type");

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowNoSuchPropertyException()
        {
 
            var successUpdating = this.paymetController.UpdatePayment(1, "nonexisting exception", "New Type");
  
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowZeroIdException()
        {
            
            var successUpdating = this.paymetController.UpdatePayment(0, "nonexisting exception", "New Type");
 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowNegativeIdException()
        {

            var successUpdating = this.paymetController.UpdatePayment(-5, "nonexisting exception", "New Type");

        }

        [TestMethod]
        public void UpdatePaymentTypeCodeShouldPassIfTypeIsChanged()
        {

            var currentPaymentId = GetExistingPaymentFromDb().Id;

            var successUpdating = this.paymetController.UpdatePayment(currentPaymentId, "PaymentTypeCode", "Random paymenttype");

            var changedPaymentName = this.db.Object.Payments.FirstOrDefault(p => p.Id == currentPaymentId).PaymentTypeCode;

            // Assert

            Assert.AreEqual("Random paymenttype", changedPaymentName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowExceptionIfTypeIsNull()
        {
            var currentPaymentId = GetExistingPaymentFromDb().Id;

            var successUpdating = this.paymetController.UpdatePayment(currentPaymentId, "PaymentTypeCode", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowExceptionIfTypeIsEmpty()
        {
            var currentPaymentId = GetExistingPaymentFromDb().Id;

            var successUpdating = this.paymetController.UpdatePayment(currentPaymentId, "PaymentTypeCode", "");
        }

        #endregion


#region FakeDataAndMethods
        private Payment CreateWorkingPayment()
        {
            return new Payment
            {
                Id = 1,
                Type = PaymentType.CreditCard,
                PaymentTypeCode = "1",
                User = null,
                UserId = 1
            };
        }

        private Payment GetExistingPaymentFromDb()
        {
            return this.db.Object.Payments.First(p => p.Id == 1);
        }

#endregion

    }
}
    
