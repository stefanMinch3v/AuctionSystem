namespace AuctionSystem.Controllers
{
    using Data;
    using Interfaces;
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

        public void CreateUser(string username, string name, string address, string email, string phone, DateTime dateOfBirth, Gender gender, Zip zip, int coins, List<Payment> payments)
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
                    Payments = payments
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

                if(user == null)
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

        public IList<Invoice> GetUserInvoices(User user)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetUserProducts(int userId)  // TO DO
        {
            using (var db = new AuctionContext())
            {
                var user = GetUserById(userId);

                
            }
        }

        public bool IsUserExisting(string username)
        {
            throw new NotImplementedException();
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
                            break; // WHAT DEFAULT TO PUT ?
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
