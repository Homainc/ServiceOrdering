using System.Collections.Generic;
using System.Threading.Tasks;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IServiceTypeRepository : IRepository<ServiceType>
    {
        Task<ServiceType> GetByNameOrCreateNewAsync(string name);
        Task<IEnumerable<ServiceType>> GetAllOrderedByProfilesCount();
    }
}
