namespace AuctionSystem.WcfService
{
    using AutoMapper;
    using Controllers;
    using Interfaces;
    using Models;
    using Models.DTOs;
    using Models.Enums;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        public int CountUserBidsForGivenProduct(int userId, int productId)
        {
            return UserController.Instance().CountUserBidsForGivenProduct(userId, productId);
        }

        public void CreateUser(string username, string password, string name, string address, string email, string phone, string dateOfBirth, Gender gender, int zipId, int coins, List<Payment> payments)
        {
            var controller = UserController.Instance();
            controller.CreateUser(username, password, name, address, email, phone, dateOfBirth, gender, zipId, coins, payments);
        }

        public UserDto GetUserById(int id)
        {
            var dbUser = UserController.Instance().GetUserByIdWithAllCollections(id);
            
            return MapDbUserToUserDto(dbUser);
        }

        private UserDto MapDbUserToUserDto(User dbUser)
        {
            return Mapper.Map<UserDto>(dbUser);
        }

        public bool DeleteUser(int userId)
        {
            return UserController.Instance().DeleteUser(userId);
        }

        public IList<Bid> GetUserBids(int userId)
        {
            return UserController.Instance().GetUserBids(userId);
        }

        public IList<Invoice> GetUserInvoices(int userId)
        {
            return UserController.Instance().GetUserInvoices(userId);
        }

        public IList<Product> GetUserProducts(int userId)
        {
            return UserController.Instance().GetUserProducts(userId);
        }

        public bool IsUserExisting(string username)
        {
            return UserController.Instance().IsUserExisting(username);
        }

        public bool UpdateUser(int userId, string property, string value)
        {
            return UserController.Instance().UpdateUser(userId, property, value);
        }
    }
}
