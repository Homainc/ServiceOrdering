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
            services.AddIdentityCore<User>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
            services.AddTransient<IUnitOfWork, ApplicationUnitOfWork>();
            return services;
        }
    }
}
