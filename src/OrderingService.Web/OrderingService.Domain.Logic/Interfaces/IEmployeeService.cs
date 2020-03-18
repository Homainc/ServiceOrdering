using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        IResponse<IEnumerable<EmployeeProfileDTO>> FilterEmployeeProfiles(string serviceName, decimal? maxServiceCost);
        IResponse<EmployeeProfileDTO> CreateEmployeeProfile(EmployeeProfileDTO employeeProfileDto);
        IResponse<EmployeeProfileDTO> UpdateEmployeeService(EmployeeProfileDTO employeeProfileDto);
        IResponse<EmployeeProfileDTO> DeleteEmployeeProfile(string employeeId);
    }
}
