namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Interfaces;
    using Data;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserControllerMock : IUserController
    {
        private readonly AuctionContext dbContext;

        public UserControllerMock(AuctionContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // TODO

        public int CountUserBidsForGivenProduct(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(string username, string name, string address, string email, string phone, DateTime dateOfBirth, Gender gender, bool isAdmin, Zip zip, int coins, List<Payment> payments)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public int GetAllUserSpentCoinsForGivenProduct(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public IList<Bid> GetUserBids(int userId)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            using (dbContext)
            {
                return dbContext.Users.FirstOrDefault(u => u.Username == username);
            }
        }

        public IList<Invoice> GetUserInvoices(int userId)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetUserProducts(User user)
        {
            throw new NotImplementedException();
        }

        public bool IsUserExisting(string username)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user, string property, string value)
        {
            throw new NotImplementedException();
        }
    }
}
