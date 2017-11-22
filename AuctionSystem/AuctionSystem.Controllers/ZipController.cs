using System.Data.Entity;

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

        public void AddZip(Zip zip)
        {
            CoreValidator.ThrowIfNull(zip, nameof(zip));
            CoreValidator.ThrowIfNullOrEmpty(zip.ZipCode, nameof(zip.ZipCode));
            CoreValidator.ThrowIfNullOrEmpty(zip.Country, nameof(zip.Country));
            CoreValidator.ThrowIfNullOrEmpty(zip.City, nameof(zip.City));

            using (var db = new AuctionContext())
            {
                var zipNew = new Zip
                {
                    ZipCode = zip.ZipCode,
                    Country = zip.Country,
                    City = zip.City
                };

                db.Zips.Add(zipNew);
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

        public bool IsZipExisting(Zip zip)
        {
            CoreValidator.ThrowIfNull(zip, nameof(zip));
            CoreValidator.ThrowIfNegativeOrZero(zip.ZipId, nameof(zip.ZipId));

            using (var db = new AuctionContext())
            {
                return db.Zips.Any(z => z.ZipId == zip.ZipId);
            }
        }


        public bool UpdateZip(Zip zip, string property, string value)
        {
            CoreValidator.ThrowIfNull(zip, nameof(zip));
            CoreValidator.ThrowIfNullOrEmpty(zip.ZipCode, nameof(zip.ZipCode));
            CoreValidator.ThrowIfNullOrEmpty(property, nameof(property));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            using (var db = new AuctionContext())
            {
                var zipNew = GetZipByZipCode(zip.ZipCode);

                CoreValidator.ThrowIfNull(zipNew, nameof(zipNew));

                db.Zips.Attach(zipNew);
                
                switch (property)
                {
                    case "ZipCode":
                        zipNew.ZipCode = value;
                        break;
                    case "Country":
                        zipNew.Country = value;
                        break;
                    case "City":
                        zipNew.City = value;
                        break;
                    default:
                        throw new Exception("There is no such property!");
                }

                db.Entry(zipNew).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
        }
    }
}
