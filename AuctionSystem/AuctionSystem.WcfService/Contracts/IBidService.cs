namespace AuctionSystem.WcfService.Contracts
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IBidService
    {
        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        [FaultContract(typeof(FaultException))]
        [FaultContract(typeof(ArgumentNullException))]
        void MakeBid(int userId, int productId, int coins);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        [FaultContract(typeof(FaultException))]
        [FaultContract(typeof(ArgumentNullException))]
        bool IsBidWon(Bid bid);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        [FaultContract(typeof(FaultException))]
        [FaultContract(typeof(ArgumentNullException))]
        BidDto GetBidById(int bidId);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        [FaultContract(typeof(FaultException))]
        [FaultContract(typeof(ArgumentNullException))]
        IList<BidDto> GetAllBidsByProductName(string name);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        [FaultContract(typeof(FaultException))]
        [FaultContract(typeof(ArgumentNullException))]
        IList<BidDto> GetAllBidsByUserId(int userId);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        [FaultContract(typeof(FaultException))]
        [FaultContract(typeof(ArgumentNullException))]
        IList<BidDto> GetAllBidsByProductId(int productId);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        [FaultContract(typeof(FaultException))]
        [FaultContract(typeof(ArgumentNullException))]
        IList<Bid> GetAllEarnedBids();

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        [FaultContract(typeof(FaultException))]
        [FaultContract(typeof(ArgumentNullException))]
        bool SetWinnersForProducts();

        [OperationContract]
        bool CheckCoinsValid(int productId, double coins);

        [OperationContract]
        bool BidExpired(int productId);
    }
}