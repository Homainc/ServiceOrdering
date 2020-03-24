﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Helpers;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork Database { get; }
        private ILogger<UserService> Logger { get; }
        private IMapper Mapper { get; }
        private IPasswordHasher<User> PasswordHasher { get; }
        private AppSettings AppSettings { get; }

        public UserService(IUnitOfWork db, ILogger<UserService> logger, IMapper mapper, 
            IPasswordHasher<User> passwordHasher, IOptions<AppSettings> appSettings)
        {
            Database = db;
            Logger = logger;
            Mapper = mapper;
            PasswordHasher = passwordHasher;
            AppSettings = appSettings.Value;
        }

        public async Task<IResult<UserDTO>> CreateAsync(UserDTO userDto, CancellationToken token)
        {
            var user = await Database.Users.GetAll()
                .SingleOrDefaultAsync(x => x.Email == userDto.Email, token);

            if (user != null)
                return new Result<UserDTO>("User with this email already exists");

            user = Mapper.Map<User>(userDto);
            user.HashedPassword = PasswordHasher.HashPassword(user, userDto.Password);
            user.RoleId = (await Database.Roles.GetAll().SingleOrDefaultAsync(x => x.Name == userDto.Role, token)).Id;
            await Database.Users.CreateAsync(user, token);

            await Database.SaveAsync(token);
            return new Result<UserDTO>(Mapper.Map<UserDTO>(user));
        }

        public async Task<IResult<UserDTO>> AuthenticateAsync(UserDTO userDto, CancellationToken token)
        {
            var user = await Database.Users.GetAll().SingleOrDefaultAsync(x => x.Email == userDto.Email, token);
            if(user == null)
                return new Result<UserDTO>("Email or password is wrong");

            var result = PasswordHasher.VerifyHashedPassword(user, user.HashedPassword, userDto.Password);
            if (result == PasswordVerificationResult.Failed)
                return new Result<UserDTO>("Email or password is wrong");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
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

            userDto = Mapper.Map<UserDTO>(user);
            userDto.Token = tokenHandler.WriteToken(userToken);
            return new Result<UserDTO>(userDto);
        }

        public async Task<IResult<UserDTO>> SignUpAsync(UserDTO userDto, CancellationToken token)
        {
            userDto.Role = "user";
            var result = await CreateAsync(userDto, token);
            if (result.DidError)
                return result;
            return await AuthenticateAsync(userDto, token);
        }

        public async Task<IResult<UserDTO>> GetUserByIdAsync(Guid id, CancellationToken token)
        {
            var userProfile = await Database.Users.GetAll().SingleOrDefaultAsync(x => x.Id == id, token);
            if(userProfile == null)
                return new Result<UserDTO>($"User with id {id} not found");
            return new Result<UserDTO>(Mapper.Map<UserDTO>(userProfile));
        }

        public async Task<IResult<UserDTO>> UpdateProfileAsync(UserDTO userDto, CancellationToken token)
        {
            var user = await Database.Users.GetAll().SingleOrDefaultAsync(x => x.Id == userDto.Id, token);
            if (user == null)
                return new Result<UserDTO>($"User with id {userDto.Id} not found");

            user.PhoneNumber = userDto.PhoneNumber;
            Database.Users.Update(user);

            var userProfile = Mapper.Map<User>(userDto);
            Database.Users.Update(userProfile);

            await Database.SaveAsync(token);
            return new Result<UserDTO>(userDto);
        }

        public void Dispose() => Database.Dispose();
    }
}
