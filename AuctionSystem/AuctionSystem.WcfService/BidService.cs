namespace AuctionSystem.WcfService
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using AuctionSystem.Models;

    public class BidService : IBidService
    {
        // TODO in each method return the controller that supposed to handle its operation

        public IList<Bid> GetAllBidsByProductId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Bid> GetAllBidsByUserId(int id)
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
