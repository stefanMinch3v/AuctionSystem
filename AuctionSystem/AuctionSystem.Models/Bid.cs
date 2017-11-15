namespace AuctionSystem.Models
{
    using System;
    using System.Runtime.Serialization;

    // TODO timestamp with fluent API

    [DataContract]
    public class Bid
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public virtual User User { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public virtual Product Product { get; set; }

        [DataMember]
        public int Coins { get; set; }

        [DataMember]
        public bool IsWon { get; set; }

        [DataMember]
        public DateTime DateOfCreated { get; set; }
    }
}
