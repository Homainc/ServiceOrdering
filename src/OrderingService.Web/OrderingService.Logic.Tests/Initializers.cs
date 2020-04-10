using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Microsoft.AspNetCore.Http;
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

        private static IHttpContextAccessor FakeHttpContextAccessor(){
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.RequestAborted).Returns(CancellationToken.None);
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);
            return mockHttpContextAccessor.Object;
        }

        public static UserService FakeUserService(ApplicationContext db)
        {
            var mockOptions = new Mock<IOptions<AppSettings>>();
            var appSettings = new AppSettings { Secret = "db_test1111111111111111111111111111111111111111111111" };
            mockOptions.Setup(x => x.Value).Returns(appSettings);
            var hca = FakeHttpContextAccessor();

            return new UserService(new UserRepository(db, hca), new SaveProvider(db, hca), Mapper, 
                new RoleRepository(db, hca), new PasswordHasher<User>(), new JwtTokenGenerator(mockOptions.Object));
        }

        public static EmployeeService FakeEmployeeService(ApplicationContext db){ 
            var hca = FakeHttpContextAccessor();
            return new EmployeeService(new EmployeeProfileRepository(db, hca), new UserRepository(db, hca), new ServiceTypeRepository(db, hca),
                new SaveProvider(db, hca), Mapper);
        }
        public static OrderService FakeOrderService(ApplicationContext db) { 
            var hca = FakeHttpContextAccessor();
            return new OrderService(new ServiceOrderRepository(db, hca), new UserRepository(db, hca), new EmployeeProfileRepository(db, hca),
                Mapper, new SaveProvider(db, hca));
        }
        public static ReviewService FakeReviewService(ApplicationContext db) { 
            var hca = FakeHttpContextAccessor();
            return new ReviewService(new ReviewRepository(db, hca), new UserRepository(db, hca), new EmployeeProfileRepository(db, hca),
                Mapper, new SaveProvider(db, hca));
        }
    }
}
