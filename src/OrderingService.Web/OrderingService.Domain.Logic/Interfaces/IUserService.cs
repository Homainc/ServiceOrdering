using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IUserService : IDisposable
    {
        IOperationResult Create(UserDTO userDto);
        IEnumerable<UserDTO> FilterEmployees(string serviceTypeName = null, decimal? serviceCost = null);
        ClaimsIdentity Authenticate(UserDTO userDto);
        IOperationResult AddEmployeeProfile(UserDTO userDto);
        IOperationResult DeleteEmployeeProfile(UserDTO userDto);
    }
}
