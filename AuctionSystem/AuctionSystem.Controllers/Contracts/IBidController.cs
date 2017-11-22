namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IBidController
    {
        void MakeBid(User user, Product product, int coins);

        bool IsBidWon(Bid bid);

        Bid GetBidById(int bidId);

        IList<Bid> GetAllBidsByUserId(User user);

        IList<Bid> GetAllBidsByProductId(Product product);

        IList<Bid> GetAllEarnedBids();
    }
}
