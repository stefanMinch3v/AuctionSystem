namespace AuctionSystem.WcfService
{
    using AutoMapper;
    using Controllers;
    using Interfaces;
    using Models;
    using Models.DTOs;
    using System.Collections.Generic;

    public class BidService : IBidService
    {
        // TODO: fix all the return list methods (try to return list of bidDtos)

        public void MakeBid(int userId, int productId, int coins)
        {
            BidController.Instance().MakeBid(userId, productId, coins);
        }

        public bool IsBidWon(int bidId)
        {
            return BidController.Instance().IsBidWon(bidId);
        }

        public IList<Bid> GetAllBidsByUserId(int id)
        {
            return BidController.Instance().GetAllBidsByUserId(id);
        }

        public IList<Bid> GetAllBidsByProductId(int id)
        {
            return BidController.Instance().GetAllBidsByProductId(id);
        }

        public IList<Bid> GetAllEarnedBids()
        {
            return BidController.Instance().GetAllEarnedBids();
        }

        public BidDto GetBidById(int bidId)
        {
            var dbBid = BidController.Instance().GetBidByIdWithAllObjects(bidId);

            return MapDbBidToBidDto(dbBid);
        }

        private BidDto MapDbBidToBidDto(Bid dbBid)
        {
            return Mapper.Map<BidDto>(dbBid);
        }
    }
}
