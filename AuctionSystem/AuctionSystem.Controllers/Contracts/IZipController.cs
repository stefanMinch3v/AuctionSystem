namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IZipController
    {
        void AddZip(Zip zip);

        bool IsZipExisting(int zipId);

        bool UpdateZip(Zip zip);

        Zip GetZipByZipCode(string zipCode);

        IList<Zip> GetAllZips();
    }
}
