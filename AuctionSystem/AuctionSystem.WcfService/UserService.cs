namespace AuctionSystem.WcfService
{
    using AutoMapper;
    using Contracts;
    using Controllers;
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;

    public class UserService : IUserService
    {
        public void CreateUser(User user)
        {
            UserController.Instance().CreateUser(user);
        }

        public bool UpdateUser(UserDto user)
        {
            try
            {
                //var userToUpdate = MapUserDtoToDbUser(user);
                var userToUpdate = MapUserDtoIntoUserLukasVersion(user);
                return UserController.Instance().UpdateUser(userToUpdate);
            }
            catch(Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public UserDto GetUserById(int id)
        {
            var dbUser = UserController.Instance().GetUserByIdWithAllCollections(id);

            return MapDbUserToUserDto(dbUser);
        }

        public UserDto GetUserByUsername(string username)
        {
            var dbUser = UserController.Instance().GetUserByNameWithAllCollections(username);

            return MapDbUserToUserDto(dbUser);
        }

        private User MapUserDtoIntoUserLukasVersion(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Name = userDto.Name,
                DateOfBirth = userDto.DateOfBirth,
                Gender = userDto.Gender,
                Phone = userDto.Phone,
                Email = userDto.Email,
                Address = userDto.Address,
                ZipId = userDto.ZipId,
                Coins = userDto.Coins,
                IsAdmin = userDto.IsAdmin,
                Password = userDto.Password,
                IsDeleted = userDto.IsDeleted
               
            };


            // return Mapper.Map<Payment>(paymentDto);

        }

        public bool DeleteUser(User user)
        {
            return UserController.Instance().DeleteUser(user);
        }

        public bool IsUserExisting(string username)
        {
            return UserController.Instance().IsUserExisting(username);
        }

        public int CountUserBidsForGivenProduct(User user, string productName)
        {
            return UserController.Instance().CountUserBidsForGivenProduct(user, productName);
        }

        public ICollection<ProductDto> GetUserProducts(User user)
        {
            var products = UserController.Instance().GetUserProducts(user);

            return TransferCollectionData(products);
        }

        public bool IsCookieValid(string cookieId)
        {
            return UserController.Instance().IsCookieValid(cookieId);
        }

        public ICollection<BidDto> GetUserBids(User user)
        {
            var bids =  UserController.Instance().GetUserBids(user);

            return TransferCollectionData(bids);
        }

        public ICollection<InvoiceDto> GetUserInvoices(User user)
        {
            var invoices = UserController.Instance().GetUserInvoices(user);

            return TransferCollectionData(invoices);
        }

        public int GetAllUserSpentCoinsForGivenProduct(User user, string productName)
        {
            return UserController.Instance().GetAllUserSpentCoinsForGivenProduct(user, productName);
        }

        private UserDto MapDbUserToUserDto(User dbUser)
        {
            return Mapper.Map<UserDto>(dbUser);
        }

        private User MapUserDtoToDbUser(UserDto userDto)
        {
            return Mapper.Map<User>(userDto);
        }

        private PaymentDto MapDbPaymentToPaymentDto(Payment dbPayment)
        {
            return Mapper.Map<PaymentDto>(dbPayment);
        }

        private ICollection<PaymentDto> TransferCollectionData(IList<Payment> payments)
        {
            var result = new List<PaymentDto>();

            foreach (var payment in payments)
            {
                var productDto = MapDbPaymentToPaymentDto(payment);
                result.Add(productDto);
            }

            return result;
        }

        private InvoiceDto MapDbInvoiceToInvoiceDto(Invoice dbInvoice)
        {
            return Mapper.Map<InvoiceDto>(dbInvoice);
        }

        private ICollection<InvoiceDto> TransferCollectionData(IList<Invoice> invoices)
        {
            var result = new List<InvoiceDto>();

            foreach (var invoice in invoices)
            {
                var invoiceDto = MapDbInvoiceToInvoiceDto(invoice);
                result.Add(invoiceDto);
            }

            return result;
        }

        private BidDto MapDbBidToBidDto(Bid dbBid)
        {
            return Mapper.Map<BidDto>(dbBid);
        }

        private ICollection<BidDto> TransferCollectionData(IList<Bid> bids)
        {
            var result = new List<BidDto>();

            foreach (var bid in bids)
            {
                var bidDto = MapDbBidToBidDto(bid);
                result.Add(bidDto);
            }

            return result;
        }

        private ProductDto MapDbProductToProductDto(Product dbProduct)
        {
            return Mapper.Map<ProductDto>(dbProduct);
        }

        private ICollection<ProductDto> TransferCollectionData(IList<Product> products)
        {
            var result = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = MapDbProductToProductDto(product);
                result.Add(productDto);
            }

            return result;
        }

        public string AddCookie(int userId)
        {
            return UserController.Instance().AddCookie(userId);
        }

        public UserDto GetUserByCookie(string cookieId)
        {
            var user = UserController.Instance().GetUserByCookie(cookieId);
            LoginController.Instance();
            return MapDbUserToUserDto(user);
        }
    }
}
