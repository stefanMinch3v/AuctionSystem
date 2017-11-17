namespace AuctionSystem.Controllers.Interfaces
{
    using Models;
    using System.Collections.Generic;

    public interface IBidController
    {
        void MakeBid(int userId, int productId, int coins);

        bool IsBidWon(int bidId);

        Bid GetBidById(int bidId);

        IList<Bid> GetAllBidsByUserId(int id);

        IList<Bid> GetAllBidsByProductId(int id);

        IList<Bid> GetAllEarnedBids();
    }
}
