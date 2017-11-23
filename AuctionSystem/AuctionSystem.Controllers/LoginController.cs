namespace AuctionSystem.Controllers
{
    using Controllers.Contracts;
    using Data;
    using System.Linq;

    public class LoginController : ILoginController
    {
        public bool ValidateLogin(string username, string password)
        {
            using (var db = new AuctionContext())
            {
                // var t = from p in db.Users where p.Username == username select p;
                var enteredUsername = db.Users
                                            .Where(u => u.Username == username)
                                            .Select(u => u.Username);
                var enteredPassword = db.Users
                                            .Where(p => p.Password == password)
                                            .Select(p => p.Password);

                if (enteredUsername.Any() && enteredPassword.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }
    }
}