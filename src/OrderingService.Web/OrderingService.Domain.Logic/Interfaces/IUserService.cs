﻿using System;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IResponse<UserDTO>> SignUpAsync(UserDTO userDto);
        Task<IResponse<UserDTO>> CreateAsync(UserDTO userDto);
        Task<IResponse<UserDTO>> AuthenticateAsync(UserDTO userDto);
        IResponse<UserDTO> GetUserById(string email);
        Task<IResponse<UserDTO>> UpdateProfileAsync(UserDTO userDto);
    }
}
