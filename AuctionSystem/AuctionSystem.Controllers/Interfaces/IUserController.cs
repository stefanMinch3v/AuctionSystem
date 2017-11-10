namespace AuctionSystem.Controllers.Interfaces
{
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;

    public interface IUserController
    {
        void CreateUser(string username, string name, string address, string email, string phone, DateTime dateOfBirth, Gender gender, bool isAdmin, Zip zip, int coins, List<Payment> payments);

        bool UpdateUser(User user, string property, string value);

        bool DeleteUser(User user);

        User GetUserByUsername(string username);

        User GetUserById(int id);

        bool IsUserExisting(string username);

        int CountUserBidsForGivenProduct(int userId, int productId);

        IList<Product> GetUserProducts(User user);

        IList<Bid> GetUserBids(int userId);

        IList<Invoice> GetUserInvoices(int userId);
    }
}
