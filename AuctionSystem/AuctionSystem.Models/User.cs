namespace AuctionSystem.Models
{
    using AuctionSystem.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string Username { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public Gender Gender { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        [Required]
        public string Email { get; set; }

        [DataMember]
        [Required]
        public string Address { get; set; }

        [DataMember]
        public int ZipId { get; set; }

        [DataMember]
        public virtual Zip Zip { get; set; }

        [DataMember]
        public int Coins { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

        [DataMember]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        [DataMember]
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
