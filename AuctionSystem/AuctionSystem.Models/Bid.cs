namespace AuctionSystem.Models
{
    using System;

    // TODO timestamp
    public class Bid
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Coins { get; set; }

        public bool IsWon { get; set; }

        public DateTime DateOfCreated { get; set; }
    }
}
