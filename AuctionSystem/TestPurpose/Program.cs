using AuctionSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionSystem.Models;
using AuctionSystem.Models.Enums;

namespace TestPurpose
{
    class Program
    {
        static void Main(string[] args)
        {
            //var zipController = new ZipController();
            //var zip = zipController.GetZipByZipCode("9000");

            //var paymentController = new PaymentController();


            var controller = new UserController();
            Console.WriteLine(controller.CountUserBidsForGivenProduct(12, 1));

            //controller.CreateUser("ivan", "ivaneE23422", "ivan", "nqkade", "user@abv.bg", "44444444", "11/11/1990", Gender.Male, null, 22, new List<Payment>());
            Console.WriteLine("Yes! Check the db!");
        }
    }
}
