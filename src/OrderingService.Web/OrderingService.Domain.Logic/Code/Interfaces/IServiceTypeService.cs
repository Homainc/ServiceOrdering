using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IServiceTypeService
    {
        Task<IEnumerable<ServiceTypeDto>> GetAllOrderedByProfilesCount();
        Task<ServiceTypeDto> CreateAsync(ServiceTypeCreateDto item);
        Task<ServiceTypeDto> UpdateAsync(ServiceTypeDto item);
        Task<ServiceTypeDto> DeleteAsync(int id);
    }
}
