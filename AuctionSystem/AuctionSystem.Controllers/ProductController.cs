namespace AuctionSystem.Controllers
{
    using Contracts;
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;
    using Data;
    using System.Linq;

    public class ProductController : IProductController
    {
        private static ProductController instance;

        private ProductController()
        {
        }

        public static ProductController Instance()
        {
            if (instance == null)
            {
                instance = new ProductController();
            }

            return instance;
        }

        public void CreateProduct(Product product)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNullOrEmpty(product.Name, nameof(product.Name));
            CoreValidator.ThrowIfNullOrEmpty(product.Description, nameof(product.Description));
            CoreValidator.ThrowIfNegativeOrZero(product.Price, nameof(product.Price));

            using (var db = new AuctionContext())
            {
                var productNew = new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    StartDate = product.StartDate,
                    IsAvailable = product.StartDate <= DateTime.Now && product.EndDate > DateTime.Now,
                    EndDate = product.EndDate
                };

                db.Products.Add(productNew);
                db.SaveChanges();
            }
        }

        public bool UpdateProduct(Product product)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNullOrEmpty(product.Name, nameof(product.Name));
            CoreValidator.ThrowIfNegativeOrZero(product.Price, nameof(product.Price));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));
            CoreValidator.ThrowIfNullOrEmpty(product.Description, nameof(product.Description));
            CoreValidator.ThrowIfDateIsNotCorrect(product.StartDate.ToString(), nameof(product.StartDate));
            CoreValidator.ThrowIfDateIsNotCorrect(product.EndDate.ToString(), nameof(product.EndDate));

            DateTime date = product.StartDate;
            if (date > product.EndDate)
            {
                throw new ArgumentException("Start date cannot be bigger than the end date");
            }

            DateTime dateEnd = product.EndDate;
            if (dateEnd < product.StartDate)
            {
                throw new ArgumentException("End date cannot be lower than the start date");
            }

            using (var db = new AuctionContext())
            {
                var dbProduct = GetProductById(product.Id);
                db.Products.Attach(dbProduct);

                dbProduct.Name = product.Name;
                dbProduct.Description = product.Description;
                dbProduct.Price = product.Price;
                dbProduct.StartDate = product.StartDate;
                dbProduct.EndDate = product.EndDate;

                db.Entry(dbProduct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteProduct(Product product)
        {
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));

            using (var db = new AuctionContext())
            {
                var productNew = GetProductById(product.Id);

                CoreValidator.ThrowIfNull(productNew, nameof(productNew));

                db.Products.Attach(productNew);

                db.Products.Remove(productNew);
                db.Entry(productNew).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

                return true;
            }
        }

        public Product GetProductByName(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            using (var db = new AuctionContext())
            {
                var product = db.Products
                                    .Include("Bids")
                                    .Include("Bids.User")
                                    .FirstOrDefault(p => p.Name == name);

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

        public Product GetProductByIdWithBidsAndUser(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

            using (var db = new AuctionContext())
            {
                var product = db.Products
                                    .Include("Bids")
                                    .Include("Bids.User")
                                    .FirstOrDefault(p => p.Id == id);

                CoreValidator.ThrowIfNull(product, nameof(product));

                return product;
            }
        }

        public bool IsProductExisting(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            using (var db = new AuctionContext())
            {
                return db.Products.Any(p => p.Name == name);
            }
        }

        public int CountUserBidsForProduct(int id)
        {
            using (var db = new AuctionContext())
            {
                var collection = GetProductUsers(GetProductById(id));
                return collection != null ? collection.Count : 0;
            }
        }

        public IList<User> GetProductUsers(Product product)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));
            
            using (var db = new AuctionContext())
            {
                var collection = db.Products.SingleOrDefault(p => p.Id == product.Id).Bids.Select(u => u.UserId);
                CoreValidator.ThrowIfNull(collection, nameof(collection));
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