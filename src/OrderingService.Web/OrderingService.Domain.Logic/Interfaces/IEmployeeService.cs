using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        Task<IPagedResult<EmployeeProfileDTO>> GetPagedEmployeesAsync(string serviceName, decimal? maxServiceCost,
            int pageSize, int pageNumber, CancellationToken token);

        Task<IResult<EmployeeProfileDTO>> CreateEmployeeAsync(EmployeeProfileDTO employeeProfileDto,
            CancellationToken token);

        Task<IResult<EmployeeProfileDTO>> UpdateEmployeeAsync(EmployeeProfileDTO employeeProfileDto,
            CancellationToken token);

        Task<IResult<EmployeeProfileDTO>> DeleteEmployeeAsync(Guid employeeId, CancellationToken token);
    }
}
