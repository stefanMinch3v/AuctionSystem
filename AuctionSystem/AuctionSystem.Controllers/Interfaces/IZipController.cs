namespace AuctionSystem.Controllers.Interfaces
{
    using Models;

    public interface IZipController
    {
        void AddZip(string zipCode, string country, string city);

        bool IsZipExisting(int zipId);

        bool UpdateZip(string zipCode, string property, string value);

        Zip GetZipByZipCode(string zipCode);
    }
}
