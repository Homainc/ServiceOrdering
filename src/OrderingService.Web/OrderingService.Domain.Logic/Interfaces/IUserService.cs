using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IResult<UserDTO>> SignUpAsync(UserDTO userDto, CancellationToken token);
        Task<IResult<UserDTO>> CreateAsync(UserDTO userDto, CancellationToken token);
        Task<IResult<UserDTO>> AuthenticateAsync(UserDTO userDto, CancellationToken token);
        Task<IResult<UserDTO>> GetUserByIdAsync(Guid id, CancellationToken token);
        Task<IResult<UserDTO>> UpdateProfileAsync(UserDTO userDto, CancellationToken token);
    }
}
