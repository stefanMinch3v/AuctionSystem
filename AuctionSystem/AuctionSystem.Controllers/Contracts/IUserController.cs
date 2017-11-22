namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;

    public interface IUserController
    {
        void CreateUser(string username, string password, string name, string address, string email, string phone, string dateOfBirth, Gender gender, int zipId, int coins, List<Payment> payments);

        bool UpdateUser(int userId, string property, string value);

        bool DeleteUser(int userId);

        User GetUserByUsername(string username);

        User GetUserById(int id);

        bool IsUserExisting(string username);

        int CountUserBidsForGivenProduct(int userId, int productId);

        int GetAllUserSpentCoinsForGivenProduct(int userId, int productId);

        IList<Product> GetUserProducts(int userId);

        IList<Bid> GetUserBids(int userId);

        IList<Invoice> GetUserInvoices(int userId);
    }
}
