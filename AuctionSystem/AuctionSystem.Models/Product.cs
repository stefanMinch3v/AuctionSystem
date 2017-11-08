namespace AuctionSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        public string Description { get; set; }

        [MaxLength(1024)]
        public byte?[] Image { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<Bid> Users { get; set; } = new List<Bid>();
    }
}
