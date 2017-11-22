namespace AuctionSystem.Models.DTOs
{
    public class InvoiceDto
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
