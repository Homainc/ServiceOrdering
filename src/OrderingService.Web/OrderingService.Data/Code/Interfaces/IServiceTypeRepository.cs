using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderingService.Data.Models;

namespace OrderingService.Data.Code.Interfaces
{
    public interface IServiceTypeRepository : IRepository<ServiceType>
    {
        Task<ServiceType> GetByNameOrCreateNewAsync(string name);
        Task<IEnumerable<ServiceType>> GetAllOrderedByProfilesCount();
        Task<ServiceType> GetByIdOrDefaultAsync(int id);

        Task<bool> AnyServiceAsync(Expression<Func<ServiceType, bool>> filter);
    }
}
