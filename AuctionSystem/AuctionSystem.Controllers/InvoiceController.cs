namespace AuctionSystem.Controllers
{
    using Interfaces;
    using Models;
    using System.Collections.Generic;

    public class InvoiceController : IInvoiceController
    {
        // TODO
        public void CreateInvoice(int userId, int productId)
        {
            throw new System.NotImplementedException();
        }

        public IList<Invoice> GetAllInvoicesForUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Invoice GetInvoiceByProductId(int id)
        {
            throw new System.NotImplementedException();
        }

        public Invoice GetInvoiceByUserId(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
