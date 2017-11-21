namespace AuctionSystem.Controllers
{
    using Controllers.Common;
    using Data;
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BidController : IBidController
    {
        public IList<Bid> GetAllBidsByProductId(int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

            using (var db = new AuctionContext())
            {
                return db.Bids.Where(b => b.ProductId == productId).ToList();
            }
        }

        public IList<Bid> GetAllBidsByUserId(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

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

        public bool IsBidWon(int bidId)
        {
            CoreValidator.ThrowIfNegativeOrZero(bidId, nameof(bidId));

            using (var db = new AuctionContext())
            {
                var currentBid = GetBidById(bidId);

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
                #region CHECK FOR EXISTING USER, PRODUCT AND FOR VALID COINS
                var userController = UserController.Instance();
                var productController = new ProductController();

                var isUserExisting = userController.IsUserExistingById(userId);
                var isProductExisting = productController.IsProductExistingById(productId);

                if (!isUserExisting || !isProductExisting)
                {
                    throw new ArgumentException("The product or the user is not existing in the system.");
                }

                var currentUser = userController.GetUserById(userId);

                db.Users.Attach(currentUser);

                if (coins > currentUser.Coins)
                {
                    throw new ArgumentException($"Your coins are {currentUser.Coins}, you've tried to spent {coins}.");
                }
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

                    var newBid = GetNewBid(userId, productId, coins);

                    currentUser.Coins -= coins;

                    var lastBidUserId = lastBidEntry.UserId;

                    var lastUser = userController.GetUserById(lastBidUserId);

                    db.Users.Attach(lastUser);

                    lastUser.Coins += lastBidEntry.Coins;

                    db.Entry(lastUser).State = System.Data.Entity.EntityState.Modified;

                    db.Bids.Add(newBid);
                    db.SaveChanges();

                    return;
                }
                #endregion

                #region LOGIC FOR CREATE BID FOR FIRST TIME
                var bid = GetNewBid(userId, productId, coins);

                currentUser.Coins -= coins;

                db.Entry(currentUser).State = System.Data.Entity.EntityState.Modified;

                db.Bids.Add(bid);
                db.SaveChanges();
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
                DateOfCreated = DateTime.Now,
                IsWon = false
            };
        }
    }
}
