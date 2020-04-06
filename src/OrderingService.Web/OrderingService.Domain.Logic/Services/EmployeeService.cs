using System;
using System.Linq;
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
        private readonly IEmployeeRepository _employees;
        public EmployeeService(IEmployeeRepository employees,
            IRepository<ServiceType> serviceTypes, ISaveProvider saveProvider, IMapper mapper)
            :base(mapper, saveProvider)
        {
            _serviceTypes = serviceTypes;
            _employees = employees;
        }

        public async Task<IPagedResult<EmployeeProfileDTO>> GetPagedEmployeesAsync(string serviceName,
            decimal? maxServiceCost, int pageSize, int pageNumber)
        {
            // TODO: Move this to repository
            var employee = _employees.GetAll();
            if (serviceName != null)
                employee = employee.Where(e => e.ServiceType.Name.Contains(serviceName));
            if (maxServiceCost.HasValue)
                employee = employee.Where(e => e.ServiceCost <= maxServiceCost.Value);

            var total = employee.Count();
            employee = employee.Paged(pageSize, pageNumber);

            return new PagedResult<EmployeeProfileDTO>(
                await employee.ProjectTo<EmployeeProfileDTO>(_mapper.ConfigurationProvider).ToListAsync(), total,
                pageSize, pageNumber);
        }

        public async Task<EmployeeProfileDTO> CreateEmployeeAsync(EmployeeProfileDTO employeeProfileDto)
        {
            // TODO: Move this check to filter
            if (!(await _employees.AnyEmployeeAsync(x => x.UserId == employeeProfileDto.UserId)))
                throw new LogicException("Employee profile already exist");

            //TODO: Remove EF Core dependency
            var serviceType = await _serviceTypes.GetAll()
                .SingleOrDefaultAsync(s => s.Name.Equals(employeeProfileDto.ServiceType.ToLower()));
            if (serviceType == null)
            {
                serviceType = new ServiceType {Name = employeeProfileDto.ServiceType.ToLower()};
                _serviceTypes.Create(serviceType);
            }

            var employeeProfile = _mapper.Map<EmployeeProfile>(employeeProfileDto);
            employeeProfile.ServiceType = serviceType;

            _employees.Create(employeeProfile);
            await _saveProvider.SaveAsync();

            return _mapper.Map<EmployeeProfileDTO>(employeeProfile);
        }

        public async Task<EmployeeProfileDTO> UpdateEmployeeAsync(EmployeeProfileDTO employeeProfileDto)
        {
            var employeeProfile = await GetEmployeeByIdOrThrow(employeeProfileDto.Id);

            var serviceType = await _serviceTypes.GetAll()
                .SingleOrDefaultAsync(s => s.Name.Equals(employeeProfileDto.ServiceType.ToLower()));
            if (serviceType == null)
            {
                serviceType = new ServiceType {Name = employeeProfileDto.ServiceType.ToLower()};
                _serviceTypes.Create(serviceType);
            }

            _mapper.Map(employeeProfileDto, employeeProfile);
            employeeProfile.ServiceType = serviceType;

            await _saveProvider.SaveAsync();

            return _mapper.Map<EmployeeProfileDTO>(employeeProfile);
        }

        public async Task<EmployeeProfileDTO> DeleteEmployeeAsync(Guid employeeId)
        {
            var employeeProfile = await GetEmployeeByIdOrThrow(employeeId);

            _employees.Delete(employeeProfile);
            await _saveProvider.SaveAsync();

            return _mapper.Map<EmployeeProfileDTO>(employeeProfile);
        }

        public async Task<EmployeeProfileDTO> GetEmployeeByIdAsync(Guid id) => 
            _mapper.Map<EmployeeProfileDTO>(await GetEmployeeByIdOrThrow(id));

        private async Task<EmployeeProfile> GetEmployeeByIdOrThrow(Guid id)
        {
            var employeeProfile = await _employees.EagerSingleOrDefaultAsync(x => x.Id == id);
            // TODO: Move this check to action controller
            if (employeeProfile == null)
                throw new LogicNotFoundException($"Employee profile with id {id} not found");
            return employeeProfile;
        }
    }
}
