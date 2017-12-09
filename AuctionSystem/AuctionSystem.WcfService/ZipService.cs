namespace AuctionSystem.WcfService
{
    using AutoMapper;
    using Controllers;
    using Contracts;
    using Models;
    using Models.DTOs;
    using System.Collections.Generic;

    public class ZipService : IZipService
    {
        public IList<Zip> GetAllZips()
        {
            return ZipController.Instance().GetAllZips();
        }
    }
}
