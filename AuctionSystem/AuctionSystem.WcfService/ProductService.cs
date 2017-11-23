namespace AuctionSystem.WcfService
{
    using AutoMapper;
    using Contracts;
    using Controllers;
    using Models;
    using Models.DTOs;
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

        public ProductDto GetProductById(int id)
        {
            var dbProduct = ProductController.Instance().GetProductByIdWithBidsAndUser(id);

            return MapDbProductToProductDto(dbProduct);
        }

        public ProductDto MapDbProductToProductDto(Product product)
        {
            return Mapper.Map<ProductDto>(product);
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
