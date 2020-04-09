using System;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeProfileDTO> GetEmployeeByIdAsync(Guid id);
        Task<IPagedResult<EmployeeProfileDTO>> GetPagedEmployeesAsync(int pageSize, int pageNumber, string serviceName, decimal? maxServiceCost, int? minAverageRate);
        Task<EmployeeProfileDTO> CreateEmployeeAsync(EmployeeProfileDTO employeeProfileDto);
        Task<EmployeeProfileDTO> UpdateEmployeeAsync(EmployeeProfileDTO employeeProfileDto);
        Task<EmployeeProfileDTO> DeleteEmployeeAsync(Guid employeeId);
        Task<bool> AnyEmployeeByIdAsync(Guid id);
        Task<bool> AnyEmployeeByUserIdAsync(Guid id);
    }
}
