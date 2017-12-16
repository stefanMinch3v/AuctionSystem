namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IBidController
    {
        void MakeBid(int userId, int productId, int coins);

        bool IsBidWon(Bid bid);

        Bid GetBidById(int bidId);

        IList<Bid> GetAllBidsByUserId(int userId);

        IList<Bid> GetAllBidsByProductId(int productId);

        IList<Bid> GetAllEarnedBids();

        IList<Bid> GetAllBidsByProductName(string name);

        bool CheckCoinsValid(int productId, double coins);

        bool BidExpired(int productId);
    }
}
