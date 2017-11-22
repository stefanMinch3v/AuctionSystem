namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;

    public interface IUserController
    {
        void CreateUser(User user);

        bool UpdateUser(User user, string property, string value);

        bool DeleteUser(User user);

        User GetUserByUsername(string username);

        User GetUserById(int id);

        bool IsUserExisting(User user);

        int CountUserBidsForGivenProduct(User user, Product product);

        int GetAllUserSpentCoinsForGivenProduct(User user, Product product);

        IList<Product> GetUserProducts(User user);

        IList<Bid> GetUserBids(User user);

        IList<Invoice> GetUserInvoices(User user);
    }
}
