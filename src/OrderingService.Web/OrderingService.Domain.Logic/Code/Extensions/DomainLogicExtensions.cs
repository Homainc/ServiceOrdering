using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OrderingService.Data.Code.Extensions;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code.Configs;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Domain.Logic.Code.MapperProfiles;
using OrderingService.Domain.Logic.Services;

namespace OrderingService.Domain.Logic.Code.Extensions
{
    public static class DomainLogicExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDataServices(config);

            //configure your Domain Logic Layer services here
            var appSettingSection = config.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            var appSettings = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/notification")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAutoMapper(typeof(UserMapperProfile), typeof(EmployeeMapperProfile),
                typeof(ReviewMapperProfile), typeof(OrderMapperProfile), typeof(ServiceType));
            services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();
            return services;
        }
    }
}
