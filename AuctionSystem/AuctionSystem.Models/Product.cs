namespace AuctionSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [DataMember]
        public bool IsAvailable { get; set; }

        [DataMember]
        [Required]
        [MinLength(2)]
        public string Description { get; set; }

        [DataMember]
        [MaxLength(1024)]
        public byte?[] Image { get; set; }

        [DataMember]
        [Required]
        public DateTime StartDate { get; set; }

        [DataMember]
        [Required]
        public DateTime EndDate { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }
}
