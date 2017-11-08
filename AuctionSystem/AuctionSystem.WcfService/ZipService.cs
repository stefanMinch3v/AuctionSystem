namespace AuctionSystem.WcfService
{
    using Interfaces;
    using System;
    using AuctionSystem.Models;

    public class ZipService : IZipService
    {
        // TODO in each method return the controller that supposed to handle its operation

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
