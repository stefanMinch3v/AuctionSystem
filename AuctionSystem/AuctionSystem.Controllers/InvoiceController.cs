namespace AuctionSystem.Controllers
{
    using Interfaces;
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class InvoiceController : IInvoiceController
    {
        public void CreateInvoice(int userId, int productId)
        {
            using (var db = new AuctionContext())
            {
                var invoice = new Invoice
                {
                    UserId = userId,
                    ProductId = productId
                };

                db.Invoices.Add(invoice);
                db.SaveChanges();
            }
        }
        public IList<Invoice> GetAllInvoicesForUser(int userId)
        {
            using (var db = new AuctionContext())
            {
                return GetAllInvoicesForUser(userId).ToList();
            }
        }

        public Invoice GetInvoiceByProductId(int id)
        {
            using (var db = new AuctionContext())
            {
                return db.Invoices.FirstOrDefault(p => p.ProductId == id);
            }
        }

        public Invoice GetInvoiceByUserId(int id)
        {
            using (var db = new AuctionContext())
            {
                return db.Invoices.FirstOrDefault(u => u.UserId == id);
            }
        }
    }
}

