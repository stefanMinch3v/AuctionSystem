namespace AuctionSystem.Tests.Mocks
{
    using AuctionSystem.Controllers.Common;
    using Controllers.Interfaces;
    using Data;
    using Models;
    using System;
    using System.Linq;

    public class ZipControllerMock : IZipController
    {
        private readonly AuctionContext dbContext;

        public ZipControllerMock(AuctionContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddZip(string zipCode, string country, string city)
        {
            CoreValidator.ThrowIfNullOrEmpty(zipCode, nameof(zipCode));
            CoreValidator.ThrowIfNullOrEmpty(country, nameof(country));
            CoreValidator.ThrowIfNullOrEmpty(city, nameof(city));
            using (dbContext)
            {
                var zip = new Zip
                {
                    ZipCode = zipCode,
                    Country = country,
                    City = city
                };
                dbContext.Zips.Add(zip);
                dbContext.SaveChanges();

            }
        }

        public Zip GetZipByZipCode(string zipCode)
        {
            CoreValidator.ThrowIfNullOrEmpty(zipCode, nameof(zipCode));
            using (dbContext)
            {
                return dbContext.Zips.SingleOrDefault(z => z.ZipCode == zipCode);
            }
        }

        public bool IsZipExisting(string zipCode)
        {
            CoreValidator.ThrowIfNullOrEmpty(zipCode, nameof(zipCode));
            using (dbContext)
            {
                return dbContext.Zips.SingleOrDefault(z => z.ZipCode == zipCode) != null;

            }
        }

        public bool UpdateZip(string zipCode, string property, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(zipCode, nameof(zipCode));
            using (dbContext)
            {
                var zip = GetZipByZipCode(zipCode);
                if (zip != null)
                {
                    switch (property)
                    {

                        case "ZipCode":
                            zip.ZipCode = value;
                            break;
                        case "Country":
                            zip.Country = value;
                            break;
                        case "City":
                            zip.City = value;
                            break;
                        default:
                            throw new Exception("There is no such property!");
                    }
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static implicit operator ZipControllerMock(InvoiceControllerMock v)
        {
            throw new NotImplementedException();
        }

        // TODO

    }
}
