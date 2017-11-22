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
        void MakeBid(int userId, int productId, int coins);

        [OperationContract]
        bool IsBidWon(int bidId);

        [OperationContract]
        BidDto GetBidById(int bidId);

        [OperationContract]
        IList<Bid> GetAllBidsByUserId(int id);

        [OperationContract]
        IList<Bid> GetAllBidsByProductId(int id);

        [OperationContract]
        IList<Bid> GetAllEarnedBids();
    }
}
