namespace AuctionSystem.Controllers
{
    using Interfaces;
    using Models;

    public class ZipController : IZipController
    {
        // TODO
        public void AddZip(string zipCode, string country, string city)
        {
            throw new System.NotImplementedException();
        }

        public Zip GetZipByName(string country)
        {
            throw new System.NotImplementedException();
        }

        public bool IsZipExisting(string country)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateZip(Zip zip, string property, string value)
        {
            throw new System.NotImplementedException();
        }
    }
}
