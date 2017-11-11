namespace AuctionSystem.Tests
{
    using AuctionSystem.Tests.Mocks;
    using Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;
    using System.Collections.Generic;
    using System.Data.Entity;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
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
            var data = new List<User>
            {
                new User() { Username = "Pesho"}
            };

            var mockSet = new Mock<DbSet<User>>().SetupData(data);
            var db = new Mock<AuctionContext>();

            db.Setup(m => m.Users).Returns(mockSet.Object);

            var userController = new UserControllerMock(db.Object);

            var userObject = userController.GetUserByUsername("Pesho");

            Assert.AreEqual("Pesho", userObject.Username);
        }
    }
}
