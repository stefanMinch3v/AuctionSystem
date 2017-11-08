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

        // TODO
        public void AddZip(string zipCode, string country, string city)
        {
            throw new NotImplementedException();
        }

        public Zip GetZipByName(string country)
        {
            throw new NotImplementedException();
        }

        public bool IsZipExisting(string country)
        {
            throw new NotImplementedException();
        }

        public bool UpdateZip(Zip zip, string property, string value)
        {
            throw new NotImplementedException();
        }
    }
}
