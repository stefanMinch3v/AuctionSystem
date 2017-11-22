namespace AuctionSystem.WcfService
{
    using Controllers;
    using Contracts;
    using Models;
    using System.Collections.Generic;

    public class InvoiceService : IInvoiceService
    {
        public void CreateInvoice(User user, Product product)
        {
            InvoiceController.Instance().CreateInvoice(user, product);
        }

        public Invoice GetInvoiceByUserId(User user)
        {
            return InvoiceController.Instance().GetInvoiceByUserId(user);
        }

        public IList<Invoice> GetAllInvoicesForUser(User user)
        {
            return InvoiceController.Instance().GetAllInvoicesForUser(user);
        }
    }
}