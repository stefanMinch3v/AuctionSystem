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
            var invoiceDb = InvoiceController.Instance().GetInvoiceInlcudeCollections(userId);

            return MapDbInvoiceToInvoiceDto(invoiceDb);
        }

        public InvoiceDto MapDbInvoiceToInvoiceDto(Invoice invoice)
        {
            return Mapper.Map<InvoiceDto>(invoice);
        }

        public IList<Invoice> GetAllInvoicesForUser(User user)
        {
            return InvoiceController.Instance().GetAllInvoicesForUser(user);
        }
    }
}