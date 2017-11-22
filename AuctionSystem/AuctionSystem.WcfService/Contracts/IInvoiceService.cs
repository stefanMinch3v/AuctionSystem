namespace AuctionSystem.WcfService.Contracts
{
    using Models;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IInvoiceService
    {
        [OperationContract(IsOneWay = true)]
        void CreateInvoice(User user, Product product);

        [OperationContract]
        Invoice GetInvoiceByUserId(User user);

        [OperationContract]
        IList<Invoice> GetAllInvoicesForUser(User user);
    }
}