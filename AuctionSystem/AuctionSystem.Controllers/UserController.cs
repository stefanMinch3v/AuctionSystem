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
            using (var db = new AuctionContext())
            {
                return GetUserById(userId).Bids.Select(b => b.ProductId == productId).Count();
            }
        }

        public int GetAllUserSpentCoinsForGivenProduct(int userId, int productId)
        {
            using (var db = new AuctionContext())
            {
                return GetUserById(userId).Bids
                                       .Where(b => b.ProductId == productId)
                                       .Sum(b => b.Coins);
            }
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
                return GetUserById(userId).Bids.ToList();
            }
        }

        public User GetUserById(int id)
        {
            using (var db = new AuctionContext())
            {
                return db.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public User GetUserByUsername(string username)
        {
            using (var db = new AuctionContext())
            {
                return db.Users.FirstOrDefault(u => u.Username == username);
            }
        }

        public IList<Invoice> GetUserInvoices(int userId)
        {
            using (var db = new AuctionContext())
            {
                return GetUserById(userId).Invoices.ToList();
            }
        }

        public IList<Product> GetUserProducts(User user)
        {
            using (var db = new AuctionContext())
            {
                var listProducts = user.Bids
                                        .Where(b => b.UserId == user.Id)
                                        .Select(b => b.Product)
                                        .ToList();

                return listProducts;
            }
        }

        public bool IsUserExisting(string username)
        {
            using (var db = new AuctionContext())
            {
                return db.Users.Any(u => u.Username == username);
            }
        }

        public bool UpdateUser(User user, string property, string value)
        {
            throw new NotImplementedException();
        }
    }
}
