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

        public void CreateInvoice(User user, Product product)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));

            using (var db = new AuctionContext())
            {
                var invoice = new Invoice
                {
                    UserId = user.Id,
                    ProductId = product.Id
                };

                db.Invoices.Add(invoice);
                db.SaveChanges();
            }
        }
        public IList<Invoice> GetAllInvoicesForUser(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = new AuctionContext())
            {
                return db.Invoices.Where(u => u.UserId == user.Id).ToList();
            }
        }

        public Invoice GetInvoiceByProductId(Product product)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));

            using (var db = new AuctionContext())
            {
                return db.Invoices.FirstOrDefault(p => p.ProductId == product.Id);
            }
        }

        public Invoice GetInvoiceByUserId(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = new AuctionContext())
            {
                return db.Invoices.FirstOrDefault(u => u.UserId == user.Id);
            }
        }
    }
}

