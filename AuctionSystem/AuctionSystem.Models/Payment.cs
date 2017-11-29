namespace AuctionSystem.Models
{
    using AuctionSystem.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class Payment
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public PaymentType Type { get; set; }

        [DataMember]
        [Required]
        public string PaymentTypeCode { get; set; }

        [DataMember]
        public int UserId { get; set; }

        //[DataMember]
        public virtual User User { get; set; }
    }
}
