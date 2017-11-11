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

        public bool DeleteUser(int id)
        {
            using (var db = new AuctionContext())
            {
                var user = GetUserById(id);

                if (user == null)
                {
                    return false;
                }

                db.Users.Remove(user);
                db.SaveChanges();

                return true;
            }
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

        public bool UpdateUser(int userId, string property, string value) // Need to add update option for the Zip
        {
            using (var db = new AuctionContext())
            {
                var user = GetUserById(userId);

                if (user != null)
                {
                    switch (property)
                    {
                        case "Phone":
                            user.Phone = value;
                            break;
                        case "Email":
                            user.Email = value;
                            break;
                        case "Address":
                            user.Address = value;
                            break;
                        case "Coins":
                            user.Coins = int.Parse(value);
                            break;
                        case "IsAdmin":
                            if (user.IsAdmin == false)
                            {
                                user.IsAdmin = true;
                            }
                            else
                            {
                                user.IsAdmin = true;
                            }
                            break;
                        case "IsDeleted":
                            if (user.IsDeleted == false)
                            {
                                user.IsDeleted = true;
                            }
                            else
                            {
                                user.IsDeleted = true;
                            }
                            break;
                        default:
                            
                                throw new Exception("There is no such property!");
                            
                    }
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }

}

