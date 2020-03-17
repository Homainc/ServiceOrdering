using System;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using OrderingService.Data.Interfaces;
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

        public IOperationResult Create(UserDTO userDto)
        {
            // TODO: User registration
            throw new NotImplementedException();
        }

        public ClaimsIdentity Authenticate(UserDTO userDto)
        {
            // TODO: User authentication
            throw new NotImplementedException();
        }

        public void Dispose() => Database.Dispose();
    }
}
