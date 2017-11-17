namespace AuctionSystem.Tests
{
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
    public class UserTest
    {
        private Mock<AuctionContext> db;
        private UserControllerMock userController;
        private Mock<DbSet<User>> mockSet;
        private List<User> data;
        private List<Payment> paymentData;

        [TestInitialize]
        public void Initialize()
        {
            this.db = new Mock<AuctionContext>();

            this.userController = new UserControllerMock(this.db.Object);

            var user = CreateTestUserWorking();


            this.data = new List<User>() { user };
            paymentData = new List<Payment>();


            this.mockSet = new Mock<DbSet<User>>().SetupData(this.data);

            this.db.Setup(m => m.Users).Returns(mockSet.Object);



        }
        [TestMethod]
        public void getUserByUsernameNameShouldPass()
        {
            var expected = getExistingUserByUserName().Username;
            var actual = userController.GetUserByUsername("John").Username;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CreateUserShouldPass()
        {

            this.userController.CreateUser("name", "pass", "name name", "adress", "email", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip() , 10, paymentData);

            var actual = this.db.Object.Users.First(p => p.Name == "name name");

            Assert.AreEqual("name name", actual.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyUsernameNameArgumentException()
        {

            this.userController.CreateUser("", "pass", "name name", "adress", "email", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullUsernameArgumentException()
        {

            this.userController.CreateUser(null, "pass", "name name", "adress", "email", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyPasswordArgumentException()
        {

            this.userController.CreateUser("name", "", "name name", "adress", "email", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullPasswordArgumentException()
        {

            this.userController.CreateUser("name", null, "name name", "adress", "email", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyNameArgumentException()
        {

            this.userController.CreateUser("name", "pass", "", "adress", "email", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullNameArgumentException()
        {

            this.userController.CreateUser("name", "pass", null, "address", "email", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyAddressArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "", "email", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullAddressArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", null, "email", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyEmailArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "address", "", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullEmailArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "address", null, "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyPhoneArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "address", "email", "", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullPhoneArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "address", "email", null, "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowDateTypeArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "adress", "email", "phone", "57", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowDateEmptyArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "adress", "email", "phone", "", Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowDateNullArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "adress", "email", "phone", null, Models.Enums.Gender.Female, true, createFakeZip(), 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNegativeCoinArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "adress", "email", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), -1, paymentData);
        }

        [TestMethod]
        public void CreateUserShouldNotThrowZeroCoinArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "adress", "email", "phone", "11/15/2017", Models.Enums.Gender.Female, true, createFakeZip(), 0, paymentData);
        }

        [TestMethod]
        public void DeleteUserShouldPass()
        {
            var existingUser = getExistingUserByUserName().Id;

            var deletedUser = this.userController.DeleteUser(existingUser);
            Assert.IsTrue(deletedUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteUserShouldNotPass()
        {
         
            var deletedUser = this.userController.DeleteUser(15);

            Assert.IsFalse(deletedUser);
        }


        public Zip createFakeZip()
        {
            var zip = new Zip
            {
                City = "aalborg",
                ZipCode = "9000",
                Country = "china"

            };
            return zip;
        }
        public User getExistingUserByUserName()
        {
            return this.db.Object.Users.First(u => u.Username == "John");
        }


        public User CreateTestUserWorking()
        {

            return new User
            {
                Id = 1,
                Username = "John",
                Name = "Arafat Khadiri",
                DateOfBirth = DateTime.Now,
                Gender = Models.Enums.Gender.Female,
                Phone = "666999696",
                Email = "John.Cena@gmail.com",
                Address = "Fake street ",
                ZipId = 1,
                Coins = 1,
                IsAdmin = true,
                Password = "banana",
                IsDeleted = false,


            };


        }

    }
}