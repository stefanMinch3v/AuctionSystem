﻿namespace AuctionSystem.WcfService
{
    using AutoMapper;
    using Contracts;
    using Controllers;
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;

    public class ProductService : IProductService
    {
        public void CreateProduct(Product product)
        {
            ProductController.Instance().CreateProduct(product);
        }

        public bool UpdateProduct(ProductDto productDto)
        {
            try
            {
                var productToUpdate = MapProductDtoToProduct(productDto);
                return ProductController.Instance().UpdateProduct(productToUpdate);
            }
            catch (Exception)
            {
                throw;
            }
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

        public bool MakeProductUnavailable(int productId)
        {
            return ProductController.Instance().MakeProductUnavailable(productId);
        }

        public IList<ProductDto> GetAllProducts()
        {
            var products = ProductController.Instance().GetAllProducts();
            var products2 = new List<ProductDto>();

            foreach (var product in products)
            {
                var productToAdd = Mapper.Map<ProductDto>(product);
                products2.Add(productToAdd);
            }
            return products2;
        }

        private ProductDto MapDbProductToProductDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Description = product.Description,
                StartDate = product.StartDate,
                EndDate = product.EndDate,
                Price = product.Price,
                IsAvailable = product.IsAvailable,
                Name = product.Name,
                RowVersion = product.RowVersion
            };
        }
        private Product MapProductDtoToProduct(ProductDto productDto)
        {
            return new Product
            {
                Id = productDto.Id,
                Description = productDto.Description,
                StartDate = productDto.StartDate,
                EndDate = productDto.EndDate,
                Price = productDto.Price,
                IsAvailable = productDto.IsAvailable,
                Name = productDto.Name,
                RowVersion = productDto.RowVersion
            };
        }
    }
}
