namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Common;
    using Controllers.Contracts;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BidControllerMock : IBidController
    {
        private readonly AuctionContext dbContext;

        public BidControllerMock(AuctionContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool BidExpired(int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));
            using (var db = dbContext)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var product = new ProductControllerMock(dbContext).GetProductById(productId);
                        db.Products.Attach(product);
                        if (!(DateTime.Now >= product.EndDate))
                        {
                            throw new ArgumentException($"The product date for finish is not yet reached: {product.EndDate}");
                        }
                        var bid = db.Bids.Where(b => b.ProductId == productId).OrderByDescending(b => b.Id).First();
                        db.Bids.Attach(bid);
                        bid.IsWon = true;
                        db.Entry(bid).State = System.Data.Entity.EntityState.Modified;
                        product.IsAvailable = false;
                        db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                }
            }
        }

        public IList<Bid> GetAllBidsByProductId(int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

            var isProductExists = new ProductControllerMock(dbContext).IsProductExistingById(productId);

            if (!isProductExists)
            {
                throw new ArgumentException("Product does not exist in the system.");
            }

            using (dbContext)
            {
                return dbContext.Bids.Where(b => b.ProductId == productId).ToList();
            }
        }

        public IList<Bid> GetAllBidsByUserId(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            var isUserExists = new UserControllerMock(dbContext).IsUserExistingById(userId);

            if (!isUserExists)
            {
                throw new ArgumentException("User does not exist in the system.");
            }

            using (dbContext)
            {
                return dbContext.Bids.Where(b => b.UserId == userId).ToList();
            }
        }

        public IList<Bid> GetAllEarnedBids()
        {
            using (dbContext)
            {
                return dbContext.Bids.Where(b => b.IsWon == true).ToList();
            }
        }

        public bool CheckCoinsValid(int productId, double coins)
        {
            using (var db = dbContext)
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

        public Bid GetBidById(int bidId)
        {
            CoreValidator.ThrowIfNegativeOrZero(bidId, nameof(bidId));

            using (dbContext)
            {
                var currentBid = dbContext.Bids.FirstOrDefault(b => b.Id == bidId);

                CoreValidator.ThrowIfNull(currentBid, nameof(currentBid));

                return currentBid;
            }
        }

        public bool IsBidWon(Bid bid)
        {
            CoreValidator.ThrowIfNegativeOrZero(bid.Id, nameof(bid.Id));

            using (dbContext)
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

            using (dbContext)
            {
                #region CHECK FOR EXISTING USER, PRODUCT AND FOR VALID COINS
                var userController = new UserControllerMock(dbContext);
                var productController = new ProductControllerMock(dbContext);

                var isUserExisting = userController.IsUserExistingById(userId);
                var isProductExisting = productController.IsProductExistingById(productId);

                if (!isUserExisting || !isProductExisting)
                {
                    throw new ArgumentException("The product or the user is not existing in the system.");
                }

                var currentUser = userController.GetUserById(userId);

                dbContext.Users.Attach(currentUser);

                if (coins > currentUser.Coins)
                {
                    throw new ArgumentException($"Your coins are {currentUser.Coins}, you've tried to spent {coins}.");
                }
                #endregion

                #region LOGIC FOR OVERBIDDING 
                var isThereAnyBid = dbContext.Bids.Any(b => b.ProductId == productId && b.IsWon == false);

                if (isThereAnyBid)
                {
                    var lastBidEntry = dbContext.Bids
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
                        throw new ArgumentException("You cannot overbid yourself.");
                    }

                    var newBid = GetNewBid(userId, productId, coins);

                    currentUser.Coins -= coins;

                    var lastBidUserId = lastBidEntry.UserId;

                    var lastUser = userController.GetUserById(lastBidUserId);

                    dbContext.Users.Attach(lastUser);

                    lastUser.Coins += lastBidEntry.Coins;

                    // dbContext.Entry(lastUser).State = System.Data.Entity.EntityState.Modified;

                    dbContext.Bids.Add(newBid);
                    dbContext.SaveChanges();

                    return;
                }
                #endregion

                #region LOGIC FOR CREATE BID FOR FIRST TIME
                var bid = GetNewBid(userId, productId, coins);

                currentUser.Coins -= coins;

                dbContext.Entry(currentUser).State = System.Data.Entity.EntityState.Modified;

                dbContext.Bids.Add(bid);
                dbContext.SaveChanges();
                #endregion
            }
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

        public Bid GetBidByIdWithAllObjects(int bidId)
        {
            CoreValidator.ThrowIfNegativeOrZero(bidId, nameof(bidId));

            using (dbContext)
            {
                var resultBid = dbContext.Bids
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

            using (this.dbContext)
            {
                var isExisting = new ProductControllerMock(this.dbContext).IsProductExisting(productName);

                if (!isExisting)
                {
                    throw new ArgumentException("The product doesn't exist in the system.");
                }

                var resultBids = this.dbContext.Bids
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
    }
}
