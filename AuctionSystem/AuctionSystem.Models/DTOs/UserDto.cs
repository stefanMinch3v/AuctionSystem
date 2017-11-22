namespace AuctionSystem.Models.DTOs
{
    using Enums;
    using System;

    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string ZipCountryCity { get; set; }

        public int Coins { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsDeleted { get; set; }

        public string Bids { get; set; }

        public string Invoices { get; set; }

        public string Payments { get; set; }
    }
}
