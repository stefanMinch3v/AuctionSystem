namespace AuctionSystem.WcfService.Contracts
{
    using Models;
    using Models.DTOs;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IBidService
    {
        [OperationContract]
        void MakeBid(int userId, int productId, int coins);

        [OperationContract]
        bool IsBidWon(Bid bid);

        [OperationContract]
        BidDto GetBidById(int bidId);

        [OperationContract]
        IList<BidDto> GetAllBidsByProductName(string name);

        [OperationContract]
        IList<BidDto> GetAllBidsByUserId(int userId);

        [OperationContract]
        IList<Bid> GetAllBidsByProductId(int productId);

        [OperationContract]
        IList<Bid> GetAllEarnedBids();
    }
}