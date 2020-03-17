using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IOperationResult> CreateAsync(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
    }
}
