using System.Linq.Expressions;
using System;
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
        private readonly IUserRepository _users;
        private readonly IRoleRepository _roles;

        public UserService(IUserRepository users, ISaveProvider saveProvider, IMapper mapper,
            IRoleRepository roles, IPasswordHasher<User> passwordHasher, ITokenGenerator tokenGenerator)
            : base(mapper, saveProvider)
        {
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _users = users;
            _roles = roles;
        }

        public async Task<UserDTO> CreateAsync(UserDTO userDto)
        {
            // TODO: Implement this check in a filter
            if (await _users.AnyUserAsync(x => x.Email == userDto.Email))
                throw new LogicException("User with this email already exists");

            var user = _mapper.Map<User>(userDto);
            user.HashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            user.RoleId = await _roles.GetRoleIdByNameAsync(userDto.Role);
            _users.Create(user);

            await _saveProvider.SaveAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> AuthenticateAsync(UserDTO userDto)
        {
            var user = await FindUserOrThrowAsync(x => x.Email == userDto.Email, "Incorrect email or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, userDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new LogicException("Incorrect email or password");

            userDto = _mapper.Map<UserDTO>(user);
            userDto.Token = _tokenGenerator.GenerateUserToken(user);
            return userDto;
        }

        public async Task<UserDTO> SignUpAsync(UserDTO userDto)
        {
            userDto.Role = "user";
            await CreateAsync(userDto);
            return await AuthenticateAsync(userDto);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id) => 
            _mapper.Map<UserDTO>(await FindUserOrThrowAsync(x => x.Id == id));

        public async Task<UserDTO> UpdateProfileAsync(UserDTO userDto)
        {
            var user = await FindUserOrThrowAsync(x => x.Id == userDto.Id);

            _mapper.Map(userDto, user);
            
            await _saveProvider.SaveAsync();
            return userDto;
        }

        private async Task<User> FindUserOrThrowAsync(Expression<Func<User, bool>> filter, string customExceptionMessage = null)
        {
            var user = await _users.EagerSingleOrDefaultAsync(filter);
            if (user == null)
                throw new LogicNotFoundException(customExceptionMessage ?? $"User not found");
            return user;
        }

        public async Task<bool> AnyUserByIdAsync(Guid id) =>
            await _users.AnyUserAsync(x => x.Id == id);
    }
}
