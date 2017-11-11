namespace AuctionSystem.Controllers
{
    using Data;
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
            throw new NotImplementedException();
        }

        public int GetAllUserSpentCoinsForGivenProduct(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(User user)
        {
            using (var db = new AuctionContext())
            {
                var user = GetUserById(id);

                if(user == null)
                {
                    return false;
                }

        public IList<Bid> GetUserBids(User user)
        {
            throw new NotImplementedException();
        }

            public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(User user)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
