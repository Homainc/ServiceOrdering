﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code.Exceptions;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Data.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class UserService : AbstractService, IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, ISaveProvider saveProvider, IMapper mapper,
            IRoleRepository roleRepository, IPasswordHasher<User> passwordHasher, ITokenGenerator tokenGenerator)
            : base(mapper, saveProvider)
        {
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserAuthDto> CreateAsync(UserCreateDto userDto)
        {
            if (await _userRepository.AnyUserAsync(x => x.Email == userDto.Email))
                throw new LogicException($"User with email {userDto.Email} already exists!");

            var user = _mapper.Map<User>(userDto);
            user.HashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            user.RoleId = await _roleRepository.GetRoleIdByNameAsync(userDto.Role);

            _userRepository.Create(user);
            await _saveProvider.SaveAsync();

            return _mapper.Map<UserAuthDto>(user);
        }

        public async Task<UserAuthDto> AuthenticateAsync(string email, string password)
        {
            var user = await GetUserByEmailOrThrowAsync(email);

            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);
            if (result == PasswordVerificationResult.Failed)
                throw new LogicException("Incorrect email or password");

            var userAuthDto = _mapper.Map<UserAuthDto>(user);
            userAuthDto.Token = _tokenGenerator.GenerateUserToken(user);
            return userAuthDto;
        }

        public async Task<UserAuthDto> SignUpAsync(UserCreateDto userDto)
        {
            userDto.Role = "user";
            await CreateAsync(userDto);
            return await AuthenticateAsync(userDto.Email, userDto.Password);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id) =>
            _mapper.Map<UserDto>(await GetUserByIdOrThrowAsync(id));

        public async Task<UserDto> UpdateProfileAsync(UserDto userDto)
        {
            var user = await GetUserByIdOrThrowAsync(userDto.Id);

            _mapper.Map(userDto, user);
            _userRepository.Update(user);
            await _saveProvider.SaveAsync();

            return userDto;
        }

        private async Task<User> GetUserByIdOrThrowAsync(Guid id) =>
            await _userRepository.EagerSingleOrDefaultAsync(x => x.Id == id) ??
            throw new LogicNotFoundException($"User with id {id} not found!");

        private async Task<User> GetUserByEmailOrThrowAsync(string email) =>
            await _userRepository.EagerSingleOrDefaultAsync(x => x.Email == email) ??
            throw new LogicNotFoundException($"User with email {email} not found!");
    }
}
