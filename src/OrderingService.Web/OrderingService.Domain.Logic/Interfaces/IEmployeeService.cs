using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        Task<IResponse<IEnumerable<EmployeeProfileDTO>>> FilterEmployeeProfilesAsync(string serviceName, decimal? maxServiceCost, CancellationToken token);
        Task<IResponse<EmployeeProfileDTO>> CreateEmployeeProfileAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token);
        Task<IResponse<EmployeeProfileDTO>> UpdateEmployeeServiceAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token);
        Task<IResponse<EmployeeProfileDTO>> DeleteEmployeeProfileAsync(Guid employeeId, CancellationToken token);
    }
}
