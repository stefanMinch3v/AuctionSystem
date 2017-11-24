namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Common;
    using Controllers.Contracts;
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class InvoiceControllerMock : IInvoiceController
    {
        private readonly AuctionContext dbContext;

        public InvoiceControllerMock(AuctionContext dbContext)

        {
            this.dbContext = dbContext;
        }

        public void CreateInvoice(User user, Product product)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));

            using (var db = dbContext)
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

            using (var db = dbContext)
            {
                return db.Invoices.Where(u => u.UserId == user.Id).ToList();
            }
        }

        public Invoice GetInvoiceByProductId(int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

            using (var db = dbContext)
            {
                return db.Invoices.FirstOrDefault(p => p.ProductId == productId);
            }
        }

        public Invoice GetInvoiceByUserId(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            using (var db = dbContext)
            {
                return db.Invoices.FirstOrDefault(u => u.UserId == userId);
            }
        }
    }
}
