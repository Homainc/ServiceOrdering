using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OrderingService.Domain.Logic;
using OrderingService.Web.Code.Filters;
using OrderingService.Web.Code.Validators;

namespace OrderingService.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDomainServices(Configuration);

            services.AddScoped<EmployeeExistFilter>();
            services.AddScoped<EmployeeNonExistFilter>();
            services.AddScoped<OrderExistFilter>();
            services.AddScoped<OrderClientAndEmployeeExistFilter>();

            services.AddOpenApiDocument();

            services.AddControllersWithViews(opt => {
                opt.Filters.Add(typeof(LoggingAttribute));
                opt.Filters.Add(typeof(LogicExceptionAttribute));
            })
                .AddNewtonsoftJson(cfg =>
                    {
                        cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    })
                .AddFluentValidation(fv =>
                {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fv.RegisterValidatorsFromAssemblyContaining<UserDtoValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<OrderDtoValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<ReviewDtoValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<EmployeeProfileDtoValidator>();
                });
            services.AddAutoMapper(typeof(Startup).Assembly);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi().UseSwaggerUi3();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
