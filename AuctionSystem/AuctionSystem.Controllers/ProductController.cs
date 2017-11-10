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
        public int CountUserBidsForProduct(int id)
        {
            using (var db = new AuctionContext())
            {
                var collection = GetProductUsers(id);
                return collection != null ? this.GetProductUsers(id).Count : 0;
            }
        }

        public void CreateProduct(string name, string description, decimal price, DateTime startDate, DateTime endDate)
        {
            using (var db = new AuctionContext())
            {
                var product = new Product
                {
                    Name = name,
                    Price = price,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate
                };

                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public bool DeleteProduct(int id)
        {
            using (var db = new AuctionContext())
            {
                var product = GetProductById(id);
                
                if (product == null) return false;
                
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
        }

        public Product GetProductById(int id)
        {
            using (var db = new AuctionContext())
            {
                return db.Products.SingleOrDefault(p => p.Id == id);
            }
        }

        public Product GetProductByName(string name)
        {
            using (var db = new AuctionContext())
            {
                return db.Products.SingleOrDefault(p => p.Name == name);
            }
        }

        public IList<User> GetProductUsers(int id)
        {
            using (var db = new AuctionContext())
            {
                var collection = db.Products.SingleOrDefault(p => p.Id == id).Users.Select(u => u.UserId);
                return collection.Select(userId => db.Users.FirstOrDefault(u => u.Id == userId)).ToList();
            }
        }

        public bool IsProductExisting(string productName)
        {
            using (var db = new AuctionContext())
            {
                return db.Products.Single(p => p.Name == productName) != null;
            }
        }

        public bool UpdateProduct(Product product, string property, string value)
        {
            throw new NotImplementedException();
        }
    }
}