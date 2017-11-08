namespace AuctionSystem.Controllers.Interfaces
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IBidController
    {
        void MakeBid(User user, Product product, int coins, DateTime dateOfCreated);

        bool IsBidWon(int bidId);

        Bid GetBidById(int bidId);

        IList<Bid> GetAllBidsByUserId(int id);

        IList<Bid> GetAllBidsByProductId(int id);

        IList<Bid> GetAllEarnedBids();
    }
}
