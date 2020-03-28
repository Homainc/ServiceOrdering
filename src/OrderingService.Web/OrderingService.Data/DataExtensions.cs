using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Data.Repositories;

namespace OrderingService.Data
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration config)
        {
            //configure your Data Layer services here
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(config.GetSection("AppSettings:ConnectionString").Value));
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            // Repositories
            services.AddScoped<IRepository<EmployeeProfile>, EmployeeProfileRepository>();
            services.AddScoped<IRepository<Review>, ReviewRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRepository<ServiceOrder>, ServiceOrderRepository>();
            services.AddScoped<IRepository<ServiceType>, ServiceTypeRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();

            services.AddScoped<ISaveProvider, SaveProvider>();
            return services;
        }
    }
}
