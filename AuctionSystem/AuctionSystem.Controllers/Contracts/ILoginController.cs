namespace AuctionSystem.Controllers.Contracts
{
    public interface ILoginController
    {
        bool ValidateLogin(string username, string password);
    }
}