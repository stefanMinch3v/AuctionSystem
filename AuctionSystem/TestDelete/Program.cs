using AuctionSystem.Controllers;
using AuctionSystem.WcfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDelete
{
    class Program
    {
        static void Main(string[] args)
        {

            var service = new LoginService().ValidateLogin("ivan", "ivan");
            var userService = new UserService().GetUserById(3);

            Console.WriteLine("da");
        }
    }
}
