using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IPagedResult<EmployeeProfileDTO>> FilterEmployeeProfilesAsync(string serviceName,
            decimal? maxServiceCost, CancellationToken token)
        {
            var employee = Database.EmployeeProfiles.GetAll();
            if (serviceName != null)
                employee = employee.Where(e => e.ServiceType.Name.Contains(serviceName));
            if (maxServiceCost.HasValue)
                employee = employee.Where(e => e.ServiceCost <= maxServiceCost.Value);

            return new PagedResult<EmployeeProfileDTO>(await employee
                .ProjectTo<EmployeeProfileDTO>(Mapper.ConfigurationProvider).ToListAsync(token));
        }

        public async Task<IResult<EmployeeProfileDTO>> CreateEmployeeProfileAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token)
        {
            var employeeProfile = await Database.EmployeeProfiles.GetAll().SingleOrDefaultAsync(x => x.Id == employeeProfileDto.Id, token);

            if (employeeProfile != null)
            {
                var result = new Result<EmployeeProfileDTO>("Employee profile already exist");
                Logger.LogError(result.ErrorMessage);
                return result;
            }

            var serviceType = await Database.ServiceTypes.GetAll()
                .SingleOrDefaultAsync(s => s.Name.Equals(employeeProfileDto.ServiceType.ToLower()), token);
            if (serviceType == null)
            {
                serviceType = new ServiceType {Name = employeeProfileDto.ServiceType.ToLower()};
                await Database.ServiceTypes.CreateAsync(serviceType, token);
            }

            employeeProfile = new EmployeeProfile
            {
                Id = employeeProfileDto.Id,
                ServiceCost = employeeProfileDto.ServiceCost,
                ServiceType = serviceType
            };
            await Database.EmployeeProfiles.CreateAsync(employeeProfile, token);
            await Database.SaveAsync(token);

            Logger.LogInformation($"Employee Profile(cost: {employeeProfile.ServiceCost}, service name: {employeeProfile.ServiceType.Name}) was added");
            return new Result<EmployeeProfileDTO>(Mapper.Map<EmployeeProfileDTO>(employeeProfile));
        }

        public async Task<IResult<EmployeeProfileDTO>> UpdateEmployeeServiceAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token)
        {
            var employeeProfile = await Database.EmployeeProfiles.GetAll().SingleOrDefaultAsync(x => x.Id == employeeProfileDto.Id, token);

            if (employeeProfile == null)
            {
                var result = new Result<EmployeeProfileDTO>($"Employee profile with id {employeeProfileDto.Id} not found");
                Logger.LogError(result.ErrorMessage);
                return result;
            }

            var serviceType = await Database.ServiceTypes.GetAll()
                .SingleOrDefaultAsync(s => s.Name.Equals(employeeProfileDto.ServiceType.ToLower()), token);
            if (serviceType == null)
            {
                serviceType = new ServiceType {Name = employeeProfileDto.ServiceType.ToLower()};
                await Database.ServiceTypes.CreateAsync(serviceType, token);
            }

            employeeProfile.ServiceCost = employeeProfileDto.ServiceCost;
            employeeProfile.ServiceType = serviceType;

            Database.EmployeeProfiles.Update(employeeProfile);
            await Database.SaveAsync(token);

            Logger.LogInformation($"Employee Profile(cost: {employeeProfile.ServiceCost}, service name: {employeeProfile.ServiceType.Name}) was updated");
            return new Result<EmployeeProfileDTO>(Mapper.Map<EmployeeProfileDTO>(employeeProfile));
        }

        public async Task<IResult<EmployeeProfileDTO>> DeleteEmployeeProfileAsync(Guid employeeId, CancellationToken token)
        {
            var employeeProfile = await Database.EmployeeProfiles.GetAll().SingleOrDefaultAsync(e => e.Id == employeeId, token);

            if (employeeProfile == null)
            {
                var result = new Result<EmployeeProfileDTO>("Employee profile not found");
                Logger.LogError(result.ErrorMessage);
                return result;
            }

            Database.EmployeeProfiles.Delete(employeeProfile);
            await Database.SaveAsync(token);

            Logger.LogInformation($"Employee profile from user id {employeeId} was deleted");
            return new Result<EmployeeProfileDTO>(Mapper.Map<EmployeeProfileDTO>(employeeProfile));
        }
    }
}
