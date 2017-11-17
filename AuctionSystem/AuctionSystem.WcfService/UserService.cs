namespace AuctionSystem.WcfService
{
    using AuctionSystem.Controllers;
    using Interfaces;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        public int CountUserBidsForGivenProduct(int userId, int productId)
        {
            return new UserController().CountUserBidsForGivenProduct(userId, productId);
        }

        public void CreateUser(string username, string password, string name, string address, string email, string phone, string dateOfBirth, Gender gender, int zipId, int coins, List<Payment> payments)
        {
            var controller = new UserController();
            controller.CreateUser(username, password, name, address, email, phone, dateOfBirth, gender, zipId, coins, payments);
        }

        public User GetUserById(int id)
        {
            var dbUser = new UserController().GetUserById(id);
            
            return TransferDbObjectToRegularObject(dbUser);
        }

        private User TransferDbObjectToRegularObject(User dbUser)
        {
            var newUser = new User
            {
                Id = dbUser.Id,
                Gender = dbUser.Gender,
                Address = dbUser.Address,
                Coins = dbUser.Coins,
                DateOfBirth = dbUser.DateOfBirth,
                Email = dbUser.Email,
                IsAdmin = dbUser.IsAdmin,
                IsDeleted = dbUser.IsDeleted,
                Name = dbUser.Name,
                Password = dbUser.Password,
                Phone = dbUser.Phone,
                Username = dbUser.Username
            };

            return newUser;
        }

        public bool DeleteUser(int userId)
        {
            return new UserController().DeleteUser(userId);
        }

        public IList<Bid> GetUserBids(int userId)
        {
            return new UserController().GetUserBids(userId);
        }

        public IList<Invoice> GetUserInvoices(int userId)
        {
            return new UserController().GetUserInvoices(userId);
        }

        public IList<Product> GetUserProducts(int userId)
        {
            return new UserController().GetUserProducts(userId);
        }

        public bool IsUserExisting(string username)
        {
            return new UserController().IsUserExisting(username);
        }

        public bool UpdateUser(int userId, string property, string value)
        {
            return new UserController().UpdateUser(userId, property, value);
        }
    }
}
