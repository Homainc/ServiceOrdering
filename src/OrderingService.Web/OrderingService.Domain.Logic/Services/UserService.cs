using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork Database { get; }
        private ILogger<UserService> Logger { get; }
        private IMapper Mapper { get; }

        public UserService(IUnitOfWork db, ILogger<UserService> logger, IMapper mapper)
        {
            Database = db;
            Logger = logger;
            Mapper = mapper;
        }

        public async Task<IOperationResult> CreateAsync(UserDTO userDto)
        {
            var user = await Database.UserManager.FindByEmailAsync(userDto.Email);

            if (user != null)
                return OperationResult.Error("User with this email already exists");

            user = Mapper.Map<User>(userDto);
            var result = await Database.UserManager.CreateAsync(user, userDto.Password);
            if (result.Errors.Any())
                return OperationResult.Error(result.Errors.First().Description);

            await Database.UserManager.AddToRoleAsync(user, userDto.Role);

            Database.Save();
            return OperationResult.Success();
        }

        public Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            // TODO: User authentication
            throw new NotImplementedException();
        }

        public void Dispose() => Database.Dispose();
    }
}
