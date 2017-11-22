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
        private Mock<DbSet<Zip>> mockSet2;
        private List<User> userData;
        private List<Zip> zipData;
        private List<Bid> bidData;
        private List<Payment> paymentData;

        [TestInitialize]
        public void Initialize()
        {
            this.db = new Mock<AuctionContext>();

            this.userController = new UserControllerMock(this.db.Object);

            var user = GetUserCreatedInDb();
            var bid = CreateTestBid();
            var zip = CreateFakeZip();

            this.userData = new List<User>() { user };

            paymentData = new List<Payment>();

            this.bidData = new List<Bid>() { bid };
            this.zipData = new List<Zip>() { zip };

            this.mockSet = new Mock<DbSet<User>>().SetupData(this.userData);
            this.mockSet2 = new Mock<DbSet<Zip>>().SetupData(this.zipData);

            this.db.Setup(m => m.Users).Returns(mockSet.Object);
            this.db.Setup(m => m.Zips).Returns(mockSet2.Object);



        }

        //GET USER BY USERNAME

        [TestMethod]
        public void getUserByUsernameNameShouldPass()
        {
            var expected = getExistingUserByUserName().Username;
            var actual = userController.GetUserByUsername("John").Username;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void getUserByUsernameNameWithNullShouldThrowException()
        {
            var actual = userController.GetUserByUsername(null).Username;

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void getUserByUsernameNameWithEmptyShouldThrowException()
        {
            var actual = userController.GetUserByUsername("").Username;

        }

        //GET USER BY ID

        [TestMethod]
        public void getUserByIdShouldPass()
        {
            var expected = GetExistingUserById().Id;
            var actual = userController.GetUserById(1).Id;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void getUserByIdWithNegativeIdShouldThrowException()
        {
            var actual = userController.GetUserById(-1).Id;

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void getUserByIdWithZeroIdShouldThrowException()
        {
            var actual = userController.GetUserById(0).Id;

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void getUserByIdWithNonExistingIdShouldThrowException()
        {
            var actual = userController.GetUserById(15).Id;

        }

        //CREATE USER 


        [TestMethod]
        public void CreateUserShouldPass()
        {
            var user = GetUserNotFromDb();
            this.userController.CreateUser(user);

            var actual = this.db.Object.Users.First(p => p.Name == "Pesho");

            Assert.AreEqual("Pesho", actual.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyUsernameNameArgumentException()
        {

            var user = GetUserNotFromDb();
            user.Username = "";

            this.userController.CreateUser(user);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullUsernameArgumentException()
        {
            var user = GetUserNotFromDb();
            user.Username = null;

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyPasswordArgumentException()
        {
            var user = GetUserNotFromDb();
            user.Password = "";

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullPasswordArgumentException()
        {

            var user = GetUserNotFromDb();
            user.Password = null;

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyNameArgumentException()
        {

            var user = GetUserNotFromDb();
            user.Name = "";

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullNameArgumentException()
        {

            var user = GetUserNotFromDb();
            user.Name = null;

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyAddressArgumentException()
        {

            var user = GetUserNotFromDb();
            user.Address = "";

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullAddressArgumentException()
        {

            var user = GetUserNotFromDb();
            user.Address = null;

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyEmailArgumentException()
        {

            var user = GetUserNotFromDb();
            user.Email = "";

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullEmailArgumentException()
        {

            var user = GetUserNotFromDb();
            user.Email = null;

            this.userController.CreateUser(user);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyPhoneArgumentException()
        {

            var user = GetUserNotFromDb();
            user.Phone = "";

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullPhoneArgumentException()
        {

            var user = GetUserNotFromDb();
            user.Phone = null;

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CreateUserShouldThrowDateTypeArgumentException()
        {

            var user = GetUserNotFromDb();
            user.DateOfBirth = DateTime.Parse("55");

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CreateUserShouldThrowDateEmptyArgumentException()
        {

            var user = GetUserNotFromDb();
            user.DateOfBirth = DateTime.Parse("");

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateUserShouldThrowDateNullArgumentException()
        {

            var user = GetUserNotFromDb();
            user.DateOfBirth = DateTime.Parse(null);

            this.userController.CreateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNegativeCoinArgumentException()
        {

            var user = GetUserNotFromDb();
            user.Coins = -1;

            this.userController.CreateUser(user);
        }

        //DELETE USER

        [TestMethod]
        public void DeleteUserShouldPass()
        {
            var existingUser = getExistingUserByUserName();

            var deletedUser = this.userController.DeleteUser(existingUser);
            Assert.IsTrue(deletedUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteUserWithNonExistingIdShouldThrowException()
        {
            var user = GetUserNotFromDb();

            var deletedUser = this.userController.DeleteUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteUserWithNegativeIdShoulThrowException()
        {
            var user = GetUserNotFromDb();
            user.Id = -1;

            var deletedUser = this.userController.DeleteUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteUserWithZeroIdShoulThrowException()
        {
            var user = GetUserNotFromDb();
            user.Id = 0;

            var deletedUser = this.userController.DeleteUser(user);
        }

        //IS USER EXISTING

        [TestMethod]
        public void IsUserExistingShouldPass()
        {
            var user = GetUserCreatedInDb();

            var existingUser = this.userController.IsUserExisting(user);

            Assert.IsTrue(existingUser);
        }

        [TestMethod]
        public void IsUserExistingShouldNotPass()
        {
            var user = GetUserNotFromDb();

            var existingUser = this.userController.IsUserExisting(user);

            Assert.IsFalse(existingUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUserExistingWithNullUserNameShouldThrowException()
        {
            var user = GetUserNotFromDb();
            user.Name = null;

            var existingUser = this.userController.IsUserExisting(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUserExistingWithEmptyUserNameShouldThrowException()
        {
            var user = GetUserNotFromDb();
            user.Name = "";

            var existingUser = this.userController.IsUserExisting(user);
        }


        //UPDATE USER - can only update phone, email ,address, coins ,isAdmin and isDeleted

        [TestMethod]
        public void UpdateUserPhoneShouldPass()
        {

            var currentUser = GetExistingUserFromDb(1);

            var successUpdating = this.userController.UpdateUser(currentUser, "phone", "111111");

            var changedUser = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUser.Id).Phone;

            // Assert


            Assert.AreEqual("111111", changedUser);
        }


        //TO DO
        //CountUserBidsForGivenProduct 
        //GetAllUserSpentCoinsForGivenProduct 
        //GET USER BIDS
        //GetUserInvoices - Nasko
        //GetUserProducts - Nasko



        public User getExistingUserByUserName()
        {
            return this.db.Object.Users.First(u => u.Username == "John");
        }

        public User GetExistingUserById()
        {
            return this.db.Object.Users.First(u => u.Id == 1);
        }

        //LIBOR TESTS



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserWithNegativeIdShouldThrowException()
        {
            var user = GetUserNotFromDb();
            user.Id = -1;

            var successUpdate = this.userController.UpdateUser(user, "Name", "John");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserWithZeroIdShouldThrowException()
        {
            var user = GetUserNotFromDb();
            user.Id = 0;

            var successUpdate = this.userController.UpdateUser(user, "Name", "John");
        }

        [TestMethod]
        public void UpdateUserNameShouldPassIfNameIsChanged()
        {
            // Act
             
            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Name", "Conor");

            var changedUserName = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUser.Id).Name;

            // Assert

            Assert.AreEqual("Conor", changedUserName);
        }

        [TestMethod]
        public void UpdateUserNameShouldReturnTrue()
        {
            // Act

            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Name", "Conor");

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserNameShouldThrowExceptionIfNameIsNull()
        { 
            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Name", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserNameShouldThrowExceptionIfNameIsEmpty()
        {
            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Name", "");
        }

        [TestMethod]
        public void UpdateUserPhoneShouldPassIfPhoneIsChanged()
        {
            // Act

            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Phone", "545644888");

            var changedUserPhone = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUser.Id).Phone;

            // Assert

            Assert.AreEqual("545644888", changedUserPhone);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserPhoneShouldThrowExceptionIfPhoneIsNull()
        {
            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Phone", null);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserPhoneShouldThrowExceptionIfPhoneIsEmpty()
        {
            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Phone", "");
        }

        [TestMethod]
        public void UpdateUserEmailShouldPassIfEmailIsChanged()
        {
            // Act

            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Email", "Conor.Mcgregor@gmail.com");

            var changedUserEmail = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUser.Id).Email;

            // Assert

            Assert.AreEqual("Conor.Mcgregor@gmail.com", changedUserEmail);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserEmailShouldThrowExceptionIfEmailIsEmpty()
        {
            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Email", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserEmailShouldThrowExceptionIfEmailIsNull()
        {
            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Email", null);
        }

        [TestMethod]
        public void UpdateUserAddressShouldPassIfAddressIsChanged()
        {
            // Act

            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Address", "Nytorv");

            var changedUserAddress = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUser.Id).Address;

            // Assert

            Assert.AreEqual("Nytorv", changedUserAddress);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserAddressShouldThrowExceptionIfAddressIsNull()
        {
            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Address", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserAddressShouldThrowExceptionIfAddressIsEmpty()
        {
            var currentUser = GetExistingUserById();

            var successUpdating = this.userController.UpdateUser(currentUser, "Address", "");
        }

        public void UpdateUserAddressShouldThrowExceptionIfAddressIsEmpty1()
        {
            var currentUserId = GetExistingUserById().Id;

        }


        //Creating Simple Test Data

        public User GetUserCreatedInDb()
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
                //IsAdmin = false,
                Password = "Banana1",
                IsDeleted = false,
                //Bids = new List<Bid> { CreateTestBid() }

            };


        }
        public User GetExistingUserFromDb(int id)
        {
            return this.db.Object.Users.First(p => p.Id == id);
        }

        public User GetUserNotFromDb()
        {
            return new User
            {
                Id = 2,
                Username = "Pesho",
                Name = "Pesho",
                DateOfBirth = DateTime.Now.AddYears(-19),
                Gender = Models.Enums.Gender.Female,
                Phone = "666999696",
                Email = "Pesho.Cena@gmail.com",
                Address = "street",
                Zip = CreateFakeZip(),
                Coins = 11,
                Password = "password"
            };
        }

        private Product CreateTestProduct()
        {
            return new Product
            {
                Name = "Table",
                Description = "New",
                Price = 2000m,
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now.AddDays(2)
            };
        }

        private Bid CreateTestBid()
        {
            return new Bid
            {

                UserId = GetUserCreatedInDb().Id,
                ProductId = CreateTestProduct().Id,
                Coins = 1,
                DateOfCreated = DateTime.Now,
                IsWon = false,
                Id = 1,

            };

        }

        private Zip CreateFakeZip()
        {
            var zip = new Zip
            {
                ZipId = 1,
                City = "aalborg",
                ZipCode = "9000",
                Country = "china"

            };
            return zip;
        }

    }
}