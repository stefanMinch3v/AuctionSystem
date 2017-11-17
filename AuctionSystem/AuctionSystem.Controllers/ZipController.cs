namespace AuctionSystem.Controllers
{
    using AuctionSystem.Controllers.Common;
    using Data;
    using Interfaces;
    using Models;
    using System;
    using System.Linq;

    public class ZipController : IZipController
    {
        
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
                return db.Zips.SingleOrDefault(z => z.ZipId == zipId) != null;

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
                if(zip != null)
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
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
