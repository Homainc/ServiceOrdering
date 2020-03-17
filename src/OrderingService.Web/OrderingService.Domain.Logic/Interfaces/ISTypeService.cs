using System.Collections.Generic;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface ISTypeService
    {
        IEnumerable<ServiceTypeDTO> List();
        IEnumerable<ServiceTypeDTO> Search(string searchName);
        IOperationResult Create(ServiceTypeDTO serviceTypeDto);
        IOperationResult Delete(ServiceTypeDTO serviceTypeDto);
    }
}
