using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<UserDTO> SignUpAsync(UserDTO userDto, CancellationToken token);
        Task<UserDTO> CreateAsync(UserDTO userDto, CancellationToken token);
        Task<UserDTO> AuthenticateAsync(UserDTO userDto, CancellationToken token);
        Task<UserDTO> GetUserByIdAsync(Guid id, CancellationToken token);
        Task<UserDTO> UpdateProfileAsync(UserDTO userDto, CancellationToken token);
    }
}
