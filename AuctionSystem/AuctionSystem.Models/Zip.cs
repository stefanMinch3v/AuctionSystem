namespace AuctionSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Zip
    {
        public int Id { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }
    }
}
