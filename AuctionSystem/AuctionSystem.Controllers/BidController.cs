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
                return db.Bids.Where(b => b.ProductId == productId).ToList();
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
                return db.Bids.Where(b => b.UserId == userId).ToList();
            }
        }

        public IList<Bid> GetAllEarnedBids()
        {
            using (var db = new AuctionContext())
            {
                return db.Bids.Where(b => b.IsWon == true).ToList();
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

        public void MakeBid(User user, Product product, int coins)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));
            CoreValidator.ThrowIfNegativeOrZero(coins, nameof(coins));

            using (var db = new AuctionContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        #region CHECK FOR EXISTING USER, PRODUCT AND FOR VALID COINS

                        var userController = UserController.Instance();
                        var productController = ProductController.Instance();

                        var isUserExisting = userController.IsUserExistingById(user.Id);
                        var isProductExisting = productController.IsProductExistingById(product.Id);

                        if (!isUserExisting || !isProductExisting)
                        {
                            throw new ArgumentException("The product or the user is not existing in the system.");
                        }

                        var currentUser = userController.GetUserById(user.Id);

                        db.Users.Attach(currentUser);

                        if (coins > currentUser.Coins)
                        {
                            throw new ArgumentException($"Your coins are {currentUser.Coins}, you've tried to spent {coins}.");
                        }
                        #endregion

                        #region LOGIC FOR OVERBIDDING 
                        var isThereAnyBid = db.Bids.Any(b => b.ProductId == product.Id && b.IsWon == false);

                        if (isThereAnyBid)
                        {
                            var lastBidEntry = db.Bids
                                                    .Where(b => b.ProductId == product.Id)
                                                    .OrderByDescending(b => b.DateOfCreated)
                                                    .Take(1)
                                                    .FirstOrDefault();

                            if (coins <= lastBidEntry.Coins)
                            {
                                transaction.Rollback();
                                throw new ArgumentException($"You cannot overbid with less than or equal to the last bidders coins: {lastBidEntry.Coins}");
                            }

                            var newBid = GetNewBid(user.Id, product.Id, coins);

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

                        var bid = GetNewBid(user.Id, product.Id, coins);

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

        private Bid GetNewBid(int userId, int productId, int coins)
        {
            return new Bid
            {
                UserId = userId,
                ProductId = productId,
                Coins = coins,
                DateOfCreated = DateTime.Now,
                IsWon = false
            };
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
    }
}
