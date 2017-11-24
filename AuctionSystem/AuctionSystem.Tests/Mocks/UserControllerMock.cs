namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Common;
    using Controllers.Contracts;
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

        public int CountUserBidsForGivenProduct(User user, Product product)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));

            using (var db = dbContext)
            {
                var currentUser = GetUserById(user.Id);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                var bids = currentUser.Bids.ToList();

                if (bids.Count == 0)
                {
                    return 0;
                }

                return bids.Select(b => b.ProductId == product.Id).Count();
            }
        }

        public bool IsUserExistingById(int userId)
        {
            using (var db = dbContext)
            {
                return db.Users.Any(u => u.Id == userId);
            }
        }

        public int GetAllUserSpentCoinsForGivenProduct(User user, Product product)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));

            using (var db = dbContext)
            {
                var currentUser = GetUserById(user.Id);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser.Bids
                                       .Where(b => b.ProductId == product.Id)
                                       .Sum(b => b.Coins);
            }
        }

        public void CreateUser(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNullOrEmpty(user.Username, nameof(user.Username));
            CoreValidator.ThrowIfNullOrEmpty(user.Password, nameof(user.Password));
            CoreValidator.ThrowIfNullOrEmpty(user.Name, nameof(user.Name));
            CoreValidator.ThrowIfNullOrEmpty(user.Address, nameof(user.Address));
            CoreValidator.ThrowIfNullOrEmpty(user.Email, nameof(user.Email));
            CoreValidator.ThrowIfNullOrEmpty(user.Phone, nameof(user.Phone));
            CoreValidator.ThrowIfDateIsNotCorrect(user.DateOfBirth.ToString(), nameof(user.DateOfBirth));
            CoreValidator.SpecialThrowForCoinsIfValueIsNegativeOnly(user.Coins, nameof(user.Coins));

            var dateParsed = user.DateOfBirth;
            if (dateParsed > DateTime.Now.AddYears(-18))
            {
                throw new ArgumentException($"Date of birth is not valid, the customer must be adult.");
            }

            if (!new ZipControllerMock(dbContext).IsZipExisting(user.ZipId ?? 0))
            {
                throw new ArgumentException($"Zip id doesn't exist in the system.");
            }

            using (var db = dbContext)
            {
                var userNew = new User
                {
                    Username = user.Username,
                    Password = user.Password,
                    Name = user.Name,
                    Address = user.Address,
                    Email = user.Email,
                    Phone = user.Phone,
                    DateOfBirth = dateParsed,
                    Gender = user.Gender,
                    ZipId = user.ZipId,
                    Coins = user.Coins,
                    Payments = user.Payments,
                    IsAdmin = false,
                    IsDeleted = false
                };

                db.Users.Add(userNew);
                db.SaveChanges();
            }
        }

        public bool DeleteUser(User user)
        {
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = dbContext)
            {
                var currentUser = GetUserById(user.Id);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                db.Users.Remove(currentUser);
                db.SaveChanges();

                return true;
            }
        }


        public IList<Bid> GetUserBids(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = dbContext)
            {
                var currentUser = GetUserById(user.Id);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser.Bids.ToList();
            }
        }


        public User GetUserById(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

            using (var db = dbContext)
            {
                var currentUser = db.Users.FirstOrDefault(u => u.Id == id);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser;
            }
        }

        public User GetUserByIdWithAllCollections(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

            using (var db = dbContext)
            {
                var currentUser = db.Users
                                        .Include("Bids")
                                        .Include("Bids.Product")
                                        .Include("Payments")
                                        .Include("Invoices")
                                        .Include("Zip")
                                        .FirstOrDefault(u => u.Id == id);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser;
            }
        }

        public User GetUserByUsername(string username)
        {
            CoreValidator.ThrowIfNullOrEmpty(username, nameof(username));

            using (var db = dbContext)
            {
                var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser;
            }
        }

        public IList<Invoice> GetUserInvoices(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = dbContext)
            {
                var currentUser = GetUserById(user.Id);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser.Invoices.ToList();
            }
        }

        public IList<Product> GetUserProducts(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = dbContext)
            {
                var currentUser = GetUserById(user.Id);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser.Bids
                                        .Where(b => b.UserId == user.Id)
                                        .Select(b => b.Product)
                                        .ToList();
            }
        }

        public bool IsUserExisting(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNullOrEmpty(user.Username, nameof(user.Username));
            CoreValidator.ThrowIfNullOrEmpty(user.Password, nameof(user.Password));
            CoreValidator.ThrowIfNullOrEmpty(user.Name, nameof(user.Name));
            CoreValidator.ThrowIfNullOrEmpty(user.Address, nameof(user.Address));
            CoreValidator.ThrowIfNullOrEmpty(user.Email, nameof(user.Email));
            CoreValidator.ThrowIfNullOrEmpty(user.Phone, nameof(user.Phone));
            CoreValidator.ThrowIfDateIsNotCorrect(user.DateOfBirth.ToString(), nameof(user.DateOfBirth));
            CoreValidator.SpecialThrowForCoinsIfValueIsNegativeOnly(user.Coins, nameof(user.Coins));

            using (var db = dbContext)
            {
                return db.Users.Any(u => u.Username == user.Username);
            }
        }

        public bool UpdateUser(User user, string property, string value)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));
            CoreValidator.ThrowIfNullOrEmpty(property, nameof(property));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            using (var db = dbContext)
            {
                var userNew = GetUserById(user.Id);

                CoreValidator.ThrowIfNull(userNew, nameof(userNew));

                db.Users.Attach(userNew);

                switch (property.ToLower())
                {
                    case "name":
                        userNew.Name = value;
                        break;
                    case "phone":
                        userNew.Phone = value;
                        break;
                    case "email":
                        userNew.Email = value;
                        break;
                    case "address":
                        userNew.Address = value;
                        break;
                    case "coins":
                        if (!Int32.TryParse(value, out int temp))
                        {
                            throw new ArgumentException("Invalid parameter for coins.");
                        }

                        var parsedValue = int.Parse(value);

                        CoreValidator.ThrowIfNegativeOrZero(parsedValue, nameof(parsedValue));

                        userNew.Coins = parsedValue;
                        break;
                    case "isadmin":
                        if (userNew.IsAdmin)
                        {
                            userNew.IsAdmin = false;
                        }
                        else
                        {
                            userNew.IsAdmin = true;
                        }
                        break;
                    case "isdeleted":
                        if (userNew.IsDeleted)
                        {
                            userNew.IsDeleted = false;
                        }
                        else
                        {
                            userNew.IsDeleted = true;
                        }
                        break;
                    default:
                        throw new ArgumentException("No such property.");
                }


                // db.Entry(userNew).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
        }
    }
}