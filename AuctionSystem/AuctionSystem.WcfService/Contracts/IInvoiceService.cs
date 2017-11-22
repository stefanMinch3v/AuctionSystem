namespace AuctionSystem.WcfService.Contracts
{
    using Models;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IInvoiceService
    {
        [OperationContract]
        void CreateInvoice(int userId, int productId);

        [OperationContract]
        Invoice GetInvoiceByUserId(int id);

        [OperationContract]
        IList<Invoice> GetAllInvoicesForUser(int userId);
    }
}
