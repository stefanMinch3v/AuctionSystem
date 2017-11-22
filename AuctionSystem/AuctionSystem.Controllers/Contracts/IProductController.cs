namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IProductController
    {
        void CreateProduct(Product product);

        bool UpdateProduct(Product product, string property, string value);

        bool DeleteProduct(Product product);

        Product GetProductByName(string name);

        Product GetProductById(int id);

        bool IsProductExisting(Product product);

        int CountUserBidsForProduct(int id);

        IList<User> GetProductUsers(Product product);
    }
}