namespace AuctionSystem.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using Utility;

    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        [Index("Username", IsUnique = true)]
        [MinLength(3)]
        [MaxLength(30)]
        public string Username { get; set; }

        [DataMember]
        [Required]
        [Password(5, 100)]
        public string Password { get; set; }

        [DataMember]
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public Gender Gender { get; set; }

        [DataMember]
        [MinLength(5)]
        [MaxLength(40)]
        public string Phone { get; set; }

        [DataMember]
        [Required]
        [Email]
        public string Email { get; set; }

        [DataMember]
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Address { get; set; }

        [DataMember]
        public int? ZipId { get; set; }

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
