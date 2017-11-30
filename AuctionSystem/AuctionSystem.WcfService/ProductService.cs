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

        public bool UpdateProduct(Product product)
        {
            return ProductController.Instance().UpdateProduct(product);
        }

        public bool DeleteProduct(Product product)
        {
            return ProductController.Instance().DeleteProduct(product);
        }

        public ProductDto GetProductByName(string name)
        {
            var dbProduct = ProductController.Instance().GetProductByName(name);

            return MapDbProductToProductDto(dbProduct);
        }

        public ProductDto GetProductById(int id)
        {
            var dbProduct = ProductController.Instance().GetProductByIdWithBidsAndUser(id);

            return MapDbProductToProductDto(dbProduct);
        }

        public bool IsProductExisting(string name)
        {
            return ProductController.Instance().IsProductExisting(name);
        }

        public int CountUserBidsForProduct(int id)
        {
            return ProductController.Instance().CountUserBidsForProduct(id);
        }

        public IList<User> GetProductUsers(Product product)
        {
            return ProductController.Instance().GetProductUsers(product);
        }

        private ProductDto MapDbProductToProductDto(Product product)
        {
            return Mapper.Map<ProductDto>(product);
        }
    }
}
