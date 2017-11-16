namespace AuctionSystem.Controllers
{
    using Data;
    using Interfaces;
    using Models;
    using System.Linq;

    public class ZipController : IZipController
    {
        // TODO
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

        public Zip GetZipById(int id)
        {
            using (var db = new AuctionContext())
            {
                return db.Zips.SingleOrDefault(z => z.Id == id);
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
                var zip = GetZipById(id);
                    if(zip == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }

        }

        public bool IsZipExisting(string country)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateZip(int id, string property, string value)
        {
            using (var db = new AuctionContext())
            {
                var zip = GetZipById(id);
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
                    }
                    
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
