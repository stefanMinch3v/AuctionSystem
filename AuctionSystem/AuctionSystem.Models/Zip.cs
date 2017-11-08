namespace AuctionSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class Zip
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string ZipCode { get; set; }

        [DataMember]
        [Required]
        public string Country { get; set; }

        [DataMember]
        [Required]
        public string City { get; set; }
    }
}
