using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OrderingService.Data;
using OrderingService.Domain.Logic.Interfaces;
using OrderingService.Domain.Logic.MapperProfiles;
using OrderingService.Domain.Logic.Services;

namespace OrderingService.Domain.Logic
{
    public static class DomainLogicExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddDataServices();

            //configure your Domain Logic Layer services here
            services.AddAutoMapper(typeof(UserMapperProfile), typeof(EmployeeMapperProfile), typeof(ReviewMapperProfile), typeof(OrderMapperProfile));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IOrderService, OrderService>();
            return services;
        }
    }
}
