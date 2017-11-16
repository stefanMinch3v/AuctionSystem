namespace AuctionSystem.WcfService
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;

    public class InvoiceService : IInvoiceService
    {
        // TODO in each method return the controller that supposed to handle its operation

        public void CreateInvoice(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public IList<Invoice> GetAllInvoicesForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Invoice GetInvoiceByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
