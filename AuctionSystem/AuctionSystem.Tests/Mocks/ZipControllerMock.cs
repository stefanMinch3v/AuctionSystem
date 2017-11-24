namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Common;
    using Controllers.Contracts;
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

        public void AddZip(Zip zip)
        {
            CoreValidator.ThrowIfNull(zip, nameof(zip));
            CoreValidator.ThrowIfNullOrEmpty(zip.ZipCode, nameof(zip.ZipCode));
            CoreValidator.ThrowIfNullOrEmpty(zip.Country, nameof(zip.Country));
            CoreValidator.ThrowIfNullOrEmpty(zip.City, nameof(zip.City));

            using (var db = dbContext)
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

            using (var db = dbContext)
            {
                return db.Zips.SingleOrDefault(z => z.ZipCode == zipCode);
            }
        }

        public bool IsZipExisting(int zipId)
        {
            CoreValidator.ThrowIfNegativeOrZero(zipId, nameof(zipId));

            using (var db = dbContext)
            {
                return db.Zips.Any(z => z.Id == zipId);
            }
        }


        public bool UpdateZip(Zip zip, string property, string value)
        {
            CoreValidator.ThrowIfNull(zip, nameof(zip));
            CoreValidator.ThrowIfNullOrEmpty(zip.ZipCode, nameof(zip.ZipCode));
            CoreValidator.ThrowIfNullOrEmpty(property, nameof(property));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            using (var db = dbContext)
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

               // db.Entry(zipNew).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
        }

        // TODO

    }
}
