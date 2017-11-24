namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IInvoiceController
    {
        void CreateInvoice(User user, Product product);

        Invoice GetInvoiceByUserId(int userId);

        Invoice GetInvoiceByProductId(int product);

        IList<Invoice> GetAllInvoicesForUser(User user);
    }
}
