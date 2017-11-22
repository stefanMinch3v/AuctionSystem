namespace AuctionSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class Zip
    {
        [DataMember]
        public int ZipId { get; set; }

        [DataMember]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string ZipCode { get; set; }

        [DataMember]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Country { get; set; }

        [DataMember]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string City { get; set; }
    }
}
