namespace AuctionSystem.WcfService.Interfaces
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        void CreateProduct(string name, string description, decimal price, DateTime startDate, DateTime endDate);

        [OperationContract]
        bool UpdateProduct(Product product, string property, string value);

        [OperationContract]
        bool DeleteProduct(Product product);

        [OperationContract]
        Product GetProductByName(Product product);

        [OperationContract]
        Product GetProductById(int id);

        [OperationContract]
        bool IsProductExisting(string productName);

        [OperationContract]
        int CountUserBidsForProduct(User user);

        [OperationContract]
        IList<User> GetProductUsers(Product product);
    }
}
