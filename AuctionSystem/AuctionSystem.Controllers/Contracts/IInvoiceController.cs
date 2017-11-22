namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IInvoiceController
    {
        void CreateInvoice(User user, Product product);

        Invoice GetInvoiceByUserId(User user);

        IList<Invoice> GetAllInvoicesForUser(User user);
        
        Invoice GetInvoiceByProductId(Product product);
    }
}
