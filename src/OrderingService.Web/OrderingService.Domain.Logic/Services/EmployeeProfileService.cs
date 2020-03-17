using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork Database { get; }
        private ILogger<EmployeeService> Logger { get; }
        public EmployeeService(IUnitOfWork db, ILogger<EmployeeService> logger)
        {
            Database = db;
            Logger = logger;
        }

        public void Dispose() => Database.Dispose();

        public IEnumerable<EmployeeProfileDTO> FilterEmployeeProfiles(string serviceName, decimal? maxServiceCost)
        {
            var employee = Database.EmployeeProfiles.GetAll();
            if (serviceName != null)
                employee = employee.Where(e => e.ServiceType.Name.Contains(serviceName));
            if (maxServiceCost.HasValue)
                employee = employee.Where(e => e.ServiceCost <= maxServiceCost.Value);

            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EmployeeProfile, EmployeeProfileDTO>()
                        .IncludeMembers(e => e.User, e => e.ServiceType);
                    cfg.CreateMap<ServiceType, EmployeeProfileDTO>();
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<User, EmployeeProfileDTO>();
                });
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<EmployeeProfile>, IEnumerable<EmployeeProfileDTO>>(employee.AsEnumerable());
        }

        public IOperationResult CreateEmployeeProfile(string userId, EmployeeProfileDTO employeeProfileDto)
        {
            var employeeProfile = Database.EmployeeProfiles.GetAll().SingleOrDefault(x => x.UserId == userId);

            if (employeeProfile != null)
            {
                var result = OperationResult.Error("Employee profile already exist");
                Logger.LogError(result.ErrorMessage);
                return result;
            }

            var serviceType = Database.ServiceTypes.GetAll()
                .SingleOrDefault(s => s.Name.Equals(employeeProfileDto.ServiceType.ToLower()));
            serviceType ??= new ServiceType {Name = employeeProfileDto.ServiceType.ToLower()};
            Database.ServiceTypes.Create(serviceType);

            employeeProfile = new EmployeeProfile
            {
                UserId = userId,
                ServiceCost = employeeProfileDto.ServiceCost,
                ServiceType = serviceType,
            };
            Database.EmployeeProfiles.Create(employeeProfile);
            Database.Save();

            Logger.LogInformation($"Employee Profile(cost: {employeeProfile.ServiceCost}, service name: {employeeProfile.ServiceType.Name}) was added");
            return OperationResult.Success();
        }

        public IOperationResult DeleteEmployeeProfile(string userId)
        {
            var employeeProfile = Database.EmployeeProfiles.GetAll().SingleOrDefault(e => e.UserId == userId);

            if (employeeProfile == null)
            {
                var result = OperationResult.Error("Employee profile not found");
                Logger.LogError(result.ErrorMessage);
            }

            Database.EmployeeProfiles.Delete(employeeProfile.Id);
            Database.Save();

            Logger.LogInformation($"Employee profile from user id {userId} was deleted");
            return OperationResult.Success();
        }
    }
}
