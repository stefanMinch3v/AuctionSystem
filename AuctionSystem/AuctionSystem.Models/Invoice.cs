namespace AuctionSystem.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Invoice
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public virtual User User { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public virtual Product Product { get; set; }
    }
}
