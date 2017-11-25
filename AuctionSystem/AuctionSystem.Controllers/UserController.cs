﻿namespace AuctionSystem.Controllers
{
    using AuctionSystem.Models.Utility;
    using Common;
    using Contracts;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserController : IUserController
    {
        private static UserController instance;

        private UserController()
        {
        }

        public static UserController Instance()
        {
            if (instance == null)
            {
                instance = new UserController();
            }

            return instance;
        }

        public int CountUserBidsForGivenProduct(User user, string productName)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNullOrEmpty(productName, nameof(productName));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = new AuctionContext())
            {
                var currentUser = GetUserById(user.Id);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                var bids = currentUser.Bids.ToList();

                if (bids.Count == 0)
                {
                    return 0;
                }

                return bids.Select(b => b.Product.Name == productName).Count();
            }
        }

        public bool IsUserExistingById(int userId)
        {
            using (var db = new AuctionContext())
            {
                return db.Users.Any(u => u.Id == userId);
            }
        }

        public int GetAllUserSpentCoinsForGivenProduct(User user, string productName)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNullOrEmpty(productName, nameof(productName));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = new AuctionContext())
            {
                var currentUser = GetUserById(user.Id);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser.Bids
                                       .Where(b => b.Product.Name == productName)
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

            ValidateUserPassword(user.Password);

            var dateParsed = user.DateOfBirth;
            if (dateParsed > DateTime.Now.AddYears(-18))
            {
                throw new ArgumentException($"Date of birth is not valid, the customer must be adult.");
            }

            if (!ZipController.Instance().IsZipExisting(user.ZipId ?? 0))
            {
                throw new ArgumentException($"Zip id doesn't exist in the system.");
            }

            using (var db = new AuctionContext())
            {
                var userNew = new User
                {
                    Username = user.Username,
                    Password = HashingSHA256.ComputeHash(user.Password),
                    Name = user.Name,
                    Address = user.Address,
                    Email = user.Email,
                    Phone = user.Phone,
                    DateOfBirth = dateParsed,
                    Gender = user.Gender,
                    ZipId = user.ZipId,
                    Coins = user.Coins,
                    IsAdmin = false,
                    IsDeleted = false
                };

                db.Users.Add(userNew);
                db.SaveChanges();
            }
        }

        public bool DeleteUser(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = new AuctionContext())
            {
                var currentUser = GetUserById(user.Id);

                db.Users.Attach(currentUser);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                currentUser.IsDeleted = true;

                db.Entry(currentUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
        }


        public IList<Bid> GetUserBids(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = new AuctionContext())
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
                                        .Include("Zip")
                                        .Include("Bids")
                                        .Include("Bids.Product")
                                        .Include("Payments")
                                        .Include("Invoices")
                                        .FirstOrDefault(u => u.Id == id);

                CoreValidator.ThrowIfNull(currentUser, nameof(currentUser));

                return currentUser;
            }
        }

        public User GetUserByNameWithAllCollections(string username)
        {
            CoreValidator.ThrowIfNullOrEmpty(username, nameof(username));

            using (var db = new AuctionContext())
            {
                var currentUser = db.Users
                                        .Include("Zip")
                                        .Include("Bids")
                                        .Include("Bids.Product")
                                        .Include("Payments")
                                        .Include("Invoices")
                                        .FirstOrDefault(u => u.Username == username);

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

        public IList<Invoice> GetUserInvoices(User user)
        {
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = new AuctionContext())
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

            using (var db = new AuctionContext())
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

        public bool IsUserExisting(string username)
        {
            CoreValidator.ThrowIfNullOrEmpty(username, nameof(username));

            using (var db = new AuctionContext())
            {
                return db.Users.Any(u => u.Username == username);
            }
        }

        public bool UpdateUser(User newUser)
        {
            CoreValidator.ThrowIfNull(newUser, nameof(newUser));
            CoreValidator.ThrowIfNull(newUser, nameof(newUser));
            CoreValidator.ThrowIfNullOrEmpty(newUser.Username, nameof(newUser.Username));
            CoreValidator.ThrowIfNullOrEmpty(newUser.Password, nameof(newUser.Password));
            CoreValidator.ThrowIfNullOrEmpty(newUser.Name, nameof(newUser.Name));
            CoreValidator.ThrowIfNullOrEmpty(newUser.Address, nameof(newUser.Address));
            CoreValidator.ThrowIfNullOrEmpty(newUser.Email, nameof(newUser.Email));
            CoreValidator.ThrowIfNullOrEmpty(newUser.Phone, nameof(newUser.Phone));
            CoreValidator.SpecialThrowForCoinsIfValueIsNegativeOnly(newUser.Coins, nameof(newUser.Coins));

            ValidateUserPassword(newUser.Password);

            if (newUser.DateOfBirth > DateTime.Now.AddYears(-18))
            {
                throw new ArgumentException($"Date of birth is not valid, the customer must be adult.");
            }

            if (!ZipController.Instance().IsZipExisting(newUser.ZipId ?? 0))
            {
                throw new ArgumentException($"Zip id doesn't exist in the system.");
            }

            using (var db = new AuctionContext())
            {
                var dbUser = GetUserById(newUser.Id);

                db.Users.Attach(dbUser);

                dbUser.Address = newUser.Address;
                dbUser.Coins = newUser.Coins;
                dbUser.DateOfBirth = newUser.DateOfBirth;
                dbUser.Email = newUser.Email;
                dbUser.Gender = newUser.Gender;
                dbUser.Name = newUser.Name;
                if (newUser.Password != dbUser.Password)
                {
                    dbUser.Password = HashingSHA256.ComputeHash(newUser.Password);
                }
                else
                {
                    dbUser.Password = newUser.Password;
                }
                dbUser.Payments = newUser.Payments;
                dbUser.Phone = newUser.Phone;
                dbUser.Username = newUser.Username;
                dbUser.ZipId = newUser.ZipId;

                db.Entry(dbUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
        }
        private void ValidateUserPassword(string password)
        {
            var minLength = 5;
            var maxLength = 100;
            bool userPasswordFlag = true;
            if (password.Length < minLength || password.Length > maxLength)
            {
                userPasswordFlag = false;
            }

            if (!password.Any(c => char.IsLower(c)))
            {
                userPasswordFlag = false;
            }

            if (!password.Any(c => char.IsUpper(c)))
            {
                userPasswordFlag = false;
            }

            if (!password.Any(c => char.IsDigit(c)))
            {
                userPasswordFlag = false;
            }

            if (!userPasswordFlag)
                throw new ArgumentException("Password must contain at least 5 symbols, at most 100 symbols, a capital letter, small letter and a digit");
        }
    }

}

