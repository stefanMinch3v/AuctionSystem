namespace AuctionSystem.Controllers
{
    using Common;
    using Data;
    using Contracts;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class InvoiceController : IInvoiceController
    {
        private static InvoiceController instance;

        private InvoiceController()
        {
        }

        public static InvoiceController Instance()
        {
            if (instance == null)
            {
                instance = new InvoiceController();
            }

            return instance;
        }

        public void CreateInvoice(int userId, int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

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
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            using (var db = new AuctionContext())
            {
                return db.Invoices.Where(u => u.UserId == userId).ToList();
            }
        }

        public Invoice GetInvoiceByProductId(int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

            using (var db = new AuctionContext())
            {
                return db.Invoices.FirstOrDefault(p => p.ProductId == productId);
            }
        }

        public Invoice GetInvoiceByUserId(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            using (var db = new AuctionContext())
            {
                return db.Invoices.FirstOrDefault(u => u.UserId == userId);
            }
        }
    }
}

