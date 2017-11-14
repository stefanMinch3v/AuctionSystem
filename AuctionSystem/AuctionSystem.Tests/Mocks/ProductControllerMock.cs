namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Interfaces;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            using (dbContext)
            {
                var product = GetProductById(id);

                try
                {
                    switch (property.ToLower())
                    {
                        case "name":
                            product.Name = value;
                            break;
                        case "price":
                            product.Price = Int32.Parse(value);
                            break;
                        case "description":
                            product.Description = value;
                            break;
                        case "startDate":
                            product.StartDate = Convert.ToDateTime(value);
                            break;
                        case "endDate":
                            product.EndDate = Convert.ToDateTime(value);
                            break;
                        default:
                            Console.WriteLine("no such property");
                            break;
                    }
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw new Exception("Failed at ProductController -> UpdateProduct");
                }
            }
        }

        public bool DeleteProduct(int id)
        {
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
            using (dbContext)
            {
                return dbContext.Products.SingleOrDefault(p => p.Name == name);
            }
        }

        public Product GetProductById(int id)
        {
            using (dbContext)
            {
                var result = dbContext.Products.SingleOrDefault(p => p.Id == id);
                return result;
            }
        }

        public bool IsProductExisting(string productName)
        {
            using (dbContext)
            {
                return dbContext.Products.Single(p => p.Name == productName) != null;
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
                var collection = dbContext.Products.SingleOrDefault(p => p.Id == id).Users.Select(u => u.UserId);
                return collection.Select(userId => dbContext.Users.FirstOrDefault(u => u.Id == userId)).ToList();
            }
        }
    }
}
