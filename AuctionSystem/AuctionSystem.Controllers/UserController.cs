namespace AuctionSystem.Controllers
{
    using Interfaces;
    using Data;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserController : IUserController
    {
        // TODO
        public int CountUserBidsForGivenProduct(int userId, int productId)
        {
            // needs product controller finished
            return 0;
        }

        public void CreateUser(string username, string name, string address, string email, string phone, DateTime dateOfBirth, Gender gender, bool isAdmin, Zip zip, int coins, List<Payment> payments)
        {
            using (var db = new AuctionContext())
            {
                var user = new User
                {
                    Username = username,
                    Name = name,
                    Address = address,
                    Email = email,
                    Phone = phone,
                    DateOfBirth = dateOfBirth,
                    Gender = gender,
                    Zip = zip,
                    Coins = coins,
                    Payments = payments,
                    IsAdmin = isAdmin,
                    IsDeleted = false
                };

                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public IList<Bid> GetUserBids(int userId)
        {
            using (var db = new AuctionContext())
            {
                var userBids = GetUserById(userId).Products.ToList();

                return userBids;
            }
        }

        public User GetUserById(int id)
        {
            using (var db = new AuctionContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);

                return user;
            }
        }

        public User GetUserByUsername(string username)
        {
            using (var db = new AuctionContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);

                return user;
            }
        }

        public IList<Invoice> GetUserInvoices(int userId)
        {
            using (var db = new AuctionContext())
            {
                var userInvoices = GetUserById(userId).Invoices.ToList();

                return userInvoices;
            }
        }

        public IList<Product> GetUserProducts(User user)
        {
            throw new NotImplementedException();
        }

        public bool IsUserExisting(string username)
        {
            using (var db = new AuctionContext())
            {
                var existingUser = db.Users.Any(u => u.Username == username);

                if (existingUser)
                {
                    return true;
                }

                return false;
            }
        }

        public bool UpdateUser(User user, string property, string value)
        {
            throw new NotImplementedException();
        }
    }
}
