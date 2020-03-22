using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private UserManager<User> UserManager { get; }
        private AppSettings AppSettings { get; }

        public UserService(IUnitOfWork db, ILogger<UserService> logger, IMapper mapper, UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher, IOptions<AppSettings> appSettings)
        {
            Database = db;
            Logger = logger;
            Mapper = mapper;
            PasswordHasher = passwordHasher;
            UserManager = userManager;
            AppSettings = appSettings.Value;
        }

        public async Task<IResponse<UserDTO>> CreateAsync(UserDTO userDto)
        {
            var user = await UserManager.FindByEmailAsync(userDto.Email);

            if (user != null)
                return Response<UserDTO>.ValidationError("User with this email already exists");

            user = Mapper.Map<User>(userDto);
            var result = await UserManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
                return Response<UserDTO>.ValidationError(result.Errors.First().Description);

            await UserManager.AddToRoleAsync(user, userDto.Role);

            Database.Save();
            return Response<UserDTO>.Success(Mapper.Map<UserDTO>(user));
        }

        public async Task<IResponse<UserDTO>> AuthenticateAsync(UserDTO userDto)
        {
            var user = await UserManager.FindByEmailAsync(userDto.Email);
            if(user == null)
                return Response<UserDTO>.ValidationError("Email or password is wrong");

            var result = PasswordHasher.VerifyHashedPassword(user, user.PasswordHash,userDto.Password);
            if (result == PasswordVerificationResult.Failed)
                return Response<UserDTO>.ValidationError("Email or password is wrong");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            user.UserProfile = Database.UserProfiles.GetAll().SingleOrDefault(x => x.Id == user.Id);
            userDto = Mapper.Map<UserDTO>(user);
            userDto.Token = tokenHandler.WriteToken(token);
            return Response<UserDTO>.Success(userDto);
        }

        public async Task<IResponse<UserDTO>> SignUpAsync(UserDTO userDto)
        {
            userDto.Role = "user";
            var result = await CreateAsync(userDto);
            if (result.DidError)
                return result;
            return await AuthenticateAsync(userDto);
        }

        public IResponse<UserDTO> GetUserById(string id)
        {
            var userProfile = Database.UserProfiles.GetAll().SingleOrDefault(x => x.Id == id);
            return Response<UserDTO>.Success(Mapper.Map<UserDTO>(userProfile));
        }

        public async Task<IResponse<UserDTO>> UpdateProfileAsync(UserDTO userDto)
        {
            var user = await UserManager.FindByIdAsync(userDto.Id);
            if (user == null)
                Response<UserDTO>.NotFound($"User with id {userDto.Id} not found");

            user.PhoneNumber = userDto.PhoneNumber;
            await UserManager.UpdateAsync(user);

            var userProfile = Mapper.Map<UserProfile>(userDto);
            Database.UserProfiles.Update(userProfile);

            Database.Save();
            return Response<UserDTO>.Success(userDto);
        }

        public void Dispose()
        {
            UserManager.Dispose();
            Database.Dispose();
        }
    }
}
