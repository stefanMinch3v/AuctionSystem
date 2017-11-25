namespace AuctionSystem.WcfService.Contracts
{
    using Models;
    using Models.DTOs;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IProductService
    {
        [OperationContract(IsOneWay = true)]
        void CreateProduct(Product product);

        [OperationContract]
        bool UpdateProduct(Product product);

        [OperationContract]
        bool DeleteProduct(Product product);

        [OperationContract]
        ProductDto GetProductByName(string name);

        [OperationContract]
        ProductDto GetProductById(int id);

        [OperationContract]
        bool IsProductExisting(string name);

        [OperationContract]
        int CountUserBidsForProduct(int id);

        [OperationContract]
        IList<User> GetProductUsers(Product product);
    }
}