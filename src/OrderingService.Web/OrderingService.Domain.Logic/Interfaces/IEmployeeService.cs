using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        IEnumerable<EmployeeProfileDTO> FilterEmployeeProfiles(string serviceName, decimal? maxServiceCost);
        IOperationResult CreateEmployeeProfile(EmployeeProfileDTO employeeProfileDto);
        IOperationResult UpdateEmployeeService(EmployeeProfileDTO employeeProfileDto);
        IOperationResult DeleteEmployeeProfile(string employeeId);
    }
}
