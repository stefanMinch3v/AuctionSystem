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
        public void CreateInvoice(int userId, int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));
            using (dbContext)
            {
                var invoice = new Invoice
                {
                    UserId = userId,
                    ProductId = productId
                };

                dbContext.Invoices.Add(invoice);
                dbContext.SaveChanges();
            }
        }

        public IList<Invoice> GetAllInvoicesForUser(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            using (this.dbContext)
            {
                return dbContext.Invoices.Where(u => u.UserId == userId).ToList();
            }
        }

        public Invoice GetInvoiceByProductId(int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));
            using (this.dbContext)
            {
                return this.dbContext.Invoices.FirstOrDefault(p => p.ProductId == productId);
            }
        }

        public Invoice GetInvoiceByUserId(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            using (this.dbContext)
            {
                return dbContext.Invoices.FirstOrDefault(u => u.UserId == userId);
            }
        }
    }
}
