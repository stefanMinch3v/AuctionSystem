namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Contracts;
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
        public void CreateProduct(Product product)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNullOrEmpty(product.Name, nameof(product.Name));
            CoreValidator.ThrowIfNullOrEmpty(product.Description, nameof(product.Description));
            CoreValidator.ThrowIfNegativeOrZero(product.Price, nameof(product.Price));

            using (var db = dbContext)
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

        public bool UpdateProduct(Product product, string property, string value)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNull(property, nameof(property));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            using (var db = dbContext)
            {
                var productNew = GetProductById(product.Id);

                CoreValidator.ThrowIfNull(productNew, nameof(productNew));

                db.Products.Attach(productNew);

                switch (property.ToLower())
                {
                    case "name":
                        CoreValidator.ThrowIfNullOrEmpty(value, nameof(property));
                        productNew.Name = value;
                        break;
                    case "price":
                        if (!Int32.TryParse(value, out int result))
                        {
                            throw new ArgumentException("The value cannot be parsed to integer");
                        }

                        CoreValidator.ThrowIfNegativeOrZero(Int32.Parse(value), nameof(property));

                        productNew.Price = Int32.Parse(value);
                        break;
                    case "description":
                        CoreValidator.ThrowIfNullOrEmpty(value, nameof(property));

                        productNew.Description = value;
                        break;
                    case "startdate":
                        CoreValidator.ThrowIfDateIsNotCorrect(value, nameof(property));

                        DateTime date = DateTime.Parse(value);

                        if (date > productNew.EndDate)
                        {
                            throw new ArgumentException("Start date cannot be bigger than the end date");
                        }
                        else if (date < DateTime.Now)
                        {
                            throw new ArgumentException("Start date cannot be lower than the current date");
                        }

                        productNew.StartDate = date;
                        break;
                    case "enddate":
                        CoreValidator.ThrowIfDateIsNotCorrect(value, nameof(property));

                        DateTime dateEnd = DateTime.Parse(value);

                        if (dateEnd < productNew.StartDate)
                        {
                            throw new ArgumentException("End date cannot be lower than the start date");
                        }
                        else if (dateEnd < DateTime.Now)
                        {
                            throw new ArgumentException("End date cannot be lower than the current date");
                        }

                        productNew.EndDate = dateEnd;
                        break;
                    default:
                        throw new ArgumentException("No such property");
                }

                // db.Entry(productNew).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteProduct(Product product)
        {
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));

            using (var db = dbContext)
            {
                var productNew = GetProductById(product.Id);

                CoreValidator.ThrowIfNull(productNew, nameof(productNew));

                db.Products.Attach(productNew);

                db.Products.Remove(productNew);
                // db.Entry(productNew).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

                return true;
            }
        }

        public Product GetProductByName(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            using (var db = dbContext)
            {
                var product = db.Products.Include("Bids").FirstOrDefault(p => p.Name == name);

                CoreValidator.ThrowIfNull(product, nameof(product));

                return product;
            }
        }

        public Product GetProductById(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

            using (var db = dbContext)
            {
                var product = db.Products.Include("Bids").SingleOrDefault(p => p.Id == id);

                CoreValidator.ThrowIfNull(product, nameof(product));

                return product;
            }
        }

        public bool IsProductExisting(Product product)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNullOrEmpty(product.Name, nameof(product.Name));

            using (var db = dbContext)
            {
                return db.Products.Any(p => p.Name == product.Name);
            }
        }

        public int CountUserBidsForProduct(int id)
        {
            using (var db = dbContext)
            {
                var collection = GetProductUsers(GetProductById(id));
                return collection != null ? collection.Count : 0;
            }
        }

        public IList<User> GetProductUsers(Product product)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));
            
            using (var db = dbContext)
            {
                var collection = db.Products.SingleOrDefault(p => p.Id == product.Id).Bids.Select(u => u.UserId);
                CoreValidator.ThrowIfNull(collection, nameof(collection));
                return collection.Select(userId => db.Users.FirstOrDefault(u => u.Id == userId)).ToList();
            }
        }

        public bool IsProductExistingById(int productId)
        {
            using (var db = dbContext)
            {
                return db.Products.Any(p => p.Id == productId);
            }
        }
    }
}
