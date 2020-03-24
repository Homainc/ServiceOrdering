using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Helpers;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork Database { get; }
        private IMapper Mapper { get; }
        public EmployeeService(IUnitOfWork db, IMapper mapper)
        {
            Database = db;
            Mapper = mapper;
        }

        public void Dispose() => Database.Dispose();

        public async Task<IPagedResult<EmployeeProfileDTO>> GetPagedEmployeesAsync(string serviceName,
            decimal? maxServiceCost, int pageSize, int pageNumber, CancellationToken token)
        {
            var employee = Database.EmployeeProfiles.GetAll();
            if (serviceName != null)
                employee = employee.Where(e => e.ServiceType.Name.Contains(serviceName));
            if (maxServiceCost.HasValue)
                employee = employee.Where(e => e.ServiceCost <= maxServiceCost.Value);

            var total = employee.Count();
            employee = employee.Paged(pageSize, pageNumber);

            return new PagedResult<EmployeeProfileDTO>(
                await employee.ProjectTo<EmployeeProfileDTO>(Mapper.ConfigurationProvider).ToListAsync(token), total,
                pageSize, pageNumber);
        }

        public async Task<IResult<EmployeeProfileDTO>> CreateEmployeeAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token)
        {
            var employeeProfile = await Database.EmployeeProfiles.GetAll().SingleOrDefaultAsync(x => x.Id == employeeProfileDto.Id, token);
            if (employeeProfile != null)
                return new Result<EmployeeProfileDTO>("Employee profile already exist");

            var serviceType = await Database.ServiceTypes.GetAll()
                .SingleOrDefaultAsync(s => s.Name.Equals(employeeProfileDto.ServiceType.ToLower()), token);
            if (serviceType == null)
            {
                serviceType = new ServiceType {Name = employeeProfileDto.ServiceType.ToLower()};
                await Database.ServiceTypes.CreateAsync(serviceType, token);
            }

            employeeProfile = Mapper.Map<EmployeeProfile>(employeeProfileDto);
            employeeProfile.ServiceType = serviceType;

            await Database.EmployeeProfiles.CreateAsync(employeeProfile, token);
            await Database.SaveAsync(token);

            return new Result<EmployeeProfileDTO>(Mapper.Map<EmployeeProfileDTO>(employeeProfile));
        }

        public async Task<IResult<EmployeeProfileDTO>> UpdateEmployeeAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token)
        {
            var employeeProfile = await Database.EmployeeProfiles.GetAll().SingleOrDefaultAsync(x => x.Id == employeeProfileDto.Id, token);
            if (employeeProfile == null)
                return new Result<EmployeeProfileDTO>($"Employee profile with id {employeeProfileDto.Id} not found");
          

            var serviceType = await Database.ServiceTypes.GetAll()
                .SingleOrDefaultAsync(s => s.Name.Equals(employeeProfileDto.ServiceType.ToLower()), token);
            if (serviceType == null)
            {
                serviceType = new ServiceType {Name = employeeProfileDto.ServiceType.ToLower()};
                await Database.ServiceTypes.CreateAsync(serviceType, token);
            }

            employeeProfile = Mapper.Map<EmployeeProfile>(employeeProfileDto);
            employeeProfile.ServiceType = serviceType;

            Database.EmployeeProfiles.Update(employeeProfile);
            await Database.SaveAsync(token);

            return new Result<EmployeeProfileDTO>(Mapper.Map<EmployeeProfileDTO>(employeeProfile));
        }

        public async Task<IResult<EmployeeProfileDTO>> DeleteEmployeeAsync(Guid employeeId, CancellationToken token)
        {
            var employeeProfile = await Database.EmployeeProfiles.GetAll().SingleOrDefaultAsync(e => e.Id == employeeId, token);

            if (employeeProfile == null)
                return new Result<EmployeeProfileDTO>("Employee profile not found");

            Database.EmployeeProfiles.Delete(employeeProfile);
            await Database.SaveAsync(token);

            return new Result<EmployeeProfileDTO>(Mapper.Map<EmployeeProfileDTO>(employeeProfile));
        }
    }
}
