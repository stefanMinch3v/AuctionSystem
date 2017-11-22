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
            this.paymetController.AddPayment(new Payment { Type = PaymentType.PayPal, PaymentTypeCode = "2ert3", UserId = 2 }, new User { Id = 2 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowNullPaymentTypeCodeException()
        {
            this.paymetController.AddPayment(new Payment { Type = PaymentType.AmazonPayment, PaymentTypeCode = null, UserId = 2 }, new User { Id = 2 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowEmptyPaymentTypeCodeException()
        {
            this.paymetController.AddPayment(new Payment { Type = PaymentType.AmazonPayment, PaymentTypeCode = "", UserId = 2 }, new User { Id = 2 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowZeroUserIdException()
        {
            this.paymetController.AddPayment(new Payment { Type = PaymentType.AmazonPayment, PaymentTypeCode = null, UserId = 0 }, new User { Id = 0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowNegativeUserIdException()
        {
            this.paymetController.AddPayment(new Payment { Type = PaymentType.AmazonPayment, PaymentTypeCode = null, UserId = -1 }, new User { Id = -1 });
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
            var user = GetSomeUser();
            

            var actual = this.paymetController.GetPaymentsByUser(user).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPaymentsByUserIdShoudReturnEmptyList()
        {
            var user = GetSomeUser();

            var actual = this.paymetController.GetPaymentsByUser(user).ToList();
            Assert.IsFalse(actual.Any());
        }
        #endregion

        #region DeleteTests
        [TestMethod]
        public void DeletePaymentShouldRerturnTrue()
        {
            var existingpayment = GetExistingPaymentFromDb();

            var deletedPayment = this.paymetController.DeletePayment(existingpayment);
            Assert.IsTrue(deletedPayment);
        }

        [TestMethod]
        public void DeletePaymentShouldRerturnFalse()
        {
            var unexistingPayment = GetPaymentNotFromDb();

            var deletedPayment = this.paymetController.DeletePayment(unexistingPayment);
            Assert.IsFalse(deletedPayment);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeletePaymentShouldThrowZeroIdException()
        {
            var payment = new Payment { Id = 0 };
            var deletedPayment = this.paymetController.DeletePayment(payment);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeletePaymentShouldThrowNegativeIdException()
        {
            var payment = new Payment { Id = -15 };

            var deletedPayment = this.paymetController.DeletePayment(payment);

        }
        #endregion

        #region UpdateTests
        [TestMethod]
        public void UpdatePaymentTypeCodeShouldReturnTrue()
        {
            // Act

            var currentPayment = GetExistingPaymentFromDb();

            var successUpdating = this.paymetController.UpdatePayment(currentPayment, "PaymentTypeCode", "New Type");

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowNoSuchPropertyException()
        {
            var currentPayment = GetExistingPaymentFromDb();
            var successUpdating = this.paymetController.UpdatePayment(currentPayment, "nonexisting exception", "New Type");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowZeroIdException()
        {
            var currentPayment = GetExistingPaymentFromDb();
            currentPayment.Id = 0;

            var successUpdating = this.paymetController.UpdatePayment(currentPayment, "nonexisting exception", "New Type");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowNegativeIdException()
        {
            var currentPayment = GetExistingPaymentFromDb();
            currentPayment.Id = -5;

            var successUpdating = this.paymetController.UpdatePayment(currentPayment, "nonexisting exception", "New Type");

        }

        [TestMethod]
        public void UpdatePaymentTypeCodeShouldPassIfTypeIsChanged()
        {

            var currentPayment = GetExistingPaymentFromDb();

            var successUpdating = this.paymetController.UpdatePayment(currentPayment, "PaymentTypeCode", "Random paymenttype");

            var changedPaymentName = this.db.Object.Payments.FirstOrDefault(p => p.Id == currentPayment.Id).PaymentTypeCode;

            // Assert

            Assert.AreEqual("Random paymenttype", changedPaymentName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowExceptionIfTypeIsNull()
        {
            var currentPayment = GetExistingPaymentFromDb();

            var successUpdating = this.paymetController.UpdatePayment(currentPayment, "PaymentTypeCode", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowExceptionIfTypeIsEmpty()
        {
            var currentPayment = GetExistingPaymentFromDb();

            var successUpdating = this.paymetController.UpdatePayment(currentPayment, "PaymentTypeCode", "");
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

        private Payment GetPaymentNotFromDb()
        {
            return new Payment
            {
                Id = 22,
                Type = PaymentType.CreditCard,
                PaymentTypeCode = "sdf31",
                User = null,
                UserId = 1
            };
        }

        private User GetSomeUser()
        {
            return new User
            {
                Name = "name",
                Password = "pass",
                Username = "name name",
                Address = "address",
                Email = "email",
                Phone = "phone",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Gender = Gender.Female,
                ZipId = 1,
                Coins = 10,
                Payments = this.data
            };
        }

        private Payment GetExistingPaymentFromDb()
        {
            return this.db.Object.Payments.First(p => p.Id == 1);
        }

        #endregion

    }
}

