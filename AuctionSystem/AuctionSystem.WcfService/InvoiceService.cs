namespace AuctionSystem.WcfService
{
    using Controllers;
    using Contracts;
    using Models;
    using System.Collections.Generic;

    public class InvoiceService : IInvoiceService
    {
        public void CreateInvoice(int userId, int productId)
        {
            var controller = InvoiceController.Instance();
            controller.CreateInvoice(userId,productId);
        }

        public IList<Invoice> GetAllInvoicesForUser(int userId)
        {
            return InvoiceController.Instance().GetAllInvoicesForUser(userId);
        }

        public Invoice GetInvoiceByUserId(int id)
        {
            return InvoiceController.Instance().GetInvoiceByUserId(id);
        }

        private Invoice TransferDbObjectToRegularObject(Invoice dbInvoice)
        {
            var newInvoice = new Invoice
            {
                UserId = dbInvoice.UserId,
                ProductId = dbInvoice.ProductId
            };
            return newInvoice;
        }
    }
}
