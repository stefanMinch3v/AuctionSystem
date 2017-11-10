namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Interfaces;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;

    public class ProductControllerMock : IProductController
    {
        private readonly AuctionContext dbContext;

        public ProductControllerMock(AuctionContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // TODO
        public void CreateProduct(string name, string description, decimal price, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(int id, string property, string value)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsProductExisting(string productName)
        {
            throw new NotImplementedException();
        }

        public int CountUserBidsForProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetProductUsers(int id)
        {
            throw new NotImplementedException();
        }
    }
}
