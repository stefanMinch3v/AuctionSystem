namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Interfaces;
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
            using (this.dbContext)
            {
                return GetAllInvoicesForUser(userId).ToList();
            }
        }

        public Invoice GetInvoiceByProductId(int id)
        {
            using (this.dbContext)
            {
                return this.dbContext.Invoices.FirstOrDefault(p => p.ProductId == id);
            }
        }

        public Invoice GetInvoiceByUserId(int id)
        {
            using (this.dbContext)
            {
                return dbContext.Invoices.FirstOrDefault(u => u.UserId == id);
            }
        }
    }
}
