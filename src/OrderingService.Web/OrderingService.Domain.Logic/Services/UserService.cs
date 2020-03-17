using System;
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

        public UserService(IUnitOfWork db, ILogger<UserService> logger)
        {
            Database = db;
            Logger = logger;
        }

        public async Task<IOperationResult> CreateAsync(UserDTO userDto)
        {
            var user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
                return OperationResult.Error("User with this email already exists");
            user = UserMapper.Map<User>(userDto);
            await Database.UserManager.CreateAsync(user, userDto.Password);
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

        private Mapper UserMapper
        {
            get
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UserDTO, User>();
                    cfg.CreateMap<UserDTO, UserProfile>();

                });
                return new Mapper(config);
            }
        }
    }
}
