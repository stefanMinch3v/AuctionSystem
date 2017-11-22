namespace AuctionSystem.Controllers
{
    using AutoMapper;
    using Common;
    using Data;
    using Interfaces;
    using Models;
    using Models.DTOs;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserController : IUserController
    {
        private static UserController instance;

        private UserController()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDto>()
                                        .ForMember(dest => dest.ZipCountryCity, opt => opt.MapFrom(src => src.Zip.Country + ", " + src.Zip.City))
                                        .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => string.Join(Environment.NewLine, src.Payments.Select(p => p.Type + ", " + p.PaymentTypeCode))))
                                        .ForMember(dest => dest.Invoices, opt => opt.MapFrom(src => string.Join(Environment.NewLine, src.Invoices.Select(p => p.ProductId + ", " + p.Product.Name))))
                                        .ForMember(dest => dest.Bids, opt => opt.MapFrom(src => string.Join(Environment.NewLine, src.Bids.Select(b => b.Product.Name + ", " + b.Coins + ", " + b.IsWon + ", " + b.DateOfCreated))));
                cfg.CreateMap<Bid, BidDto>()
                                        .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                                        .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            });
        }

        public static UserController Instance()
        {
            if (instance == null)
            {
                instance = new UserController();
            }

            return instance;
        }

        public int CountUserBidsForGivenProduct(int userId, int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

            using (var db = new AuctionContext())
            {
                var currentUser = GetUserById(userId);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                var bids = currentUser.Bids.ToList();

                if (bids.Count == 0)
                {
                    return 0;
                }

                return bids.Select(b => b.ProductId == productId).Count();
            }
        }

        public bool IsUserExistingById(int userId)
        {
            using (var db = new AuctionContext())
            {
                return db.Users.Any(u => u.Id == userId);
            }
        }

        public int GetAllUserSpentCoinsForGivenProduct(int userId, int productId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            CoreValidator.ThrowIfNegativeOrZero(productId, nameof(productId));

            using (var db = new AuctionContext())
            {
                var currentUser = GetUserById(userId);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser.Bids
                                       .Where(b => b.ProductId == productId)
                                       .Sum(b => b.Coins);
            }
        }

        public void CreateUser(string username, string password, string name, string address, string email, string phone, string dateOfBirth, Gender gender, int zipId, int coins, List<Payment> payments)
        {
            CoreValidator.ThrowIfNullOrEmpty(username, nameof(username));
            CoreValidator.ThrowIfNullOrEmpty(password, nameof(password));
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNullOrEmpty(address, nameof(address));
            CoreValidator.ThrowIfNullOrEmpty(email, nameof(email));
            CoreValidator.ThrowIfNullOrEmpty(phone, nameof(phone));
            CoreValidator.ThrowIfDateIsNotCorrect(dateOfBirth, nameof(dateOfBirth));
            CoreValidator.SpecialThrowForCoinsIfValueIsNegativeOnly(coins, nameof(coins));
            CoreValidator.ThrowIfNegativeOrZero(zipId, nameof(zipId));

            var dateParsed = DateTime.Parse(dateOfBirth);
            if (dateParsed > DateTime.Now.AddYears(-18))
            {
                throw new ArgumentException($"Date of birth is not valid, the customer must be adult.");
            }

            if (!new ZipController().IsZipExisting(zipId))
            {
                throw new ArgumentException($"Zip id doesn't exist in the system.");
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
                    ZipId = zipId,
                    Coins = coins,
                    Payments = payments,
                    IsAdmin = false,
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
                var currentUser = GetUserById(id);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                db.Users.Remove(currentUser);
                db.SaveChanges();

                return true;
            }
        }


        public IList<Bid> GetUserBids(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            using (var db = new AuctionContext())
            {
                var currentUser = GetUserById(userId);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser.Bids.ToList();
            }
        }


        public User GetUserById(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

            using (var db = new AuctionContext())
            {
                var currentUser = db.Users.FirstOrDefault(u => u.Id == id);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser;
            }
        }

        public User GetUserByIdWithAllCollections(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

            using (var db = new AuctionContext())
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

            using (var db = new AuctionContext())
            {
                var currentUser = db.Users.FirstOrDefault(u => u.Username == username);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser;
            }
        }

        public IList<Invoice> GetUserInvoices(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            using (var db = new AuctionContext())
            {
                var currentUser = GetUserById(userId);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser.Invoices.ToList();
            }
        }

        public IList<Product> GetUserProducts(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            using (var db = new AuctionContext())
            {
                var currentUser = GetUserById(userId);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser.Bids
                                        .Where(b => b.UserId == userId)
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

