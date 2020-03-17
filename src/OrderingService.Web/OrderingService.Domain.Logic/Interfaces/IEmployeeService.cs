using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        IEnumerable<EmployeeProfileDTO> FilterEmployeeProfiles(string serviceName, decimal? maxServiceCost);
        IOperationResult CreateEmployeeProfile(string userId, EmployeeProfileDTO employeeProfileDto);
        IOperationResult DeleteEmployeeProfile(string userId);
    }
}
