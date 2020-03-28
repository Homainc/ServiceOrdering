using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Helpers;
using OrderingService.Domain.Logic.Code.Exceptions;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Data.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class UserService : AbstractService, IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AppSettings _appSettings;
        private readonly IRepository<User> _users;
        private readonly IRepository<Role> _roles;

        public UserService(IRepository<User> users, ISaveProvider saveProvider, IMapper mapper, 
            IRepository<Role> roles, IPasswordHasher<User> passwordHasher, IOptions<AppSettings> appSettings)
                :base(mapper, saveProvider)
        {
            _passwordHasher = passwordHasher;
            _appSettings = appSettings.Value;
            _users = users;
            _roles = roles;
        }

        public async Task<UserDTO> CreateAsync(UserDTO userDto, CancellationToken token)
        {
            var user = await _users.GetAll()
                .SingleOrDefaultAsync(x => x.Email == userDto.Email, token);

            if (user != null)
                throw new LogicException("User with this email already exists");

            user = _mapper.Map<User>(userDto);
            user.HashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            user.RoleId = (await _roles.GetAll().SingleOrDefaultAsync(x => x.Name == userDto.Role, token)).Id;
            _users.Create(user);

            await _saveProvider.SaveAsync(token);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> AuthenticateAsync(UserDTO userDto, CancellationToken token)
        {
            var user = await _users.GetAll().SingleOrDefaultAsync(x => x.Email == userDto.Email, token);
            if(user == null)
                throw new LogicException("Email or password is wrong");

            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, userDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new LogicException("Email or password is wrong");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var userToken = tokenHandler.CreateToken(tokenDescriptor);

            userDto = _mapper.Map<UserDTO>(user);
            userDto.Token = tokenHandler.WriteToken(userToken);
            return userDto;
        }

        public async Task<UserDTO> SignUpAsync(UserDTO userDto, CancellationToken token)
        {
            userDto.Role = "user";
            await CreateAsync(userDto, token);
            return await AuthenticateAsync(userDto, token);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id, CancellationToken token)
        {
            var userProfile = await _users.GetAll().SingleOrDefaultAsync(x => x.Id == id, token);
            if(userProfile == null)
                throw new LogicException($"User with id {id} not found");
            
            return _mapper.Map<UserDTO>(userProfile);
        }

        public async Task<UserDTO> UpdateProfileAsync(UserDTO userDto, CancellationToken token)
        {
            var user = await _users.GetAll().SingleOrDefaultAsync(x => x.Id == userDto.Id, token);
            if (user == null)
                throw new LogicException($"User with id {userDto.Id} not found");

            _mapper.Map(userDto, user);
            _users.Update(user);

            await _saveProvider.SaveAsync(token);
            return userDto;
        }
    }
}
