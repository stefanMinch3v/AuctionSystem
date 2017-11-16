namespace AuctionSystem.Controllers
{
    using Data;
    using Interfaces;
    using Models;
    using System;
    using System.Linq;

    public class ZipController : IZipController
    {
        
        public void AddZip(string zipCode, string country, string city)
        {
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
            using (var db = new AuctionContext())
            {
                return db.Zips.SingleOrDefault(z => z.ZipCode == zipCode);
            }
        }

        public Zip GetZipByName(string country)
        {
            throw new System.NotImplementedException();
        }

        public bool IsZipExisting(int id)
        {
            using (var db = new AuctionContext())
            {
                return db.Users.Any(z => z.Id == id);
                
            }

        }

        public bool IsZipExisting(string country)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateZip(string zipCode, string property, string value)
        {
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
            return true;
        }

        public bool UpdateZip(Zip zip, string property, string value)
        {
            throw new System.NotImplementedException();
        }
    }
}
