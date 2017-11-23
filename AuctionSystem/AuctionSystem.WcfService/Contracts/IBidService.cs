namespace AuctionSystem.WcfService.Contracts
{
    using Models;
    using Models.DTOs;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IBidService
    {
        [OperationContract(IsOneWay = true)]
        void MakeBid(User user, Product product, int coins);

        [OperationContract]
        bool IsBidWon(Bid bid);

        [OperationContract]
        BidDto GetBidById(int bidId);

        [OperationContract]
        IList<Bid> GetAllBidsByUserId(User user);

        [OperationContract]
        IList<Bid> GetAllBidsByProductId(Product product);

        [OperationContract]
        IList<Bid> GetAllEarnedBids();
    }
}