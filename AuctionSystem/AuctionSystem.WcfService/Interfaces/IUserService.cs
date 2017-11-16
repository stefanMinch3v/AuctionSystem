namespace AuctionSystem.WcfService.Interfaces
{
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        void CreateUser(string username, string name, string address, string email, string phone, DateTime dateOfBirth, Gender gender, Zip zip, int coins, List<Payment> payments);

        [OperationContract]
        bool UpdateUser(User user, string property, string value);

        [OperationContract]
        bool DeleteUser(User user);

        [OperationContract]
        bool IsUserExisting(string username);

        [OperationContract]
        int CountUserBidsForGivenProduct(int userId, int productId);

        [OperationContract]
        IList<Product> GetUserProducts(User user);

        [OperationContract]
        IList<Bid> GetUserBids(User user);

        [OperationContract]
        IList<Invoice> GetUserInvoices(User user);
    }
}
