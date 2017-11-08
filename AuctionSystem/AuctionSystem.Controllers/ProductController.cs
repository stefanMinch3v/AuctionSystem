namespace AuctionSystem.Controllers
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ProductController : IProductController
    {
        // TODO
        public int CountUserBidsForProduct(User user)
        {
            throw new NotImplementedException();
        }

        public void CreateProduct(string name, string description, decimal price, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByName(Product product)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetProductUsers(Product product)
        {
            throw new NotImplementedException();
        }

        public bool IsProductExisting(string productName)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(Product product, string property, string value)
        {
            throw new NotImplementedException();
        }
    }
}
