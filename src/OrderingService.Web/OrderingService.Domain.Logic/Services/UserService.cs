using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

        public async Task<IResponse<UserDTO>> CreateAsync(UserDTO userDto)
        {
            var user = Database.Users.GetAll().SingleOrDefault(x => x.Email == userDto.Email);

            if (user != null)
                return Response<UserDTO>.ValidationError("User with this email already exists");

            user = Mapper.Map<User>(userDto);
            user.HashedPassword = PasswordHasher.HashPassword(user, userDto.Password);
            user.RoleId = Database.Roles.GetAll().SingleOrDefault(x => x.Name == userDto.Role).Id;
            Database.Users.Create(user);

            Database.Save();
            return Response<UserDTO>.Success(Mapper.Map<UserDTO>(user));
        }

        public async Task<IResponse<UserDTO>> AuthenticateAsync(UserDTO userDto)
        {
            var user = Database.Users.GetAll().SingleOrDefault(x => x.Email == userDto.Email);
            if(user == null)
                return Response<UserDTO>.ValidationError("Email or password is wrong");

            var result = PasswordHasher.VerifyHashedPassword(user, user.HashedPassword, userDto.Password);
            if (result == PasswordVerificationResult.Failed)
                return Response<UserDTO>.ValidationError("Email or password is wrong");

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
            var token = tokenHandler.CreateToken(tokenDescriptor);

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

        public IResponse<UserDTO> GetUserById(Guid id)
        {
            var userProfile = Database.Users.GetAll().SingleOrDefault(x => x.Id == id);
            return Response<UserDTO>.Success(Mapper.Map<UserDTO>(userProfile));
        }

        public async Task<IResponse<UserDTO>> UpdateProfileAsync(UserDTO userDto)
        {
            var user = Database.Users.GetAll().SingleOrDefault(x => x.Id == userDto.Id);
            if (user == null)
                Response<UserDTO>.NotFound($"User with id {userDto.Id} not found");

            user.PhoneNumber = userDto.PhoneNumber;
            Database.Users.Update(user);

            var userProfile = Mapper.Map<User>(userDto);
            Database.Users.Update(userProfile);

            Database.Save();
            return Response<UserDTO>.Success(userDto);
        }

        public void Dispose() => Database.Dispose();
    }
}
