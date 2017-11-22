namespace AuctionSystem.WcfService.Contracts
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IProductService
    {
        [OperationContract(IsOneWay = true)]
        void CreateProduct(Product product);

        [OperationContract]
        bool UpdateProduct(Product product, string property, string value);

        [OperationContract]
        bool DeleteProduct(Product product);

        [OperationContract]
        Product GetProductByName(string name);

        [OperationContract]
        Product GetProductById(int id);

        [OperationContract]
        bool IsProductExisting(Product product);

        [OperationContract]
        int CountUserBidsForProduct(int id);

        [OperationContract]
        IList<User> GetProductUsers(Product product);
    }
}