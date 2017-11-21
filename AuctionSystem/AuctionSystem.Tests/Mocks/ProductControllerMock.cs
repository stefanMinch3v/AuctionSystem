namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Interfaces;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Controllers.Common;

    public class ProductControllerMock : IProductController
    {
        private readonly AuctionContext dbContext;

        public ProductControllerMock(AuctionContext db)
        {
            this.dbContext = db;
        }

        // TODO
        public void CreateProduct(string name, string description, decimal price, DateTime startDate, DateTime endDate)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNullOrEmpty(description, nameof(description));
            CoreValidator.ThrowIfNegativeOrZero(price, nameof(price));
            using (dbContext)
            {
                var product = new Product
                {
                    Name = name,
                    Price = price,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate
                };

                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
        }

        public bool UpdateProduct(int id, string property, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(property));
            using (dbContext)
            {
                var product = GetProductById(id);

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
                dbContext.SaveChanges();
                return true;
            }
        }

        public bool DeleteProduct(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));
            using (dbContext)
            {
                var product = GetProductById(id);

                if (product == null) return false;

                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
                return true;
            }
        }

        public Product GetProductByName(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            using (dbContext)
            {
                return dbContext.Products.SingleOrDefault(p => p.Name == name);
            }
        }

        public Product GetProductById(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));
            using (dbContext)
            {
                var result = dbContext.Products.SingleOrDefault(p => p.Id == id);
                return result;
            }
        }

        public bool IsProductExisting(string productName)
        {
            CoreValidator.ThrowIfNullOrEmpty(productName, nameof(productName));
            using (dbContext)
            {
                return dbContext.Products.SingleOrDefault(p => p.Name == productName) != null;
            }
        }

        public int CountUserBidsForProduct(int id)
        {
            using (dbContext)
            {
                var collection = GetProductUsers(id);
                return collection != null ? this.GetProductUsers(id).Count : 0;
            }
        }

        public IList<User> GetProductUsers(int id)
        {
            using (dbContext)
            {
                var collection = dbContext.Products.SingleOrDefault(p => p.Id == id).Bids.Select(u => u.UserId);
                return collection.Select(userId => dbContext.Users.FirstOrDefault(u => u.Id == userId)).ToList();
            }
        }

        public bool IsProductExistingById(int productId)
        {
            using (dbContext)
            {
                return dbContext.Products.Any(p => p.Id == productId);
            }
        }
    }
}
