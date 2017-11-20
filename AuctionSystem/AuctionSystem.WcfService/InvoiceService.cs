namespace AuctionSystem.WcfService
{
    using AuctionSystem.Controllers;
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;

    public class InvoiceService : IInvoiceService
    {
        

        public void CreateInvoice(int userId, int productId)
        {
            var controller = new InvoiceController();
            controller.CreateInvoice(userId,productId);
        }

        public IList<Invoice> GetAllInvoicesForUser(int userId)
        {
            return new InvoiceController().GetAllInvoicesForUser(userId);
        }

        public Invoice GetInvoiceByUserId(int id)
        {
            return new InvoiceController().GetInvoiceByUserId(id);
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
