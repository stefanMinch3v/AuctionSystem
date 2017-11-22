namespace AuctionSystem.WcfService.Interfaces
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
        void CreateUser(string username, string password, string name, string address, string email, string phone, string dateOfBirth, Gender gender, int zipId, int coins, List<Payment> payments);

        [OperationContract]
        bool UpdateUser(int userId, string property, string value);

        [OperationContract]
        UserDto GetUserById(int id);

        [OperationContract]
        bool DeleteUser(int userId);

        [OperationContract]
        bool IsUserExisting(string username);

        [OperationContract]
        int CountUserBidsForGivenProduct(int userId, int productId);

        [OperationContract]
        IList<Product> GetUserProducts(int userId);

        [OperationContract]
        IList<Bid> GetUserBids(int userId);

        [OperationContract]
        IList<Invoice> GetUserInvoices(int userId);
    }
}
