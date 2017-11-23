using AuctionSystem.Controllers.Contracts;
using AuctionSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Controllers
{
    public class LoginController : ILoginController
    {
        public bool ValidateLogin(string username, string password)
        {
            using (var db = new AuctionContext())
            {
                // var t = from p in db.Users where p.Username == username select p;
                var enteredUsername = db.Users.Where(u => u.Username == username).Select(u => u.Username);
                var enteredPassword = db.Users.Where(p => p.Password == password).Select(p => p.Password);
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