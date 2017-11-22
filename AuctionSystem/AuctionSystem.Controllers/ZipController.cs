namespace AuctionSystem.Controllers
{
    using Common;
    using Data;
    using Contracts;
    using Models;
    using System;
    using System.Linq;

    public class ZipController : IZipController
    {
        private static ZipController instance;

        private ZipController()
        {
        }

        public static ZipController Instance()
        {
            if (instance == null)
            {
                instance = new ZipController();
            }

            return instance;
        }

        public void AddZip(string zipCode, string country, string city)
        {
            CoreValidator.ThrowIfNullOrEmpty(zipCode, nameof(zipCode));
            CoreValidator.ThrowIfNullOrEmpty(country, nameof(country));
            CoreValidator.ThrowIfNullOrEmpty(city, nameof(city));

            using (var db = new AuctionContext())
            {
                var zip = new Zip
                {
                    ZipCode = zipCode,
                    Country = country,
                    City = city
                };

                db.Zips.Add(zip);
                db.SaveChanges();
            }

        }

        public Zip GetZipByZipCode(string zipCode)
        {
            CoreValidator.ThrowIfNullOrEmpty(zipCode, nameof(zipCode));

            using (var db = new AuctionContext())
            {
                return db.Zips.SingleOrDefault(z => z.ZipCode == zipCode);
            }
        }

        public bool IsZipExisting(int zipId)
        {
            CoreValidator.ThrowIfNegativeOrZero(zipId, nameof(zipId));

            using (var db = new AuctionContext())
            {
                return db.Zips.Any(z => z.ZipId == zipId);
            }
        }


        public bool UpdateZip(string zipCode, string property, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(zipCode, nameof(zipCode));
            CoreValidator.ThrowIfNullOrEmpty(property, nameof(property));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            using (var db = new AuctionContext())
            {
                var zip = GetZipByZipCode(zipCode);

                CoreValidator.ThrowIfNull(zip, nameof(zip));

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

                db.SaveChanges();

                return true;
            }
        }
    }
}
