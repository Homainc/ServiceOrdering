using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        Task<IPagedResult<EmployeeProfileDTO>> GetPagedEmployeesAsync(string serviceName, decimal? maxServiceCost,
            int pageSize, int pageNumber, CancellationToken token);

        Task<EmployeeProfileDTO> CreateEmployeeAsync(EmployeeProfileDTO employeeProfileDto,
            CancellationToken token);

        Task<EmployeeProfileDTO> UpdateEmployeeAsync(EmployeeProfileDTO employeeProfileDto,
            CancellationToken token);

        Task<EmployeeProfileDTO> DeleteEmployeeAsync(Guid employeeId, CancellationToken token);
    }
}
