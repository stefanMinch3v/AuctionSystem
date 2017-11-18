namespace AuctionSystem.WcfService.Interfaces
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IProductService
    {
        [OperationContract(IsOneWay = true)]
        void CreateProduct(string name, string description, decimal price, DateTime startDate, DateTime endDate);

        [OperationContract]
        bool UpdateProduct(int id, string property, string value);

        [OperationContract]
        bool DeleteProduct(int id);

        [OperationContract]
        Product GetProductByName(string name);

        [OperationContract]
        Product GetProductById(int id);

        [OperationContract]
        bool IsProductExisting(string productName);

        [OperationContract]
        int CountUserBidsForProduct(int id);

        [OperationContract]
        IList<User> GetProductUsers(int id);
    }
}
