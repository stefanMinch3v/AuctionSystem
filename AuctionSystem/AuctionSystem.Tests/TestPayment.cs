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
        private Mock<DbSet<User>> mockSet2;
        private List<Payment> data;
        private List<User> data2;

        [TestInitialize]
        public void Initialize()
        {
            this.db = new Mock<AuctionContext>();
            this.paymetController = new PaymentControllerMock(this.db.Object);

            this.data = new List<Payment>() { CreateWorkingPayment() };
            this.data2 = new List<User> { GetSomeUserWithPayments() };

            this.mockSet = new Mock<DbSet<Payment>>().SetupData(this.data);
            this.mockSet2 = new Mock<DbSet<User>>().SetupData(this.data2);

            this.db.Setup(m => m.Payments).Returns(mockSet.Object);
            this.db.Setup(m => m.Users).Returns(mockSet2.Object);
        }

        #region CreateTests
        [TestMethod]
        public void CreateWorkingPaymentShouldPass()
        {
            this.paymetController.AddPayment(new Payment { Type = PaymentType.PayPal, PaymentTypeCode = "2ert3", UserId = 2 }, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowNullPaymentTypeCodeException()
        {
            this.paymetController.AddPayment(new Payment { Type = PaymentType.AmazonPayment, PaymentTypeCode = null, UserId = 2 }, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowEmptyPaymentTypeCodeException()
        {
            this.paymetController.AddPayment(new Payment { Type = PaymentType.AmazonPayment, PaymentTypeCode = "", UserId = 2 }, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowZeroUserIdException()
        {
            this.paymetController.AddPayment(new Payment { Type = PaymentType.AmazonPayment, PaymentTypeCode = null, UserId = 0 }, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePaymentShouldThrowNegativeUserIdException()
        {
            this.paymetController.AddPayment(new Payment { Type = PaymentType.AmazonPayment, PaymentTypeCode = null, UserId = -1 }, 2);
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
            var user = GetSomeUserWithPayments();
            

            var actual = this.paymetController.GetPaymentsByUser(user).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPaymentsByUserIdShoudReturnEmptyList()
        {
            var user = GetSomeUserWithoutPayments();

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
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeletePaymentShouldThrowException()
        {
            var unexistingPayment = GetPaymentNotFromDb();

            var deletedPayment = this.paymetController.DeletePayment(unexistingPayment);
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
            currentPayment.PaymentTypeCode = "New Type";

            var successUpdating = this.paymetController.UpdatePayment(currentPayment);

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowZeroIdException()
        {
            var currentPayment = GetExistingPaymentFromDb();
            currentPayment.Id = 0;

            var successUpdating = this.paymetController.UpdatePayment(currentPayment);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowNegativeIdException()
        {
            var currentPayment = GetExistingPaymentFromDb();
            currentPayment.Id = -5;

            var successUpdating = this.paymetController.UpdatePayment(currentPayment);

        }

        [TestMethod]
        public void UpdatePaymentTypeCodeShouldPassIfTypeIsChanged()
        {

            var currentPayment = GetExistingPaymentFromDb();
            currentPayment.PaymentTypeCode = "Random paymenttype";

            var successUpdating = this.paymetController.UpdatePayment(currentPayment);

            var changedPaymentName = this.db.Object.Payments.FirstOrDefault(p => p.Id == currentPayment.Id).PaymentTypeCode;

            // Assert

            Assert.AreEqual("Random paymenttype", changedPaymentName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowExceptionIfTypeIsNull()
        {
            var currentPayment = GetExistingPaymentFromDb();
            currentPayment.PaymentTypeCode = null;

            var successUpdating = this.paymetController.UpdatePayment(currentPayment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePaymentTypeCodeShouldThrowExceptionIfTypeIsEmpty()
        {
            var currentPayment = GetExistingPaymentFromDb();
            currentPayment.PaymentTypeCode = "";

            var successUpdating = this.paymetController.UpdatePayment(currentPayment);
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
                Id = 212,
                Type = PaymentType.CreditCard,
                PaymentTypeCode = "sdf31",
                UserId = 1
            };
        }

        private User GetSomeUserWithPayments()
        {
            return new User
            {
                Id = 1,
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

        private User GetSomeUserWithoutPayments()
        {
            return new User
            {
                Id = 2,
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
            };
        }

        private Payment GetExistingPaymentFromDb()
        {
            return this.db.Object.Payments.First(p => p.Id == 1);
        }

        #endregion

    }
}

