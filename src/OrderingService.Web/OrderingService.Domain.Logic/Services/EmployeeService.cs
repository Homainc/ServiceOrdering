using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private IMapper Mapper { get; }
        public EmployeeService(IUnitOfWork db, ILogger<EmployeeService> logger, IMapper mapper)
        {
            Database = db;
            Logger = logger;
            Mapper = mapper;
        }

        public void Dispose() => Database.Dispose();

        public IEnumerable<EmployeeProfileDTO> FilterEmployeeProfiles(string serviceName, decimal? maxServiceCost)
        {
            var employee = Database.EmployeeProfiles.GetAll();
            if (serviceName != null)
                employee = employee.Where(e => e.ServiceType.Name.Contains(serviceName));
            if (maxServiceCost.HasValue)
                employee = employee.Where(e => e.ServiceCost <= maxServiceCost.Value);

            return employee.ProjectTo<EmployeeProfileDTO>(Mapper.ConfigurationProvider);
        }

        public IOperationResult CreateEmployeeProfile(EmployeeProfileDTO employeeProfileDto)
        {
            var employeeProfile = Database.EmployeeProfiles.GetAll().SingleOrDefault(x => x.Id == employeeProfileDto.Id);

            if (employeeProfile != null)
            {
                var result = OperationResult.Error("Employee profile already exist");
                Logger.LogError(result.ErrorMessage);
                return result;
            }

            var serviceType = Database.ServiceTypes.GetAll()
                .SingleOrDefault(s => s.Name.Equals(employeeProfileDto.ServiceType.ToLower()));
            if (serviceType == null)
            {
                serviceType = new ServiceType {Name = employeeProfileDto.ServiceType.ToLower()};
                Database.ServiceTypes.Create(serviceType);
            }

            employeeProfile = new EmployeeProfile
            {
                Id = employeeProfileDto.Id,
                ServiceCost = employeeProfileDto.ServiceCost,
                ServiceType = serviceType
            };
            Database.EmployeeProfiles.Create(employeeProfile);
            Database.Save();

            Logger.LogInformation($"Employee Profile(cost: {employeeProfile.ServiceCost}, service name: {employeeProfile.ServiceType.Name}) was added");
            return OperationResult.Success();
        }

        public IOperationResult UpdateEmployeeService(EmployeeProfileDTO employeeProfileDto)
        {
            var employeeProfile = Database.EmployeeProfiles.GetAll().SingleOrDefault(x => x.Id == employeeProfileDto.Id);

            if (employeeProfile == null)
            {
                var result = OperationResult.Error($"Employee profile with id {employeeProfileDto.Id} not found");
                Logger.LogError(result.ErrorMessage);
                return result;
            }

            var serviceType = Database.ServiceTypes.GetAll()
                .SingleOrDefault(s => s.Name.Equals(employeeProfileDto.ServiceType.ToLower()));
            if (serviceType == null)
            {
                serviceType = new ServiceType {Name = employeeProfileDto.ServiceType.ToLower()};
                Database.ServiceTypes.Create(serviceType);
            }

            employeeProfile.ServiceCost = employeeProfileDto.ServiceCost;
            employeeProfile.ServiceType = serviceType;

            Database.EmployeeProfiles.Update(employeeProfile);
            Database.Save();

            Logger.LogInformation($"Employee Profile(cost: {employeeProfile.ServiceCost}, service name: {employeeProfile.ServiceType.Name}) was updated");
            return OperationResult.Success();
        }

        public IOperationResult DeleteEmployeeProfile(string employeeId)
        {
            var employeeProfile = Database.EmployeeProfiles.GetAll().SingleOrDefault(e => e.Id == employeeId);

            if (employeeProfile == null)
            {
                var result = OperationResult.Error("Employee profile not found");
                Logger.LogError(result.ErrorMessage);
            }

            Database.EmployeeProfiles.Delete(employeeProfile);
            Database.Save();

            Logger.LogInformation($"Employee profile from user id {employeeId} was deleted");
            return OperationResult.Success();
        }
    }
}
