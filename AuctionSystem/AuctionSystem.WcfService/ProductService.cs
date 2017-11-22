namespace AuctionSystem.WcfService
{
    using Controllers;
    using Controllers.Contracts;
    using Contracts;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ProductService : IProductService
    {
        public void CreateProduct(Product product)
        {
            ProductController.Instance().CreateProduct(product);
        }

        public bool UpdateProduct(Product product, string property, string value)
        {
            return ProductController.Instance().UpdateProduct(product, property, value);
        }

        public bool DeleteProduct(Product product)
        {
            return ProductController.Instance().DeleteProduct(product);
        }

        public Product GetProductByName(string name)
        {
            return ProductController.Instance().GetProductByName(name);
        }

        public Product GetProductById(int id)
        {
            return ProductController.Instance().GetProductById(id);
        }

        public bool IsProductExisting(Product product)
        {
            return ProductController.Instance().IsProductExisting(product);
        }

        public int CountUserBidsForProduct(int id)
        {
            return ProductController.Instance().CountUserBidsForProduct(id);
        }

        public IList<User> GetProductUsers(Product product)
        {
            return ProductController.Instance().GetProductUsers(product);
        }
    }
}
