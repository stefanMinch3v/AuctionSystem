namespace AuctionSystem.WcfService.Contracts
{
    using Models;
    using Models.DTOs;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IInvoiceService
    {
        [OperationContract(IsOneWay = true)]
        void CreateInvoice(User user, Product product);

        [OperationContract]
        InvoiceDto GetInvoiceByUserId(int userId);

        [OperationContract]
        IList<Invoice> GetAllInvoicesForUser(User user);
    }
}