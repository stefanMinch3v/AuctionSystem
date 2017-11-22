namespace AuctionSystem.WcfService.Contracts
{
    using Models;
    using Models.DTOs;
    using Models.Enums;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        void CreateUser(User user);

        [OperationContract]
        bool UpdateUser(User user, string property, string value);

        [OperationContract]
        User GetUserById(int id);

        [OperationContract]
        bool DeleteUser(User user);

        [OperationContract]
        bool IsUserExisting(User user);

        [OperationContract]
        int CountUserBidsForGivenProduct(User user, Product product);

        [OperationContract]
        IList<Product> GetUserProducts(User user);

        [OperationContract]
        IList<Bid> GetUserBids(User user);

        [OperationContract]
        IList<Invoice> GetUserInvoices(User user);
    }
}
