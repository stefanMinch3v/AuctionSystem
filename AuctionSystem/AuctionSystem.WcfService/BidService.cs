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

        public void MakeBid(int userId, int productId, int coins)
        {
            BidController.Instance().MakeBid(userId, productId, coins);
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

        public IList<BidDto> GetAllBidsByUserId(int userId)
        {
            var bids = BidController.Instance().GetAllBidsByUserId(userId);

            return TransferCollectionData(bids);
        }

        public IList<Bid> GetAllBidsByProductId(int productId)
        {
            return BidController.Instance().GetAllBidsByProductId(productId);
        }

        public IList<Bid> GetAllEarnedBids()
        {
            return BidController.Instance().GetAllEarnedBids();
        }

        public IList<BidDto> GetAllBidsByProductName(string name)
        {
            var bids = BidController.Instance().GetAllBidsByProductName(name);

            return TransferCollectionData(bids);
        }

        private BidDto MapDbBidToBidDto(Bid dbBid)
        {
            return Mapper.Map<BidDto>(dbBid);
        }

        private IList<BidDto> TransferCollectionData(IList<Bid> bids)
        {
            var result = new List<BidDto>();

            foreach (var bid in bids)
            {
                var bidDto = MapDbBidToBidDto(bid);
                result.Add(bidDto);
            }

            return result;
        }
    }
}
