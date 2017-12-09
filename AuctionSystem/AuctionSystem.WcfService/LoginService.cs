namespace AuctionSystem.WcfService
{
    using Contracts;
    using Controllers;

    public class LoginService : ILoginService
    {
        public bool ValidateLogin(string username, string password)
        {
            return LoginController.Instance().ValidateLogin(username, password);
        }
    }
}
