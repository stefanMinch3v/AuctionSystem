namespace AuctionSystem.Models.DTOs
{
    using System;

    public class BidDto
    {
        public int UserId { get; set; } 
        
        public string Username { get; set; }

        public int ProductId { get; set; }
        
        public string ProductName { get; set; }

        public int Coins { get; set; }
        
        public bool IsWon { get; set; }

        public DateTime DateOfCreated { get; set; }
    }
}
