using System;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeProfileDto> GetEmployeeByIdAsync(Guid id);

        Task<IPagedResult<EmployeeProfileDto>> GetPagedEmployeesAsync(int pageSize, int pageNumber, string searchString,
            decimal? maxServiceCost, int? minAverageRate, int? serviceTypeId);
        Task<EmployeeProfileDto> CreateEmployeeAsync(EmployeeProfileCreateDto employeeProfileDto);
        Task<EmployeeProfileDto> UpdateEmployeeAsync(EmployeeProfileUpdateDto employeeProfileDto);
        Task<EmployeeProfileDto> DeleteEmployeeAsync(Guid employeeId);
        Task<Guid> GetUserIdByEmployeeIdAsync(Guid employeeId);
    }
}
