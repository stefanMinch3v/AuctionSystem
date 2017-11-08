namespace AuctionSystem.Models
{
    using AuctionSystem.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        public int ZipId { get; set; }

        public Zip Zip { get; set; }

        public int Coins { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Bid> Products { get; set; } = new List<Bid>();

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
