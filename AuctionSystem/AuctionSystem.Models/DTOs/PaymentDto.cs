namespace AuctionSystem.Models.DTOs
{
    using Models.Enums;

    public class PaymentDto
    {
        public PaymentType Type { get; set; }

        public string PaymentTypeCode { get; set; }

        public string Username { get; set; }
    }
}
