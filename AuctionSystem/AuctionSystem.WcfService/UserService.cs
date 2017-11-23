namespace AuctionSystem.WcfService
{
    using AutoMapper;
    using Contracts;
    using Controllers;
    using Models;
    using Models.DTOs;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        public void CreateUser(User user)
        {
            UserController.Instance().CreateUser(user);
        }

        public bool UpdateUser(User user, string property, string value)
        {
            return UserController.Instance().UpdateUser(user, property, value);
        }

        public UserDto GetUserById(int id)
        {
            var dbUser = UserController.Instance().GetUserByIdWithAllCollections(id);

            return MapDbUserToUserDto(dbUser);
        }

        public bool DeleteUser(User user)
        {
            return UserController.Instance().DeleteUser(user);
        }

        public bool IsUserExisting(User user)
        {
            return UserController.Instance().IsUserExisting(user);
        }

        public int CountUserBidsForGivenProduct(User user, Product product)
        {
            return UserController.Instance().CountUserBidsForGivenProduct(user, product);
        }

        public IList<Product> GetUserProducts(User user)
        {
            return UserController.Instance().GetUserProducts(user);
        }

        public IList<Bid> GetUserBids(User user)
        {
            return UserController.Instance().GetUserBids(user);
        }

        public IList<Invoice> GetUserInvoices(User user)
        {
            return UserController.Instance().GetUserInvoices(user);
        }

        private UserDto MapDbUserToUserDto(User dbUser)
        {
            return Mapper.Map<UserDto>(dbUser);
        }
    }
}
