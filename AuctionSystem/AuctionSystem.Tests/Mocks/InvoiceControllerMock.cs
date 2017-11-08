namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Interfaces;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public IList<Invoice> GetAllInvoicesForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Invoice GetInvoiceByProductId(int id)
        {
            throw new NotImplementedException();
        }

        public Invoice GetInvoiceByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
