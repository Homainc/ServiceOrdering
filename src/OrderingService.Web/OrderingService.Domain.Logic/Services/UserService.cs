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
        private SignInManager<User> SignInManager { get; }
        private UserManager<User> UserManager { get; }
        private AppSettings AppSettings { get; }

        public UserService(IUnitOfWork db, ILogger<UserService> logger, IMapper mapper, UserManager<User> userManager,
            SignInManager<User> signInManager, IOptions<AppSettings> appSettings)
        {
            Database = db;
            Logger = logger;
            Mapper = mapper;
            SignInManager = signInManager;
            UserManager = userManager;
            AppSettings = appSettings.Value;
        }

        public async Task<IOperationResult> CreateAsync(UserDTO userDto)
        {
            var user = await UserManager.FindByEmailAsync(userDto.Email);

            if (user != null)
                return OperationResult.Error("User with this email already exists");

            user = Mapper.Map<User>(userDto);
            var result = await UserManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
                return OperationResult.Error(result.Errors.First().Description);

            await UserManager.AddToRoleAsync(user, userDto.Role);

            Database.Save();
            return OperationResult.Success();
        }

        public async Task<string> AuthenticateAsync(UserDTO userDto)
        {
            var user = Mapper.Map<User>(userDto);
            var result = await SignInManager.CheckPasswordSignInAsync(user, userDto.Password, false);
            if (!result.Succeeded)
                return null;

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
            return tokenHandler.WriteToken(token);
        }

        public void Dispose()
        {
            UserManager.Dispose();
            Database.Dispose();
        }
    }
}
