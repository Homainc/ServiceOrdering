using System;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeProfileDTO> GetEmployeeByIdAsync(Guid id);

        Task<IPagedResult<EmployeeProfileDTO>> GetPagedEmployeesAsync(int pageSize, int pageNumber, string searchString,
            decimal? maxServiceCost, int? minAverageRate, int? serviceTypeId);
        Task<EmployeeProfileDTO> CreateEmployeeAsync(EmployeeProfileDTO employeeProfileDto);
        Task<EmployeeProfileDTO> UpdateEmployeeAsync(EmployeeProfileDTO employeeProfileDto);
        Task<EmployeeProfileDTO> DeleteEmployeeAsync(Guid employeeId);
        Task<Guid> GetUserIdByEmployeeIdAsync(Guid employeeId);
    }
}
