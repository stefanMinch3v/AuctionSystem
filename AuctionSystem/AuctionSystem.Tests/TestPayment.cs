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
        // TODO
        // Tips for mocking tests

        // Arrange, Act, Assert;

        // var mockSet = new Mock<DbSet<User>>(); // prepare the virtual user table 

        // var mockContext = new Mock<AuctionContext>(); // creates the virtual database
        // mockContext.Setup(m => m.Users).Returns(mockSet.Object); // creates the table users

        // var userController = new UserControllerMock(); // our mock implementation of the controller with fake data
        // userController. some method

        // mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once()); // to check if the user it added only once
        // mockContext.Verify(m => m.SaveChanges(), Times.Once()); // save changes only once
        // it can be used mockSet.Verify(m => m.Add(It.Is<User>(u => u.Age == 20))) // this will throw an exception if the current user's age that you're trying to add is != 20

        // Simple example create user
        [TestInitialize]
        public void Initialize()
        {
            this.db = new Mock<AuctionContext>();
            this.paymetController = new PaymentControllerMock(this.db.Object);
     
            var workingPayment = GetWorkingPayment();
        }
        [TestMethod]
        public void GetPaymentByIdShouldŃotPass()
        {
            var expected = getExistingPaymentFromDb().Id;
            var actual = paymetController.GetPaymentById(1);
            Assert.AreEqual(expected, actual);
        }


        private Payment GetWorkingPayment()
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
    
