namespace AuctionSystem.Models.DTOs
{
    using Models.Enums;

    public class PaymentDto
    {
        public int Id { get; set; }

        public PaymentType Type { get; set; }

        public string PaymentTypeCode { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
