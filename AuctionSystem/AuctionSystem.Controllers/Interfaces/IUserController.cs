namespace AuctionSystem.Controllers.Interfaces
{
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;

    public interface IUserController
    {
        void CreateUser(string username, string name, string address, string email, string phone, DateTime dateOfBirth, Gender gender, Zip zip, int coins, List<Payment> payments);

        bool UpdateUser(int userId, string property, string value);

        bool DeleteUser(int userId);

        User GetUserByUsername(User user);

        User GetUserById(int id);

        bool IsUserExisting(string username);

        int CountUserBidsForGivenProduct(int userId, int productId);

        IList<Product> GetUserProducts(User user);

        IList<Bid> GetUserBids(User user);

        IList<Invoice> GetUserInvoices(User user);
    }
}
