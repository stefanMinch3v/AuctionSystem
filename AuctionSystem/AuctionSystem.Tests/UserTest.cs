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

        [TestInitialize]
        public void Initialize()
        {
            this.db = new Mock<AuctionContext>();

            this.userController = new UserControllerMock(this.db.Object);

            var user = CreateTestUserWorking();
            

            this.data = new List<User>() { user };
            

            this.mockSet = new Mock<DbSet<User>>().SetupData(this.data);

            this.db.Setup(m => m.Users).Returns(mockSet.Object);

            
            
        }
        [TestMethod]
        public void getUserByNameShouldPass()
        {
            var expected = getExistingUserByUserName().Username;
            var actual = userController.GetUserByUsername("John").Username;
            Assert.AreEqual(expected, actual);
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

