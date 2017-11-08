namespace AuctionSystem.Controllers.Interfaces
{
    using Models;
    using System.Collections.Generic;

    public interface IInvoiceController
    {
        void CreateInvoice(int userId, int productId);

        Invoice GetInvoiceByUserId(int id);

        Invoice GetInvoiceByProductId(int id);

        IList<Invoice> GetAllInvoicesForUser(int userId);
    }
}
