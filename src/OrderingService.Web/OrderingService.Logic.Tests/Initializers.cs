using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using OrderingService.Data.Repositories;
using OrderingService.Data.EF;
using OrderingService.Data.Models;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Helpers;
using OrderingService.Domain.Logic.MapperProfiles;
using OrderingService.Domain.Logic.Services;

namespace OrderingService.Logic.Tests
{
    public static class Initializers
    {
        private static Mapper Mapper
        {
            get
            {
                var mapperCfg = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<UserMapperProfile>();
                    cfg.AddProfile<EmployeeMapperProfile>();
                    cfg.AddProfile<ReviewMapperProfile>();
                    cfg.AddProfile<OrderMapperProfile>();

                });
                return new Mapper(mapperCfg);
            }
        }

        public static UserDTO DefaultUser => new UserDTO
        {
            Email = "test1@test.net",
            FirstName = "test1_first_name",
            LastName = "test1_second_name",
            Password = "test1_pwD1@",
            PhoneNumber = "+1234567890",
            Role = "user",
        };

        public static EmployeeProfileDTO DefaultEmployeeProfile => new EmployeeProfileDTO
        {
            Description = "test",
            ServiceCost = 1,
            ServiceType = "IT",
            User = DefaultUser
        };

        public static ApplicationContext FakeContext(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new ApplicationContext(options);
        }

        public static UserService FakeUserService(ApplicationContext db)
        {
            var mockOptions = new Mock<IOptions<AppSettings>>();
            var appSettings = new AppSettings { Secret = "db_test1111111111111111111111111111111111111111111111" };
            mockOptions.Setup(x => x.Value).Returns(appSettings);

            return new UserService(new UserRepository(db), new SaveProvider(db), Mapper, 
                new RoleRepository(db), new PasswordHasher<User>(), new JwtTokenGenerator(mockOptions.Object));
        }

        public static EmployeeService FakeEmployeeService(ApplicationContext db) => new EmployeeService(
            new EmployeeProfileRepository(db), new UserRepository(db), new ServiceTypeRepository(db), new SaveProvider(db), Mapper);

        public static OrderService FakeOrderService(ApplicationContext db) => new OrderService(
            new ServiceOrderRepository(db), new EmployeeProfileRepository(db), new UserRepository(db), Mapper,
            new SaveProvider(db));

        public static ReviewService FakeReviewService(ApplicationContext db) => new ReviewService(
            new ReviewRepository(db), new EmployeeProfileRepository(db), new UserRepository(db), Mapper,
            new SaveProvider(db));
    }
}
