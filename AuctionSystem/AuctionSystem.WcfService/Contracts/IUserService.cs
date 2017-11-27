namespace AuctionSystem.WcfService.Contracts
{
    using Models;
    using Models.DTOs;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        void CreateUser(User user);

        [OperationContract]
        bool UpdateUser(UserDto user);

        [OperationContract]
        UserDto GetUserById(int id);

        [OperationContract]
        bool DeleteUser(User user);

        [OperationContract]
        bool IsUserExisting(string username);

        [OperationContract]
        int CountUserBidsForGivenProduct(User user, string productName);

        [OperationContract]
        int GetAllUserSpentCoinsForGivenProduct(User user, string productName);

        [OperationContract]
        IList<ProductDto> GetUserProducts(User user);

        [OperationContract]
        IList<BidDto> GetUserBids(User user);

        [OperationContract]
        IList<InvoiceDto> GetUserInvoices(User user);

        [OperationContract]
        UserDto GetUserByUsername(string username);
    }
}
