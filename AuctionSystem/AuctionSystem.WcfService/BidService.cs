namespace AuctionSystem.WcfService
{
    using AutoMapper;
    using Controllers;
    using Contracts;
    using Models;
    using Models.DTOs;
    using System.Collections.Generic;

    public class BidService : IBidService
    {
        // TODO: fix all the return list methods (try to return list of bidDtos)

        private BidDto MapDbBidToBidDto(Bid dbBid)
        {
            return Mapper.Map<BidDto>(dbBid);
        }

        public void MakeBid(User user, Product product, int coins)
        {
            BidController.Instance().MakeBid(user, product, coins);
        }

        public bool IsBidWon(Bid bid)
        {
            return BidController.Instance().IsBidWon(bid);
        }

        public BidDto GetBidById(int bidId)
        {
            var bidDb = BidController.Instance().GetBidByIdWithAllObjects(bidId);

            return MapDbBidToBidDto(bidDb);
        }

        public IList<Bid> GetAllBidsByUserId(User user)
        {
            return BidController.Instance().GetAllBidsByUserId(user);
        }

        public IList<Bid> GetAllBidsByProductId(Product product)
        {
            return BidController.Instance().GetAllBidsByProductId(product);
        }

        public IList<Bid> GetAllEarnedBids()
        {
            return BidController.Instance().GetAllEarnedBids();
        }
    }
}
