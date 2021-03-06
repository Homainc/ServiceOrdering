﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderingService.Data.Code.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Data.Repositories;

namespace OrderingService.Data.Code.Extensions
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
            services.AddScoped<IEmployeeRepository, EmployeeProfileRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IServiceOrderRepository, ServiceOrderRepository>();
            services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<ISaveProvider, SaveProvider>();
            return services;
        }
    }
}
