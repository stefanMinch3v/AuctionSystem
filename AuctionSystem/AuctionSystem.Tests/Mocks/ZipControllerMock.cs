namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Interfaces;
    using Data;
    using Models;
    using System;

    public class ZipControllerMock : IZipController
    {
        private readonly AuctionContext dbContext;

        public ZipControllerMock(AuctionContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddZip(string zipCode, string country, string city)
        {
            throw new NotImplementedException();
        }

        public Zip GetZipByZipCode(string zipCode)
        {
            throw new NotImplementedException();
        }

        public bool IsZipExisting(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateZip(string zipCode, string property, string value)
        {
            throw new NotImplementedException();
        }

        // TODO

    }
}
