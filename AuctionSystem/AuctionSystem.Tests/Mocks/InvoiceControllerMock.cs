namespace AuctionSystem.Tests.Mocks
{
    using AuctionSystem.Controllers.Common;
    using Controllers.Contracts;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class InvoiceControllerMock : IInvoiceController
    {
        private readonly AuctionContext dbContext;

        public InvoiceControllerMock(AuctionContext dbContext)

        {
            this.dbContext = dbContext;
        }


        // TODO
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

        public Invoice GetInvoiceByProductId(Product product)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));

            using (var db = dbContext)
            {
                return db.Invoices.FirstOrDefault(p => p.ProductId == product.Id);
            }
        }

        public Invoice GetInvoiceByUserId(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = dbContext)
            {
                return db.Invoices.FirstOrDefault(u => u.UserId == user.Id);
            }
        }
    }
}
