namespace AuctionSystem.WcfService
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using AuctionSystem.Models;
    using AuctionSystem.Controllers;
    using AuctionSystem.Controllers.Interfaces;

    public class ProductService : IProductService
    {
        private IProductController productService = new ProductController();

        public int CountUserBidsForProduct(int id)
        {
            return productService.CountUserBidsForProduct(id);
        }
        public void CreateProduct(string name, string description, decimal price, DateTime startDate, DateTime endDate)
        {
            productService.CreateProduct(name, description, price, startDate, endDate);
        }

        public bool DeleteProduct(int id)
        {
            return productService.DeleteProduct(id);
        }

        public Product GetProductById(int id)
        {
            return GetDTOProduct(productService.GetProductById(id));

        }

        public Product GetProductByName(string name)
        {
            var product = productService.GetProductByName(name);
            return GetDTOProduct(product);
        }

        public IList<User> GetProductUsers(int id)
        {
            return productService.GetProductUsers(id);
        }

        public bool IsProductExisting(string productName)
        {
            return productService.IsProductExisting(productName);
        }

        public bool UpdateProduct(int id, string property, string value)
        {
            return productService.UpdateProduct(id, property, value);
        }
        private Product GetDTOProduct(Product product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                StartDate = product.StartDate,
                EndDate = product.EndDate,
                IsAvailable = product.IsAvailable,
                Price = product.Price,
                Bids = product.Bids,
                Image = product.Image
            };
        }
    }
}
