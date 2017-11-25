using AuctionSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.WcfService.Contracts
{
    [ServiceContract]
    public interface IZipService
    {

        [OperationContract]
        IList<Zip> GetAllZips();
    }
}
