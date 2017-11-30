namespace AuctionSystem.Controllers
{
    using AutoMapper;
    using Common;
    using Controllers.Contracts;
    using Data;
    using Models;
    using Models.DTOs;
    using System;
    using System.Linq;

    public class LoginController : ILoginController
    {
        private static LoginController instance;

        private LoginController()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDto>()
                                       // .ForMember(dest => dest.Bids, opt => opt.MapFrom(src => string.Join(Environment.NewLine, src.Bids.Select(b => $"Product: {b.Product.Name}, Coins: {b.Coins}, Date: {b.DateOfCreated}, IsWon: {b.IsWon}"))))
                                        .ForMember(dest => dest.Invoices, opt => opt.MapFrom(src => string.Join(Environment.NewLine, src.Invoices.Select(i => $"Product: {i.Product.Name} - User: {i.User.Name}"))))
                                        .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => string.Join(Environment.NewLine, src.Payments.Select(p => $"Type: {p.Type} - Code: {p.PaymentTypeCode}"))))
                                        .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => string.Join(Environment.NewLine, src.Payments.Select(p => p.Id.ToString()))));
                cfg.CreateMap<Bid, BidDto>()
                                        .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                                        .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<Invoice, InvoiceDto>();
                                        //.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Name))
                                        //.ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.Name))
                                        //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                                        //.ForMember(dest => dest.DateOfIssued, opt => opt.MapFrom(src => src.Product.EndDate));
                cfg.CreateMap<Payment, PaymentDto>();
               
            });
        }

        public static LoginController Instance()
        {
            if (instance == null)
            {
                instance = new LoginController();
            }

            return instance;
        }

        public bool ValidateLogin(string username, string password)
        {
            using (var db = new AuctionContext())
            {
                var hashedPW = HashingSHA256.ComputeHash(password);
                return db.Users.Any(u => u.Username == username && u.Password == hashedPW);
            }
        }
    }
}