namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Common;
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

        public int CountUserBidsForGivenProduct(int userId, int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

            using (dbContext)
            {
                return GetUserById(userId).Bids
                                            .Select(b => b.ProductId == productId).Count();
            }
        }

        public int GetAllUserSpentCoinsForGivenProduct(int userId, int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

            using (dbContext)
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

            using (dbContext)
            {
                var user = new User
                {
                    Username = username,
                    Password = password,
                    Name = name,
                    Address = address,
                    Email = email,
                    Phone = phone,
                    DateOfBirth = DateTime.Parse(dateOfBirth),
                    Gender = gender,
                    Zip = zip,
                    Coins = coins,
                    Payments = payments,
                    IsAdmin = isAdmin,
                    IsDeleted = false
                };

                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
        }

        public bool DeleteUser(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

            using (dbContext)
            {
                var user = GetUserById(id);

                if (user == null)
                {
                    return false;
                }

                dbContext.Users.Remove(user);
                dbContext.SaveChanges();

                return true;
            }
        }


        public IList<Bid> GetUserBids(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            using (dbContext)
            {
                return GetUserById(userId).Bids.ToList();
            }
        }

        public User GetUserById(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

            using (dbContext)
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Id == id);

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

            using (dbContext)
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Username == username);

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

            using (dbContext)
            {
                return GetUserById(userId).Invoices.ToList();
            }
        }

        public IList<Product> GetUserProducts(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));

            using (dbContext)
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

            using (dbContext)
            {
                return dbContext.Users.Any(u => u.Username == username);
            }
        }

        public bool UpdateUser(int userId, string property, string value)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            CoreValidator.ThrowIfNullOrEmpty(property, nameof(property));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            using (dbContext)
            {
                var user = GetUserById(userId);

                CoreValidator.ThrowIfNull(user, nameof(user));

                dbContext.Users.Attach(user);

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


                dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();

                return true;
            }
        }
    }
}
