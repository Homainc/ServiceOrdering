using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IUserService : IDisposable
    {
        IOperationResult Create(UserDTO userDto);
        ClaimsIdentity Authenticate(UserDTO userDto);
    }
}
