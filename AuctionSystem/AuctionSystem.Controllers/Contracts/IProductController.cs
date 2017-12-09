namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IProductController
    {
        void CreateProduct(Product product);

        bool UpdateProduct(Product product);

        bool DeleteProduct(Product product);

        Product GetProductByName(string name);

        Product GetProductById(int id);

        bool IsProductExisting(string name);

        int CountUserBidsForProduct(int id);

        IList<User> GetProductUsers(Product product);

        IList<Product> GetAllProducts();
    }
}