namespace AuctionSystem.WcfService.Contracts
{
    using System.ServiceModel;

    [ServiceContract]
    public interface ILoginService
    {
        [OperationContract]
        bool ValidateLogin(string username, string password);
    }
}
