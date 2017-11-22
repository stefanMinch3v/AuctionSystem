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

            var user = CreateTestUserWorking();
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
            var expected = getExistingUserById().Id;
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

            this.userController.CreateUser("name", "pass", "name name", "adress", "email", "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);

            var actual = this.db.Object.Users.First(p => p.Name == "name name");

            Assert.AreEqual("name name", actual.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyUsernameNameArgumentException()
        {

            this.userController.CreateUser("", "pass", "name name", "adress", "email", "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullUsernameArgumentException()
        {

            this.userController.CreateUser(null, "pass", "name name", "adress", "email", "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyPasswordArgumentException()
        {

            this.userController.CreateUser("name", "", "name name", "adress", "email", "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullPasswordArgumentException()
        {

            this.userController.CreateUser("name", null, "name name", "adress", "email", "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyNameArgumentException()
        {

            this.userController.CreateUser("name", "pass", "", "adress", "email", "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullNameArgumentException()
        {

            this.userController.CreateUser("name", "pass", null, "address", "email", "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyAddressArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "", "email", "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullAddressArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", null, "email", "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyEmailArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "address", "", "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullEmailArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "address", null, "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowEmptyPhoneArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "address", "email", "", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNullPhoneArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "address", "email", null, DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowDateTypeArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "adress", "email", "phone", "57", Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowDateEmptyArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "adress", "email", "phone", "", Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowDateNullArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "adress", "email", "phone", null, Models.Enums.Gender.Female, 1, 10, paymentData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateUserShouldThrowNegativeCoinArgumentException()
        {

            this.userController.CreateUser("name", "pass", "name name", "adress", "email", "phone", DateTime.Now.AddYears(-20).ToString(), Models.Enums.Gender.Female, 1, -1, paymentData);
        }

        //DELETE USER

        [TestMethod]
        public void DeleteUserShouldPass()
        {
            var existingUser = getExistingUserByUserName().Id;

            var deletedUser = this.userController.DeleteUser(existingUser);
            Assert.IsTrue(deletedUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteUserWithNonExistingIdShouldThrowException()
        {  
            var deletedUser = this.userController.DeleteUser(15);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteUserWithNegativeIdShoulThrowException()
        {
            var deletedUser = this.userController.DeleteUser(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteUserWithZeroIdShoulThrowException()
        {
            var deletedUser = this.userController.DeleteUser(0);
        }

        //IS USER EXISTING

        [TestMethod]
        public void IsUserExistingShouldPass()
        {
            var existingUser = this.userController.IsUserExisting("John");

            Assert.IsTrue(existingUser);
        }

        [TestMethod]
        public void IsUserExistingShouldNotPass()
        {
            var existingUser = this.userController.IsUserExisting("Peter");

            Assert.IsFalse(existingUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUserExistingWithNullUserNameShouldThrowException()
        {
            var existingUser = this.userController.IsUserExisting(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUserExistingWithEmptyUserNameShouldThrowException()
        {
            var existingUser = this.userController.IsUserExisting("");
        }


        //UPDATE USER - can only update phone, email ,address, coins ,isAdmin and isDeleted

        [TestMethod]
        public void UpdateUserPhoneShouldPass()
        {
            
            var currentUserId = GetExistingUserFromDb(1).Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "phone", "111111");

            var changedUser = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUserId).Phone;

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

        public User getExistingUserById()
        {
            return this.db.Object.Users.First(u => u.Id == 1);
        }

        //LIBOR TESTS
        
        /*
         
         [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void UpdateUserWithNegativeIdShouldThrowException()

            {
                var successUpdate = this.userController.UpdateUser(-1, "Name", "John");
            }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserWithZeroIdShouldThrowException()
        {
            var successUpdate = this.userController.UpdateUser(0, "Name", "John");
        }

        [TestMethod]
        public void UpdateUserIdShouldPass()
        {
            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(15, "Name", "John");

            var changedUserId = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUserId).Id;

            // Assert

            Assert.AreEqual(15, changedUserId);
        }

        [TestMethod]
        public void UpdateUserNameShouldPassIfNameIsChanged()
        {
            // Act

            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Name", "Conor");

            var changedUserName = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUserId).Name;

            // Assert

            Assert.AreEqual("Conor", changedUserName);
        }

        [TestMethod]
        public void UpdateUserNameShouldReturnTrue()
        {
            // Act

            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Name", "Conor");

            // Assert

            Assert.IsTrue(successUpdating);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserNameShouldThrowExceptionIfNameIsNull()
        {
            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Name", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserNameShouldThrowExceptionIfNameIsEmpty()
        {
            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Name", "");
        }

        [TestMethod]
        public void UpdateUserPhoneShouldPassIfPhoneIsChanged()
        {
            // Act

            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Phone", "545644888");

            var changedUserPhone = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUserId).Phone;

            // Assert

            Assert.AreEqual("545644888", changedUserPhone);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserPhoneShouldThrowExceptionIfPhoneIsNull()
        {
            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Phone", null);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserPhoneShouldThrowExceptionIfPhoneIsEmpty()
        {
            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Phone", "");
        }

        [TestMethod]
        public void UpdateUserEmailShouldPassIfEmailIsChanged()
        {
            // Act

            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Email", "Conor.Mcgregor@gmail.com");

            var changedUserEmail = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUserId).Email;

            // Assert

            Assert.AreEqual("Conor.Mcgregor@gmail.com", changedUserEmail);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserEmailShouldThrowExceptionIfEmailIsEmpty()
        {
            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Email", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserEmailShouldThrowExceptionIfEmailIsNull()
        {
            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Email", null);
        }

        [TestMethod]
        public void UpdateUserAddressShouldPassIfAddressIsChanged()
        {
            // Act

            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Address", "Nytorv");

            var changedUserAddress = this.db.Object.Users.FirstOrDefault(u => u.Id == currentUserId).Address;

            // Assert

            Assert.AreEqual("Nytorv", changedUserAddress);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserAddressShouldThrowExceptionIfAddressIsNull()
        {
            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Address", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUserAddressShouldThrowExceptionIfAddressIsEmpty()
        {
            var currentUserId = getExistingUserById().Id;

            var successUpdating = this.userController.UpdateUser(currentUserId, "Address", "");
        }

        public void UpdateUserAddressShouldThrowExceptionIfAddressIsEmpty1()
        {
            var currentUserId = getExistingUserById().Id;

        }
        */

        //Creating Simple Test Data

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

        public Product CreateTestProduct()
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

        public Bid CreateTestBid()
        {
            return new Bid {

                UserId=  CreateTestUserWorking().Id,
                ProductId = CreateTestProduct().Id,
                Coins = 1,
                DateOfCreated = DateTime.Now,
                IsWon = false,
                Id = 1,

            };

        }

        public Zip CreateFakeZip()
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