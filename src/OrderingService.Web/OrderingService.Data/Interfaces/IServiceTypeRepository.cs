﻿using System.Threading.Tasks;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IServiceTypeRepository : IRepository<ServiceType>
    {
        Task<ServiceType> GetByNameOrCreateNewAsync(string name);
    }
}