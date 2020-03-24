using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        Task<IPagedResult<EmployeeProfileDTO>> FilterEmployeeProfilesAsync(string serviceName, decimal? maxServiceCost, CancellationToken token);
        Task<IResult<EmployeeProfileDTO>> CreateEmployeeProfileAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token);
        Task<IResult<EmployeeProfileDTO>> UpdateEmployeeServiceAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token);
        Task<IResult<EmployeeProfileDTO>> DeleteEmployeeProfileAsync(Guid employeeId, CancellationToken token);
    }
}
