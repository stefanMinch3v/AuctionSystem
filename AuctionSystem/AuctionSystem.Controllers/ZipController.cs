
namespace AuctionSystem.Controllers
{
    using Common;
    using Contracts;
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity;
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

        public bool IsZipExisting(int zipId)
        {
            CoreValidator.ThrowIfNegativeOrZero(zipId, nameof(zipId));

            using (var db = new AuctionContext())
            {
                return db.Zips.Any(z => z.Id == zipId);
            }
        }

        public IList<Zip> GetAllZips()
        {
            using (var db = new AuctionContext())
            {
                return db.Zips.ToList();
            }
        }

        public bool UpdateZip(Zip newZip)
        {
            CoreValidator.ThrowIfNull(newZip, nameof(newZip));
            CoreValidator.ThrowIfNegativeOrZero(newZip.Id, nameof(newZip.Id));
            CoreValidator.ThrowIfNullOrEmpty(newZip.ZipCode, nameof(newZip.ZipCode));
            CoreValidator.ThrowIfNullOrEmpty(newZip.City, nameof(newZip.City));
            CoreValidator.ThrowIfNullOrEmpty(newZip.Country, nameof(newZip.Country));

            using (var db = new AuctionContext())
            {
                var dbZip = GetZipByZipCode(newZip.ZipCode);

                CoreValidator.ThrowIfNull(dbZip, nameof(dbZip));

                db.Zips.Attach(dbZip);

                dbZip = newZip;

                db.Entry(dbZip).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
        }
    }
}
