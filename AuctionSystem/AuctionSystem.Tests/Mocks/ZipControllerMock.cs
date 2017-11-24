namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Common;
    using Controllers.Contracts;
    using Data;
    using Models;
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


        public bool UpdateZip(Zip newZip)
        {
            CoreValidator.ThrowIfNull(newZip, nameof(newZip));
            CoreValidator.ThrowIfNegativeOrZero(newZip.Id, nameof(newZip.Id));
            CoreValidator.ThrowIfNullOrEmpty(newZip.ZipCode, nameof(newZip.ZipCode));
            CoreValidator.ThrowIfNullOrEmpty(newZip.City, nameof(newZip.City));
            CoreValidator.ThrowIfNullOrEmpty(newZip.Country, nameof(newZip.Country));

            using (this.dbContext)
            {
                var dbZip = GetZipByZipCode(newZip.ZipCode);

                CoreValidator.ThrowIfNull(dbZip, nameof(dbZip));

                this.dbContext.Zips.Attach(dbZip);

                dbZip = newZip;

                //this.dbContext.Entry(dbZip).State = EntityState.Modified;
                this.dbContext.SaveChanges();

                return true;
            }
        }
    }
}
