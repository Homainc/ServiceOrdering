using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code;
using OrderingService.Domain.Logic.Code.Exceptions;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Domain.Logic.Helpers;

namespace OrderingService.Domain.Logic.Services
{
    public class EmployeeService : AbstractService, IEmployeeService
    {
        private readonly IRepository<ServiceType> _serviceTypes;
        private readonly IRepository<EmployeeProfile> _employees;
        public EmployeeService(IRepository<EmployeeProfile> employees, 
            IRepository<ServiceType> serviceTypes, ISaveProvider saveProvider, IMapper mapper)
            :base(mapper, saveProvider)
        {
            _serviceTypes = serviceTypes;
            _employees = employees;
        }

        public async Task<IPagedResult<EmployeeProfileDTO>> GetPagedEmployeesAsync(string serviceName,
            decimal? maxServiceCost, int pageSize, int pageNumber, CancellationToken token)
        {
            var employee = _employees.GetAll();
            if (serviceName != null)
                employee = employee.Where(e => e.ServiceType.Name.Contains(serviceName));
            if (maxServiceCost.HasValue)
                employee = employee.Where(e => e.ServiceCost <= maxServiceCost.Value);

            var total = employee.Count();
            employee = employee.Paged(pageSize, pageNumber);

            return new PagedResult<EmployeeProfileDTO>(
                await employee.ProjectTo<EmployeeProfileDTO>(_mapper.ConfigurationProvider).ToListAsync(token), total,
                pageSize, pageNumber);
        }

        public async Task<EmployeeProfileDTO> CreateEmployeeAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token)
        {
            var employeeProfile = await _employees.GetAll().SingleOrDefaultAsync(x => x.UserId == employeeProfileDto.UserId, token);
            if (employeeProfile != null)
                throw new LogicException("Employee profile already exist");

            var serviceType = await _serviceTypes.GetAll()
                .SingleOrDefaultAsync(s => s.Name.Equals(employeeProfileDto.ServiceType.ToLower()), token);
            if (serviceType == null)
            {
                serviceType = new ServiceType {Name = employeeProfileDto.ServiceType.ToLower()};
                _serviceTypes.Create(serviceType);
            }

            employeeProfile = _mapper.Map<EmployeeProfile>(employeeProfileDto);
            employeeProfile.ServiceType = serviceType;

            _employees.Create(employeeProfile);
            await _saveProvider.SaveAsync(token);

            return _mapper.Map<EmployeeProfileDTO>(employeeProfile);
        }

        public async Task<EmployeeProfileDTO> UpdateEmployeeAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token)
        {
            var employeeProfile = await GetEmployeeByIdOrThrow(employeeProfileDto.Id, token);

            var serviceType = await _serviceTypes.GetAll()
                .SingleOrDefaultAsync(s => s.Name.Equals(employeeProfileDto.ServiceType.ToLower()), token);
            if (serviceType == null)
            {
                serviceType = new ServiceType {Name = employeeProfileDto.ServiceType.ToLower()};
                _serviceTypes.Create(serviceType);
            }

            employeeProfile = _mapper.Map<EmployeeProfile>(employeeProfileDto);
            employeeProfile.ServiceType = serviceType;

            await _saveProvider.SaveAsync(token);

            return _mapper.Map<EmployeeProfileDTO>(employeeProfile);
        }

        public async Task<EmployeeProfileDTO> DeleteEmployeeAsync(Guid employeeId, CancellationToken token)
        {
            var employeeProfile = await GetEmployeeByIdOrThrow(employeeId, token);

            _employees.Delete(employeeProfile);
            await _saveProvider.SaveAsync(token);

            return _mapper.Map<EmployeeProfileDTO>(employeeProfile);
        }

        public async Task<EmployeeProfileDTO> GetEmployeeByIdAsync(Guid id, CancellationToken token) => 
            _mapper.Map<EmployeeProfileDTO>(await GetEmployeeByIdOrThrow(id, token));

        private async Task<EmployeeProfile> GetEmployeeByIdOrThrow(Guid id, CancellationToken token)
        {
            var employeeProfile = await _employees.GetAll().SingleOrDefaultAsync(e => e.Id == id, token);
            if (employeeProfile == null)
                throw new LogicException($"Employee profile with id {id} not found");
            return employeeProfile;
        }
    }
}
