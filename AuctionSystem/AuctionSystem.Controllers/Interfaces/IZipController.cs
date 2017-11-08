namespace AuctionSystem.Controllers.Interfaces
{
    using Models;

    public interface IZipController
    {
        void AddZip(string zipCode, string country, string city);

        bool IsZipExisting(string country);

        bool UpdateZip(Zip zip, string property, string value);

        Zip GetZipByName(string country);
    }
}
