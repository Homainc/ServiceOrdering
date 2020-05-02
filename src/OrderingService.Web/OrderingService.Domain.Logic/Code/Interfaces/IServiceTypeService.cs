using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IServiceTypeService
    {
        Task<IEnumerable<ServiceTypeDTO>> GetAllOrderedByProfilesCount();
    }
}
