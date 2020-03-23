using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IResponse<UserDTO>> SignUpAsync(UserDTO userDto, CancellationToken token);
        Task<IResponse<UserDTO>> CreateAsync(UserDTO userDto, CancellationToken token);
        Task<IResponse<UserDTO>> AuthenticateAsync(UserDTO userDto, CancellationToken token);
        Task<IResponse<UserDTO>> GetUserByIdAsync(Guid id, CancellationToken token);
        Task<IResponse<UserDTO>> UpdateProfileAsync(UserDTO userDto, CancellationToken token);
    }
}
