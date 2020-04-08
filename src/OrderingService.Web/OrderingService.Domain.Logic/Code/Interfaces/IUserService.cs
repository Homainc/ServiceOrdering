using System;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> SignUpAsync(UserDTO userDto);
        Task<UserDTO> CreateAsync(UserDTO userDto);
        Task<UserDTO> AuthenticateAsync(UserDTO userDto);
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<UserDTO> UpdateProfileAsync(UserDTO userDto);
        Task<bool> AnyUserByIdAsync(Guid id);
    }
}
