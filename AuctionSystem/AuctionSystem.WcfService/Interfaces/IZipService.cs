namespace AuctionSystem.WcfService.Interfaces
{
    using Models;
    using System.ServiceModel;

    [ServiceContract]
    public interface IZipService
    {
        [OperationContract]
        void AddZip(string zipCode, string country, string city);

        [OperationContract]
        bool IsZipExisting(string country);

        [OperationContract]
        bool UpdateZip(Zip zip, string property, string value);

        [OperationContract]
        Zip GetZipByName(string country);
    }
}
