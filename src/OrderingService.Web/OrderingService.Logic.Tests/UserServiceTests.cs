using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OrderingService.Data.EF;
using OrderingService.Data.Models;
using OrderingService.Data.Repositories;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Helpers;
using OrderingService.Domain.Logic.Interfaces;
using OrderingService.Domain.Logic.MapperProfiles;
using OrderingService.Domain.Logic.Services;
using Xunit;

namespace OrderingService.Logic.Tests
{
    public class UserServiceTests
    {
        private UserService FakeService(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
            var context = new ApplicationContext(options);
            var uow = new ApplicationUnitOfWork(context);
            var mapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMapperProfile>();
            });
            var mapper = new Mapper(mapperCfg);

            var logger = Mock.Of<ILogger<UserService>>();

            var hasher = new PasswordHasher<User>();

            var mockOptions = new Mock<IOptions<AppSettings>>();
            var appSettings = new AppSettings {Secret = "db_test1111111111111111111111111111111111111111111111"};
            mockOptions.Setup(x => x.Value).Returns(appSettings);

            return new UserService(uow, logger, mapper, hasher, mockOptions.Object);
        }

        private UserDTO DefaultUser => new UserDTO
        {
            Email = "test1@test.net",
            FirstName = "test1_first_name",
            LastName = "test1_second_name",
            Password = "test1_pwD1@",
            PhoneNumber = "+1234567890",
            Role = "user",
        };

        [Fact]
        public void Can_create_user()
        {
            // Assign
            var user = DefaultUser;
            IResponse<UserDTO> result;

            // Action
            using (var service = FakeService("Can_create_user"))
            {
                result = service.CreateAsync(user).Result;
            }

            // Assert
            Assert.False(result.DidError);
            Assert.Equal(user.Email, result.Model.Email);
            Assert.Equal("user", user.Role);
        }

        [Fact]
        public void Can_auth_user()
        {
            // Assign
            IResponse<UserDTO> result;
            var user = DefaultUser;

            // Action
            using (var service = FakeService("Can_auth_user"))
            {
                service.CreateAsync(user).Wait();
                result = service.AuthenticateAsync(user).Result;
            }

            // Assert
            Assert.False(result.DidError);
            Assert.NotNull(result.Model.Token);
        }

        [Fact]
        public void Can_not_create_user_with_same_email()
        {
            IResponse<UserDTO> result;
            var user = DefaultUser;
            var sameEmailUser = new UserDTO
            {
                FirstName = "tes",
                LastName = "sss",
                Password = "test1_pwD1@",
                PhoneNumber = "+121137890",
                Role = "user"
            };
            sameEmailUser.Email = user.Email;

            // Action
            using(var service = FakeService("Can_not_create_user_with_same_email"))
            {
                service.CreateAsync(user).Wait();
                result = service.CreateAsync(sameEmailUser).Result;
            }

            Assert.True(result.DidError);
        } 
    }
}
