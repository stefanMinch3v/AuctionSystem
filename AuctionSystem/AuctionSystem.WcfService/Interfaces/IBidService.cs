namespace AuctionSystem.WcfService.Interfaces
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IBidService
    {
        [OperationContract]
        void MakeBid(User user, Product product, int coins, DateTime dateOfCreated);

        [OperationContract]
        bool IsBidWon(int bidId);

        [OperationContract]
        Bid GetBidById(int bidId);

        [OperationContract]
        IList<Bid> GetAllBidsByUserId(int id);

        [OperationContract]
        IList<Bid> GetAllBidsByProductId(int id);
    }
}
