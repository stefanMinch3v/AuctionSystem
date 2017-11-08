namespace AuctionSystem.Models
{
    using AuctionSystem.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class Payment
    {
        public int Id { get; set; }

        public PaymentType Type { get; set; }

        [Required]
        public string PaymentTypeCode { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
