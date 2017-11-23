namespace AuctionSystem.Models.DTOs
{
    using System;
    using System.Collections.Generic;

    public class ProductDto
    {
        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }
}
