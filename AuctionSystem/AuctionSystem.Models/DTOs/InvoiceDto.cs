namespace AuctionSystem.Models.DTOs
{
    using System;

    public class InvoiceDto
    {
        public string User { get; set; }

        public ProductDto Product { get; set; }

        public decimal Price { get; set; }

        public DateTime DateOfIssued { get; set; }
    }
}
