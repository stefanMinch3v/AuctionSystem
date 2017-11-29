namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IUserController
    {
        void CreateUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(User user);

        User GetUserByUsername(string username);

        User GetUserById(int id);

        bool IsUserExisting(string username);

        int CountUserBidsForGivenProduct(User user, string productName);

        int GetAllUserSpentCoinsForGivenProduct(User user, string productName);

        IList<Product> GetUserProducts(User user);

        IList<Bid> GetUserBids(User user);

        IList<Invoice> GetUserInvoices(User user);

        bool isCookieValid(string cookieId);
    }
}
