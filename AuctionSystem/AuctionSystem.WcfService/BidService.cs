namespace AuctionSystem.WcfService
{
    using AutoMapper;
    using Controllers;
    using Contracts;
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    public class BidService : IBidService
    {
        // TODO: fix all the return list methods (try to return list of bidDtos)

        public void MakeBid(int userId, int productId, int coins)
        {
            try
            {
                BidController.Instance().MakeBid(userId, productId, coins);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool BidExpired(int productId)
        {
            try
            {
                return BidController.Instance().BidExpired(productId);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool IsBidWon(Bid bid)
        {
            try
            {
                return BidController.Instance().IsBidWon(bid);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public BidDto GetBidById(int bidId)
        {
            try
            {
                var bidDb = BidController.Instance().GetBidByIdWithAllObjects(bidId);

                return MapDbBidToBidDto(bidDb);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }

        }

        public IList<BidDto> GetAllBidsByUserId(int userId)
        {
            try
            {
                var bids = BidController.Instance().GetAllBidsByUserId(userId);

                return TransferCollectionData(bids);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }

        }

        public IList<BidDto> GetAllBidsByProductId(int productId)
        {
            try
            {
                var bids = BidController.Instance().GetAllBidsByProductId(productId);

                return TransferCollectionData(bids);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<Bid> GetAllEarnedBids()
        {
            try
            {
                return BidController.Instance().GetAllEarnedBids();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IList<BidDto> GetAllBidsByProductName(string name)
        {
            try
            {
                var bids = BidController.Instance().GetAllBidsByProductName(name);

                return TransferCollectionData(bids);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool SetWinnersForProducts()
        {
            try
            {
                return BidController.Instance().SetWinnersForProducts();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
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

        public bool CheckCoinsValid(int productId, double coins)
        {
            return BidController.Instance().CheckCoinsValid(productId, coins);
        }
    }
}
