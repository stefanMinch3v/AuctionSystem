using AuctionSystem.Controllers.Common;

namespace AuctionSystem.Controllers
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using Data;
    using System.Linq;

    public class ProductController : IProductController
    {
        // TODO
        public void CreateProduct(string name, string description, decimal price, DateTime startDate, DateTime endDate)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNullOrEmpty(description, nameof(description));
            CoreValidator.ThrowIfNegativeOrZero(price, nameof(price));
            using (var db = new AuctionContext())
            {
                var product = new Product
                {
                    Name = name,
                    Price = price,
                    Description = description,
                    StartDate = startDate,
                    IsAvailable = startDate <= DateTime.Now && endDate > DateTime.Now,
                    EndDate = endDate
                };

                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public bool UpdateProduct(int id, string property, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(property));
            using (var db = new AuctionContext())
            {
                var product = GetProductById(id);
                db.Products.Attach(product);
                switch (property.ToLower())
                {
                    case "name":
                        CoreValidator.ThrowIfNullOrEmpty(value, nameof(property));
                        product.Name = value;
                        break;
                    case "price":
                        if (!Int32.TryParse(value, out int result))
                        {
                            throw new ArgumentException("The value cannot be parsed to integer");
                        }
                        CoreValidator.ThrowIfNegativeOrZero(Int32.Parse(value), nameof(property));
                        product.Price = Int32.Parse(value);
                        break;
                    case "description":
                        CoreValidator.ThrowIfNullOrEmpty(value, nameof(property));
                        product.Description = value;
                        break;
                    case "startdate":
                        CoreValidator.ThrowIfDateIsNotCorrect(value, nameof(property));
                        DateTime date = DateTime.Parse(value);
                        if (date > product.EndDate)
                        {
                            throw new ArgumentException("Start date cannot be bigger than the end date");
                        }
                        else if (date < DateTime.Now)
                        {
                            throw new ArgumentException("Start date cannot be lower than the current date");
                        }
                        product.StartDate = date;
                        break;
                    case "enddate":
                        CoreValidator.ThrowIfDateIsNotCorrect(value, nameof(property));
                        DateTime dateEnd = DateTime.Parse(value);
                        if (dateEnd < product.StartDate)
                        {
                            throw new ArgumentException("End date cannot be lower than the start date");
                        }
                        else if (dateEnd < DateTime.Now)
                        {
                            throw new ArgumentException("End date cannot be lower than the current date");
                        }
                        product.EndDate = dateEnd;
                        break;
                    default:
                        throw new ArgumentException("No such property");
                }
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteProduct(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));
            using (var db = new AuctionContext())
            {
                var product = GetProductById(id);
                db.Products.Attach(product);
                if (product == null) return false;

                db.Products.Remove(product);
                db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
        }

        public Product GetProductByName(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            using (var db = new AuctionContext())
            {
                var product = db.Products.Include("Bids").FirstOrDefault(p => p.Name == name);
                CoreValidator.ThrowIfNull(product, nameof(product));
                return product;

            }
        }

        public Product GetProductById(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));
            using (var db = new AuctionContext())
            {
                var product = db.Products.Include("Bids").SingleOrDefault(p => p.Id == id);
                CoreValidator.ThrowIfNull(product, nameof(product));
                return product;
            }
        }

        public bool IsProductExisting(string productName)
        {
            CoreValidator.ThrowIfNullOrEmpty(productName, nameof(productName));
            using (var db = new AuctionContext())
            {
                return db.Products.Any(p => p.Name == productName);
            }
        }

        public int CountUserBidsForProduct(int id)
        {
            using (var db = new AuctionContext())
            {
                var collection = GetProductUsers(id);
                return collection != null ? this.GetProductUsers(id).Count : 0;
            }
        }

        public IList<User> GetProductUsers(int id)
        {
            using (var db = new AuctionContext())
            {
                var collection = db.Products.SingleOrDefault(p => p.Id == id).Bids.Select(u => u.UserId);
                return collection.Select(userId => db.Users.FirstOrDefault(u => u.Id == userId)).ToList();
            }
        }

        public bool IsProductExistingById(int productId)
        {
            using (var db = new AuctionContext())
            {
                return db.Products.Any(p => p.Id == productId);
            }
        }
    }
}