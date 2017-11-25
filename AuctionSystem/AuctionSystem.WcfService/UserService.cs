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

        public bool UpdateUser(UserDto user)
        {
            var userToUpdate = MapUserDtoToDbUser(user);

            return UserController.Instance().UpdateUser(userToUpdate);
        }

        public UserDto GetUserById(int id)
        {
            var dbUser = UserController.Instance().GetUserByIdWithAllCollections(id);

            return MapDbUserToUserDto(dbUser);
        }

        public UserDto GetUserByUsername(string username)
        {
            var dbUser = UserController.Instance().GetUserByNameWithAllCollections(username);

            return MapDbUserToUserDto(dbUser);
        }

        public bool DeleteUser(User user)
        {
            return UserController.Instance().DeleteUser(user);
        }

        public bool IsUserExisting(string username)
        {
            return UserController.Instance().IsUserExisting(username);
        }

        public int CountUserBidsForGivenProduct(User user, string productName)
        {
            return UserController.Instance().CountUserBidsForGivenProduct(user, productName);
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

        public int GetAllUserSpentCoinsForGivenProduct(User user, string productName)
        {
            return UserController.Instance().GetAllUserSpentCoinsForGivenProduct(user, productName);
        }

        private UserDto MapDbUserToUserDto(User dbUser)
        {
            return Mapper.Map<UserDto>(dbUser);
        }

        private User MapUserDtoToDbUser(UserDto userDto)
        {
            return Mapper.Map<User>(userDto);
        }
    }
}
