using System;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IUserService
    {
        Task<UserAuthDto> SignUpAsync(UserCreateDto userDto);
        Task<UserAuthDto> AuthenticateAsync(string email, string password);
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> UpdateProfileAsync(UserDto userDto);
    }
}
