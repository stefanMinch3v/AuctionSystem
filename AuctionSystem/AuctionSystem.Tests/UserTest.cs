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
        private Mock<DbSet<User>> mockSetUser;
        private Mock<DbSet<Zip>> mockSetZip;
        private Mock<DbSet<Payment>> mockSetPayment;
        private Mock<DbSet<Bid>> mockSetBid;
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
            var payment = CreateTestPayment();

            this.userData = new List<User>() { user };
            this.paymentData = new List<Payment>() { payment };
            this.bidData = new List<Bid>() { bid };
            this.zipData = new List<Zip>() { zip };

            this.mockSetUser = new Mock<DbSet<User>>().SetupData(this.userData);
            this.mockSetZip = new Mock<DbSet<Zip>>().SetupData(this.zipData);
            this.mockSetPayment = new Mock<DbSet<Payment>>().SetupData(this.paymentData);
            this.mockSetBid = new Mock<DbSet<Bid>>().SetupData(this.bidData);

            this.db.Setup(m => m.Users).Returns(mockSetUser.Object);
            this.db.Setup(m => m.Zips).Returns(mockSetZip.Object);
            this.db.Setup(m => m.Payments).Returns(mockSetPayment.Object);
            this.db.Setup(m => m.Bids).Returns(mockSetBid.Object);
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
            var user = GetUserCreatedInDb().Username;

            var existingUser = this.userController.IsUserExisting(user);

            Assert.IsTrue(existingUser);
        }

        [TestMethod]
        public void IsUserExistingShouldNotPass()
        {
            var user = GetUserNotFromDb().Username;

            var existingUser = this.userController.IsUserExisting(user);

            Assert.IsFalse(existingUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUserExistingWithNullUserNameShouldThrowException()
        {
            var user = GetUserNotFromDb();
            user.Username = null;

            var existingUser = this.userController.IsUserExisting(user.Username);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUserExistingWithEmptyUserNameShouldThrowException()
        {
            var user = GetUserNotFromDb();
            user.Username = "";

            var existingUser = this.userController.IsUserExisting(user.Username);
        }


        //UPDATE USER - can only update phone, email ,address, coins ,isAdmin and isDeleted

        [TestMethod]
        public void UpdateUserPhoneShouldPass()
        {

            var currentUser = GetExistingUserFromDb(1);

            currentUser.Phone = "111111";

            var successUpdating = this.userController.UpdateUser(currentUser);

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
            var user = GetExistingUserFromDb(1);
            user.Id = -1;

            var successUpdate = this.userController.UpdateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserWithZeroIdShouldThrowException()
        {
            var user = GetUserNotFromDb();
            user.Id = 0;

            var successUpdate = this.userController.UpdateUser(user);
        }

        [TestMethod]
        public void UpdateUserNameShouldPassIfNameIsChanged()
        {
            // Act
             
            var currentUser = GetExistingUserById();

            currentUser.Name = "Conor";

            var successUpdating = this.userController.UpdateUser(currentUser);

            var changedUserName = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUser.Id).Name;

            // Assert

            Assert.AreEqual("Conor", changedUserName);
        }

        [TestMethod]
        public void UpdateUserNameShouldReturnTrue()
        {
            // Act

            var currentUser = GetExistingUserById();

            currentUser.Name = "Conor";

            var successUpdating = this.userController.UpdateUser(currentUser);

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserNameShouldThrowExceptionIfNameIsNull()
        { 
            var currentUser = GetExistingUserById();

            currentUser.Name = null;

            var successUpdating = this.userController.UpdateUser(currentUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserNameShouldThrowExceptionIfNameIsEmpty()
        {
            var currentUser = GetExistingUserById();

            currentUser.Name = "";

            var successUpdating = this.userController.UpdateUser(currentUser);
        }

        [TestMethod]
        public void UpdateUserPhoneShouldPassIfPhoneIsChanged()
        {
            // Act

            var currentUser = GetExistingUserById();

            currentUser.Phone = "545644888";

            var successUpdating = this.userController.UpdateUser(currentUser);

            var changedUserPhone = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUser.Id).Phone;

            // Assert

            Assert.AreEqual("545644888", changedUserPhone);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserPhoneShouldThrowExceptionIfPhoneIsNull()
        {
            var currentUser = GetExistingUserById();

            currentUser.Phone = null;

            var successUpdating = this.userController.UpdateUser(currentUser);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserPhoneShouldThrowExceptionIfPhoneIsEmpty()
        {
            var currentUser = GetExistingUserById();

            currentUser.Phone = "";

            var successUpdating = this.userController.UpdateUser(currentUser);
        }

        [TestMethod]
        public void UpdateUserEmailShouldPassIfEmailIsChanged()
        {
            // Act

            var currentUser = GetExistingUserById();

            currentUser.Email = "Conor.Mcgregor@gmail.com";

            var successUpdating = this.userController.UpdateUser(currentUser);

            var changedUserEmail = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUser.Id).Email;

            // Assert

            Assert.AreEqual("Conor.Mcgregor@gmail.com", changedUserEmail);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserEmailShouldThrowExceptionIfEmailIsEmpty()
        {
            var currentUser = GetExistingUserById();

            currentUser.Email = "";

            var successUpdating = this.userController.UpdateUser(currentUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserEmailShouldThrowExceptionIfEmailIsNull()
        {
            var currentUser = GetExistingUserById();

            currentUser.Email = null;

            var successUpdating = this.userController.UpdateUser(currentUser);
        }

        [TestMethod]
        public void UpdateUserAddressShouldPassIfAddressIsChanged()
        {
            // Act

            var currentUser = GetExistingUserById();

            currentUser.Address = "Nytorv";

            var successUpdating = this.userController.UpdateUser(currentUser);

            var changedUserAddress = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUser.Id).Address;

            // Assert

            Assert.AreEqual("Nytorv", changedUserAddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserAddressShouldThrowExceptionIfAddressIsNull()
        {
            var currentUser = GetExistingUserById();

            currentUser.Address = null;

            var successUpdating = this.userController.UpdateUser(currentUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserAddressShouldThrowExceptionIfAddressIsEmpty()
        {
            var currentUser = GetExistingUserById();

            currentUser.Address = "";

            var successUpdating = this.userController.UpdateUser(currentUser);
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
                DateOfBirth = DateTime.Now.AddYears(-19),
                Gender = Models.Enums.Gender.Female,
                Phone = "666999696",
                Email = "John.Cena@gmail.com",
                Address = "Fake street ",
                ZipId = 1,
                Coins = 1,
                IsAdmin = false,
                Password = "Banana1",
                IsDeleted = false,
                Payments = this.paymentData
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
                ZipId = CreateFakeZip().Id,
                Coins = 11,
                IsAdmin = false,
                IsDeleted = false,
                Password = "password",
                Payments = this.paymentData
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
                Id = 1,
                City = "aalborg",
                ZipCode = "9000",
                Country = "china"

            };
            return zip;
        }

        private Payment CreateTestPayment()
            => new Payment
            {
                UserId = 1,
                Type = Models.Enums.PaymentType.PayPal,
                PaymentTypeCode = "vacklavec@paypal.com"
            };

    }
}