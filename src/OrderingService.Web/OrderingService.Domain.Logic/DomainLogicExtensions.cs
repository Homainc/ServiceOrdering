using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OrderingService.Data;
using OrderingService.Domain.Logic.Interfaces;
using OrderingService.Domain.Logic.Services;

namespace OrderingService.Domain.Logic
{
    public static class DomainLogicExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddDataServices();
            //configure your Domain Logic Layer services here
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
