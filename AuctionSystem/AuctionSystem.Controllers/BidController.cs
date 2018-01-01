namespace AuctionSystem.Controllers
{
    using Common;
    using Data;
    using Contracts;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BidController : IBidController
    {
        private static BidController instance;

        private BidController()
        {
        }

        public static BidController Instance()
        {
            if (instance == null)
            {
                instance = new BidController();
            }

            return instance;
        }

        public IList<Bid> GetAllBidsByProductId(int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

            var isProductExists = ProductController.Instance().IsProductExistingById(productId);

            if (!isProductExists)
            {
                throw new ArgumentException("Product does not exist in the system.");
            }

            using (var db = new AuctionContext())
            {
                return db.Bids
                            .Include("User")
                            .Include("Product")
                            .Where(b => b.ProductId == productId)
                            .OrderByDescending(b => b.DateOfCreated)
                            .ThenByDescending(b => b.Coins)
                            .ToList();
            }
        }

        public IList<Bid> GetAllBidsByUserId(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            var isUserExists = UserController.Instance().IsUserExistingById(userId);

            if (!isUserExists)
            {
                throw new ArgumentException("User does not exist in the system.");
            }

            using (var db = new AuctionContext())
            {
                return db.Bids
                            .Include("User")
                            .Include("Product")
                            .Where(b => b.UserId == userId)
                            .OrderByDescending(b => b.DateOfCreated)
                            .ThenByDescending(b => b.Coins)
                            .ToList();
            }
        }

        public IList<Bid> GetAllEarnedBids()
        {
            using (var db = new AuctionContext())
            {
                return db.Bids
                            .Include("User")
                            .Include("Product")
                            .Where(b => b.IsWon == true)
                            .OrderByDescending(b => b.DateOfCreated)
                            .ToList();
            }
        }

        public Bid GetBidById(int bidId)
        {
            CoreValidator.ThrowIfNegativeOrZero(bidId, nameof(bidId));

            using (var db = new AuctionContext())
            {
                var currentBid = db.Bids.FirstOrDefault(b => b.Id == bidId);

                CoreValidator.ThrowIfNull(currentBid, nameof(currentBid));

                return currentBid;
            }
        }

        public bool IsBidWon(Bid bid)
        {
            CoreValidator.ThrowIfNegativeOrZero(bid.Id, nameof(bid.Id));

            using (var db = new AuctionContext())
            {
                var currentBid = GetBidById(bid.Id);

                CoreValidator.ThrowIfNull(currentBid, nameof(currentBid));

                return currentBid.IsWon;
            }
        }

        public void MakeBid(int userId, int productId, int coins)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));
            CoreValidator.ThrowIfNegativeOrZero(coins, nameof(coins));

            using (var db = new AuctionContext())
            {
                using (var transaction = db.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        #region CHECK FOR EXISTING USER, PRODUCT AND FOR VALID COINS

                        var userController = UserController.Instance();
                        var productController = ProductController.Instance();

                        ValidateUserAndProduct(userId, productId, userController, productController);
                        ValidateProductForBidding(productId, productController);

                        var currentUser = userController.GetUserById(userId);

                        db.Users.Attach(currentUser);

                        ValidateCoins(productId, coins, productController, currentUser);

                        #endregion

                        #region LOGIC FOR OVERBIDDING 
                        var isThereAnyBid = db.Bids.Any(b => b.ProductId == productId && b.IsWon == false);

                        if (isThereAnyBid)
                        {
                            var lastBidEntry = db.Bids
                                                    .Where(b => b.ProductId == productId)
                                                    .OrderByDescending(b => b.DateOfCreated)
                                                    .Take(1)
                                                    .FirstOrDefault();

                            if (coins <= lastBidEntry.Coins)
                            {
                                throw new ArgumentException($"You cannot overbid with less than or equal to the last bidders coins: {lastBidEntry.Coins}");
                            }

                            if (lastBidEntry.UserId == currentUser.Id)
                            {
                                throw new ArgumentException($"You cannot overbid yourself.");
                            }

                            var newBid = GetNewBid(userId, productId, coins);

                            currentUser.Coins -= coins;

                            var lastBidUserId = lastBidEntry.UserId;

                            var lastUser = userController.GetUserById(lastBidUserId);

                            db.Users.Attach(lastUser);

                            lastUser.Coins += lastBidEntry.Coins;

                            db.Entry(lastUser).State = System.Data.Entity.EntityState.Modified;

                            db.Bids.Add(newBid);
                            db.SaveChanges();

                            transaction.Commit();

                            return;
                        }

                        #endregion

                        #region LOGIC FOR CREATE BID FOR FIRST TIME

                        var bid = GetNewBid(userId, productId, coins);

                        currentUser.Coins -= coins;

                        db.Entry(currentUser).State = System.Data.Entity.EntityState.Modified;

                        db.Bids.Add(bid);
                        db.SaveChanges();

                        transaction.Commit();

                        #endregion
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool CheckCoinsValid(int productId, double coins)
        {
            using (var db = new AuctionContext())
            {
                var lastBidEntry = db.Bids
                                        .Where(b => b.ProductId == productId)
                                        .OrderByDescending(b => b.DateOfCreated)
                                        .Take(1)
                                        .FirstOrDefault();

                if (coins <= lastBidEntry.Coins)
                {
                    return false;
                }
                return true;
            }
        }

        public Bid GetBidByIdWithAllObjects(int bidId)
        {
            CoreValidator.ThrowIfNegativeOrZero(bidId, nameof(bidId));

            using (var db = new AuctionContext())
            {
                var resultBid = db.Bids
                                    .Include("User")
                                    .Include("Product")
                                    .FirstOrDefault(b => b.Id == bidId);

                CoreValidator.ThrowIfNull(resultBid, nameof(resultBid));

                return resultBid;
            }
        }

        public IList<Bid> GetAllBidsByProductName(string productName)
        {
            CoreValidator.ThrowIfNullOrEmpty(productName, nameof(productName));

            using (var db = new AuctionContext())
            {
                var isExisting = ProductController.Instance().IsProductExisting(productName);

                if (!isExisting)
                {
                    throw new ArgumentException("The product doesn't exist in the system.");
                }

                var resultBids = db.Bids
                                    .Include("Product")
                                    .Include("User")
                                    .Where(b => b.Product.Name == productName)
                                    .OrderByDescending(b => b.DateOfCreated)
                                    .ThenByDescending(b => b.Coins)
                                    .ToList();

                CoreValidator.ThrowIfNull(resultBids, nameof(resultBids));

                return resultBids;
            }
        }

        public bool SetWinnersForProducts()
        {
            var expiredProducts = ProductController.Instance().GetExpiredProductsIds();

            using (var db = new AuctionContext())
            {
                var bids = db.Bids
                                .Where(b => expiredProducts.Contains(b.ProductId))
                                .ToList();

                if (bids.Count == 0)
                {
                    return false;
                }

                foreach (var productId in expiredProducts)
                {
                    var currentBids = bids
                                        .Where(b => b.ProductId == productId)
                                        .ToList(); // remove this and include the where in first or default below

                    var lastBidder = currentBids
                                            .OrderByDescending(r => r.DateOfCreated) // or coins doesn't matter
                                            .Take(1)
                                            .FirstOrDefault();

                    lastBidder.IsWon = true;
                }

                db.SaveChanges();
            }

            return true;
        }

        private Bid GetNewBid(int userId, int productId, int coins)
        {
            return new Bid
            {
                UserId = userId,
                ProductId = productId,
                Coins = coins,
                DateOfCreated = DateTime.UtcNow,
                IsWon = false
            };
        }

        private static void ValidateCoins(int productId, int coins, ProductController productController, User currentUser)
        {
            if (coins > currentUser.Coins)
            {
                throw new ArgumentException($"Your coins are {currentUser.Coins}, you've tried to spent {coins}.");
            }

            var productPrice = productController.GetProductById(productId).Price;

            if (productPrice > coins)
            {
                throw new ArgumentException($"Cannot bid less than the product price.({productPrice})");
            }
        }

        private static void ValidateProductForBidding(int productId, ProductController productController)
        {
            var isProductAvailableForBidding = productController.IsProductAvailableForBidding(productId);

            if (!isProductAvailableForBidding)
            {
                throw new ArgumentException("This product is no longer available for bidding.");
            }
        }

        private static void ValidateUserAndProduct(int userId, int productId, UserController userController, ProductController productController)
        {
            var isUserExisting = userController.IsUserExistingById(userId);
            var isProductExisting = productController.IsProductExistingById(productId);

            if (!isUserExisting || !isProductExisting)
            {
                throw new ArgumentException("The product or the user is not existing in the system.");
            }
        }
    }
}
