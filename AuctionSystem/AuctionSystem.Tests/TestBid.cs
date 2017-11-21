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
    public class BidTest
    {

        private Mock<AuctionContext> db;
        private BidControllerMock bidController;
        private UserControllerMock userController;
        private ZipControllerMock zipController;
        private ProductControllerMock productController;
        private Mock<DbSet<Bid>> mockSetBid;
        private Mock<DbSet<User>> mockSetUser;
        private Mock<DbSet<Zip>> mockSetZip;
        private Mock<DbSet<Product>> mockSetProduct;
        private List<Product> products;
        private List<Zip> zips;
        private List<User> users;
        private List<Bid> bids;

        public object Id { get; private set; }

        #region Mock database initialization
        [TestInitialize]
        public void Initialize()
        {
            // Arrange

            //setting dependency injection for the tests
            db = new Mock<AuctionContext>();
            bidController = new BidControllerMock(db.Object);
            userController = new UserControllerMock(db.Object);
            zipController = new ZipControllerMock(db.Object);
            productController = new ProductControllerMock(db.Object);

            // this.data = new List<Product>() { availableProduct, unavailableProduct }; // insert in collection

            products = new List<Product>() { GetProduct() };
            zips = new List<Zip>() { GetZip() };
            users = new List<User>() { GetUser(), GetUser2() };
            bids = GetBids();

            //Seting up the mocked DB

            mockSetBid = new Mock<DbSet<Bid>>().SetupData(bids);
            mockSetUser = new Mock<DbSet<User>>().SetupData(users);
            mockSetZip = new Mock<DbSet<Zip>>().SetupData(zips);
            mockSetProduct = new Mock<DbSet<Product>>().SetupData(products);

            db.Setup(m => m.Bids).Returns(mockSetBid.Object);
            db.Setup(m => m.Users).Returns(mockSetUser.Object);
            db.Setup(m => m.Zips).Returns(mockSetZip.Object);
            db.Setup(m => m.Products).Returns(mockSetProduct.Object);


        }
        #endregion

        #region GetAllBidsByProductId method tests
        [TestMethod]
        public void GetAllBidsByProductIdShouldPass()
        {
            // Act
            var expected = GetExistingBidFromDb();
            var actual = bidController.GetAllBidsByProductId(GetProduct().Id);

            // Assert
            Assert.IsTrue(CompareBidLists(expected.ToList(), actual.ToList()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void getAllBidsByProductIdwithNegativeIdShouldFail()
        {
            // Act
            bidController.GetAllBidsByProductId(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void getAllBidsByProductIdwithZeroIdShouldFail()
        {
            // Act
            bidController.GetAllBidsByProductId(0);
        }

        [TestMethod]
        public void getAllBidsByProductIdShouldReturnNull()
        {
            // Act
            var bids = this.bidController.GetAllBidsByProductId(Int32.MaxValue);

            // Assert
            Assert.IsNull(bids);
        }
        #endregion

        #region GetAllBidsByUserId method tests
        [TestMethod]
        public void GetAllBidsByUserIdShouldPass()
        {
            // Act
            var expected = new List<Bid> { GetBid() };
            var actual = bidController.GetAllBidsByUserId(GetUser().Id);

            // Assert
            Assert.IsTrue(CompareBidLists(expected, actual.ToList()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void getAllBidsByUserIdwithZeroIdShouldFail()
        {
            // Act
            bidController.GetAllBidsByUserId(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void getAllBidsByUserIdwithNegativeIdShouldFail()
        {
            // Act
            bidController.GetAllBidsByUserId(-1);
        }

        [TestMethod]
        public void getAllBidsByUserIdShouldReturnNull()
        {
            // Act
            var bids = this.bidController.GetAllBidsByUserId(Int32.MaxValue);

            // Assert
            Assert.IsNull(bids);
        }
        #endregion

        #region getAllEarnedBids method tests
        [TestMethod]
        public void GetAllEarnedBidsShouldPass()
        {
            // Act
            var expected = new List<Bid> { GetBid2() };
            var actual = bidController.GetAllEarnedBids();

            // Assert
            Assert.IsTrue(CompareBidLists(expected, actual.ToList()));
        }
        #endregion

        #region GetBidById method tests
        [TestMethod]
        public void GetBidByIdShouldPass()
        {
            // Act
            var expected = GetBid();
            var actual = bidController.GetBidById(GetBid().Id);

            // Assert
            Assert.AreEqual(expected.Id, actual.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBidByIdwithZeroIdShouldFail()
        {
            // Act
            bidController.GetBidById(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBidByIdwithNegativeIdShouldFail()
        {
            // Act
            bidController.GetBidById(-1);
        }

        [TestMethod]
        public void GetBidByIdShouldReturnNull()
        {
            // Act
            var bids = this.bidController.GetBidById(Int32.MaxValue);

            // Assert
            Assert.IsNull(bids);
        }
        #endregion

        #region IsBidWon method tests
        [TestMethod]
        public void IsBidWonShouldPass()
        {
            var result = bidController.IsBidWon(GetBid2().Id);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsBidWonWhichIsNotFinishedShouldPass()
        {
            var result = bidController.IsBidWon(GetBid().Id);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsBidWonwithZeroIdShouldFail()
        {
            // Act
            bidController.IsBidWon(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsBidWonwithNegativeIdShouldFail()
        {
            // Act
            bidController.IsBidWon(-1);
        }

        [TestMethod]
        public void IsBidWonShouldReturnNull()
        {
            // Act
            var bids = this.bidController.IsBidWon(Int32.MaxValue);

            // Assert
            Assert.IsNull(bids);
        }
        #endregion

        #region MakeBid method tests
        [TestMethod]
        public void MakeBidShouldPass()
        {
            //Arrange further


            // Act
            var userId = GetUser().Id;
            var productId = GetProduct().Id;
            var coins = GetUser().Coins;

            bidController.MakeBid(userId, productId, coins);
            var expectedId = 0;
            var actualBid = bidController.GetAllBidsByUserId(userId).Last();
            var actualId = actualBid.Id;

            // Assert
            Assert.AreEqual(expectedId, actualId);
            //Assert.IsTrue(true);
        }

        [TestMethod]
        public void MakeBidCheckCoinsSubstractShouldPass()
        {
            //Arrange further
            productController.CreateProduct(GetProduct().Name + "Test", GetProduct().Description, GetProduct().Price, DateTime.Now, DateTime.Now.AddMinutes(1));
            var product = productController.GetProductByName(GetProduct().Name + "Test");

            // Act
            var userId = GetUser().Id;
            var productId = product.Id;
            var coins = 50;
            var coinsBefore = GetUser().Coins;

            bidController.MakeBid(userId, productId + 1, coins);

            var actualBid = bidController.GetAllBidsByUserId(userId).Last();
            var coinsAfter = userController.GetUserById(actualBid.UserId).Coins;

            // Assert
            Assert.AreEqual(coinsBefore - coins, coinsAfter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeBidWithZeroUserIdShouldFail()
        {
            //Arrange further
            productController.CreateProduct(GetProduct().Name + "Test", GetProduct().Description, GetProduct().Price, DateTime.Now, DateTime.Now.AddMinutes(1));
            var product = productController.GetProductByName(GetProduct().Name + "Test");

            // Act
            var userId = 0;
            var productId = product.Id;
            var coins = 550;

            bidController.MakeBid(userId, productId + 1, coins);
        }

        [TestMethod]
        public void MakeBidCoinsReturnShouldPass()
        {
            var product = GetProduct();
            var lastBid = bidController
                        .GetAllBidsByProductId(product.Id)
                        .OrderBy(p => p.Id)
                        .Last();
            var oldUser = userController.GetUserById(lastBid.UserId);
            var lastCoinsBid = lastBid.Coins;
            
            // Act
            var newUserId = GetUser().Id;
            var productId = product.Id;
            var coins = 550;
            var userCoinsBefore = GetUser2().Coins; 

            bidController.MakeBid(newUserId, productId, coins);
            var userCoinsAfter = userController.GetUserById(oldUser.Id).Coins;

            //Assert
            Assert.AreEqual(userCoinsBefore + lastCoinsBid, userCoinsAfter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeBidWithMoreCoinsThanAvailableShouldFail()
        {
            var product = GetProduct();

            // Act
            var userId = GetUser().Id;
            var productId = product.Id;
            var coins = GetUser().Coins+1;

            bidController.MakeBid(userId, productId, coins);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeBidWithLessCoinsThanCurrentBidShouldFail()
        {
            var product = GetProduct();

            // Act
            var userId = GetUser().Id;
            var productId = product.Id;
            var coins = 10; //current bid is 25

            bidController.MakeBid(userId, productId, coins);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeBidWithUserNotExistingIdShouldFail()
        {
            var product = GetProduct();

            // Act
            var userId = Int32.MaxValue;
            var productId = product.Id;
            var coins = 550; 

            bidController.MakeBid(userId, productId, coins);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeBidWithNegativeUserIdShouldFail()
        {
            var product = GetProduct();

            // Act
            var userId = -1;
            var productId = product.Id;
            var coins = 550; 

            bidController.MakeBid(userId, productId, coins);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeBidWithZeroProductIdShouldFail()
        {
            var product = GetProduct();

            // Act
            var userId = GetUser().Id;
            var productId = 0;
            var coins = 550;

            bidController.MakeBid(userId, productId, coins);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeBidWithNegativeProductIdShouldFail()
        {
            var product = GetProduct();

            // Act
            var userId = GetUser().Id;
            var productId = -1;
            var coins = 550; 

            bidController.MakeBid(userId, productId, coins);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeBidWithNotExistingProductIdShouldFail()
        {
            var product = GetProduct();

            // Act
            var userId = GetUser().Id;
            var productId = 5;
            var coins = 550; 

            bidController.MakeBid(userId, productId, coins);
        }
        #endregion

        #region Helpers
        private List<Bid> GetBids() => new List<Bid>
        {
            GetBid(),
            GetBid2()
        };
        private Bid GetBid() => new Bid
        {
            Id = 1,
            Coins = 15,
            DateOfCreated = DateTime.Now.AddHours(-5),
            IsWon = false,
            ProductId = GetProduct().Id,
            UserId = GetUser().Id
        };

        private Bid GetBid2() => new Bid
        {
            Id = 2,
            Coins = 25,
            DateOfCreated = DateTime.Now.AddHours(-4),
            IsWon = true,
            ProductId = GetProduct().Id,
            UserId = GetUser2().Id
        };

        private User GetUser() => new User
        {
            Id = 1,
            Address = "Some valid address",
            DateOfBirth = DateTime.Now.AddYears(-20),
            Coins = 1000,
            Email = "TestEmail@test.com",
            Gender = Models.Enums.Gender.Male,
            Name = "Test",
            Password = "testPassword",
            Username = "Pesho",
            Phone = "53097445",
            Zip = GetZip()
        };
        private User GetUser2() => new User
        {
            Id = 2,
            Address = "Some valid address",
            DateOfBirth = DateTime.Now.AddYears(-20),
            Coins = 1000,
            Email = "TestEmail2@test.com",
            Gender = Models.Enums.Gender.Male,
            Name = "Test2",
            Password = "testPassword",
            Username = "Pesho2",
            Phone = "53097455",
            Zip = GetZip()
        };
        private Zip GetZip() => new Zip
        {
            Id = 1,
            City = "Aalborg",
            Country = "Denmark",
            ZipCode = "9000"
        };

        private Product GetProduct() => new Product
        {
            Id = 1,
            Name = "Available FakeProduct",
            Description = "Unique fake product",
            Price = 9,
            IsAvailable = true,
            StartDate = DateTime.Now.AddDays(-2),
            EndDate = DateTime.Now.AddDays(2)
        };

        private List<Bid> GetExistingBidFromDb()
        {
            return db.Object.Bids.Where(b => b.ProductId == GetProduct().Id).ToList();
        }

        private bool CompareBidLists(List<Bid> expected, List<Bid> actual)
        {
            var expectedIds = GetIdsFromList(expected);
            var actualIds = GetIdsFromList(actual);
            var firstNotSecond = expectedIds.Except(actualIds);
            var secondNotFirst = actualIds.Except(expectedIds);
            string text2 = String.Empty;
            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }

        private List<int> GetIdsFromList(List<Bid> list)
        {
            List<int> expectedIds = new List<int>();
            foreach (var Bid in list)
            {
                expectedIds.Add(Bid.Id);
            }
            return expectedIds;
        }
        #endregion
    }
}
