namespace AuctionSystem.Controllers
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;

    public class BidController : IBidController
    {
        // TODO
        public IList<Bid> GetAllBidsByProductId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Bid> GetAllBidsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Bid> GetAllEarnedBids()
        {
            throw new NotImplementedException();
        }

        public Bid GetBidById(int bidId)
        {
            throw new NotImplementedException();
        }

        public bool IsBidWon(int bidId)
        {
            throw new NotImplementedException();
        }

        public void MakeBid(User user, Product product, int coins, DateTime dateOfCreated)
        {
            throw new NotImplementedException();
        }
    }
}
