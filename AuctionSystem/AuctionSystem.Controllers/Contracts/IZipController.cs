namespace AuctionSystem.Controllers.Contracts
{
    using Models;

    public interface IZipController
    {
        void AddZip(Zip zip);

        bool IsZipExisting(int zipId);

        bool UpdateZip(Zip zip);

        Zip GetZipByZipCode(string zipCode);
    }
}
