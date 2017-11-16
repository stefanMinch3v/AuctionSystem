namespace AuctionSystem.Controllers
{
    using Common;
    using Data;
    using Interfaces;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserController : IUserController
    {
        
        public int CountUserBidsForGivenProduct(int userId, int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

            using (var db = new AuctionContext())
            {
                return GetUserById(userId).Bids
                                            .Select(b => b.ProductId == productId).Count();
            }
        }

        public int GetAllUserSpentCoinsForGivenProduct(int userId, int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

            using (var db = new AuctionContext())
            {
                return GetUserById(userId).Bids
                                       .Where(b => b.ProductId == productId)
                                       .Sum(b => b.Coins);
            }
        }

        public void CreateUser(string username, string password, string name, string address, string email, string phone, string dateOfBirth, Gender gender, bool isAdmin, Zip zip, int coins, List<Payment> payments)
        {
            CoreValidator.ThrowIfNullOrEmpty(username, nameof(username));
            CoreValidator.ThrowIfNullOrEmpty(password, nameof(password));
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNullOrEmpty(address, nameof(address));
            CoreValidator.ThrowIfNullOrEmpty(email, nameof(email));
            CoreValidator.ThrowIfNullOrEmpty(phone, nameof(phone));
            CoreValidator.ThrowIfDateIsNotCorrect(dateOfBirth, nameof(dateOfBirth));
            CoreValidator.ThrowIfNegativeOrZero(coins, nameof(coins));

            var dateParsed = DateTime.Parse(dateOfBirth);
            if (dateParsed > DateTime.Now.AddYears(-18))
            {
                throw new ArgumentException($"Date of birth is not valid, the customer must be adult.");
            }

            using (var db = new AuctionContext())
            {
                var user = new User
                {
                    Username = username,
                    Password = password,
                    Name = name,
                    Address = address,
                    Email = email,
                    Phone = phone,
                    DateOfBirth = dateParsed,
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
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

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
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            using (var db = new AuctionContext())
            {
                return GetUserById(userId).Bids.ToList();
            }
        }

        public User GetUserById(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

            using (var db = new AuctionContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    throw new ArgumentNullException($"No such user in the system.");
                }

                return user;
            }
        }

        public User GetUserByUsername(string username)
        {
            CoreValidator.ThrowIfNullOrEmpty(username, nameof(username));

            using (var db = new AuctionContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentNullException($"{nameof(username)} does not exist in the system.");
                }

                return user;
            }
        }

        public IList<Invoice> GetUserInvoices(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            using (var db = new AuctionContext())
            {
                return GetUserById(userId).Invoices.ToList();
            }
        }

        public IList<Product> GetUserProducts(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));

            using (var db = new AuctionContext())
            {
                return user.Bids
                                .Where(b => b.UserId == user.Id)
                                .Select(b => b.Product)
                                .ToList();
            }
        }

        public bool IsUserExisting(string username)
        {
            CoreValidator.ThrowIfNullOrEmpty(username, nameof(username));

            using (var db = new AuctionContext())
            {
                return db.Users.Any(u => u.Username == username);
            }
        }

        public bool UpdateUser(int userId, string property, string value)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            CoreValidator.ThrowIfNullOrEmpty(property, nameof(property));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            using (var db = new AuctionContext())
            {
                var user = GetUserById(userId);

                CoreValidator.ThrowIfNull(user, nameof(user));

                db.Users.Attach(user);

                switch (property.ToLower())
                {
                    case "phone":
                        user.Phone = value;
                        break;
                    case "email":
                        user.Email = value;
                        break;
                    case "address":
                        user.Address = value;
                        break;
                    case "coins":
                        if (!Int32.TryParse(value, out int temp))
                        {
                            throw new ArgumentException("Invalid parameter for coins.");
                        }

                        var parsedValue = int.Parse(value);

                        CoreValidator.ThrowIfNegativeOrZero(parsedValue, nameof(parsedValue));

                        user.Coins = parsedValue;
                        break;
                    case "isadmin":
                        if (user.IsAdmin)
                        {
                            user.IsAdmin = false;
                        }
                        else
                        {
                            user.IsAdmin = true;
                        }
                        break;
                    case "isdeleted":
                        if (user.IsDeleted)
                        {
                            user.IsDeleted = false;
                        }
                        else
                        {
                            user.IsDeleted = true;
                        }
                        break;
                    default:
                        throw new ArgumentException("No such property.");
                }


                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
        }
    }

}

