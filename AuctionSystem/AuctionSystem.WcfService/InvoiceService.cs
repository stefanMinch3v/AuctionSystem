namespace AuctionSystem.WcfService
{
    using AutoMapper;
    using Contracts;
    using Controllers;
    using Models;
    using Models.DTOs;
    using System.Collections.Generic;

    public class InvoiceService : IInvoiceService
    {
        public void CreateInvoice(User user, Product product)
        {
            InvoiceController.Instance().CreateInvoice(user, product);
        }

        public InvoiceDto GetInvoiceByUserId(int userId)
        {
            var invoiceDb = InvoiceController.Instance().GetInvoiceInlcudeCollectionByUserId(userId);

            return MapDbInvoiceToInvoiceDto(invoiceDb);
        }

        public InvoiceDto GetInvoiceByProductId(int productId)
        {
            var invoiceDb = InvoiceController.Instance().GetInvoiceInlcudeCollectionByProductId(productId);

            return MapDbInvoiceToInvoiceDto(invoiceDb);
        }

        public IList<Invoice> GetAllInvoicesForUser(User user)
        {
            return InvoiceController.Instance().GetAllInvoicesForUser(user);
        }

        public InvoiceDto MapDbInvoiceToInvoiceDto(Invoice invoice)
        {
            return Mapper.Map<InvoiceDto>(invoice);
        }
    }
}