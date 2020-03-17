using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderingService.Data.EF;
using OrderingService.Data.Models;

namespace OrderingService.Data
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            //configure your Data Layer services here
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OrderingServiceDB;Trusted_Connection=True;MultipleActiveResultSets=true"));
            
            services.AddIdentityCore<User>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
            return services;
        }
    }
}
