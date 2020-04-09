using System;
using System.Threading.Tasks;
using AutoMapper;
using OrderingService.Common.Interfaces;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class EmployeeService : AbstractService, IEmployeeService
    {
        private readonly IServiceTypeRepository _serviceTypes;
        private readonly IEmployeeRepository _employees;

        public EmployeeService(IEmployeeRepository employees,
            IServiceTypeRepository serviceTypes, ISaveProvider saveProvider, IMapper mapper)
            : base(mapper, saveProvider)
        {
            _serviceTypes = serviceTypes;
            _employees = employees;
        }

        public async Task<IPagedResult<EmployeeProfileDTO>> GetPagedEmployeesAsync(int pageSize, int pageNumber,
            string serviceName, decimal? maxServiceCost, int? minAverageRate) =>
            (await _employees.GetPagedEmployeesAsync(pageSize, pageNumber, serviceName, maxServiceCost, minAverageRate))
            .ToPagedDto<EmployeeProfileDTO, EmployeeProfile>(_mapper);

        public async Task<EmployeeProfileDTO> CreateEmployeeAsync(EmployeeProfileDTO employeeProfileDto)
        {
            var employeeProfile = _mapper.Map<EmployeeProfile>(employeeProfileDto);
            employeeProfile.ServiceType = await _serviceTypes.GetByNameOrCreateNewAsync(employeeProfileDto.ServiceType);

            _employees.Create(employeeProfile);
            await _saveProvider.SaveAsync();

            return _mapper.Map<EmployeeProfileDTO>(employeeProfile);
        }

        public async Task<EmployeeProfileDTO> UpdateEmployeeAsync(EmployeeProfileDTO employeeProfileDto)
        {
            var employeeProfile = await _employees.EagerSingleAsync(x => x.Id == employeeProfileDto.Id);

            _mapper.Map(employeeProfileDto, employeeProfile);
            employeeProfile.ServiceType = await _serviceTypes.GetByNameOrCreateNewAsync(employeeProfileDto.ServiceType);
            employeeProfile.ServiceTypeId = employeeProfile.ServiceType.Id;
            _employees.Update(employeeProfile);
            
            await _saveProvider.SaveAsync();

            return _mapper.Map<EmployeeProfileDTO>(employeeProfile);
        }

        public async Task<EmployeeProfileDTO> DeleteEmployeeAsync(Guid id)
        {
            var employeeProfile = await _employees.EagerSingleAsync(x => x.Id == id);

            _employees.Delete(employeeProfile);
            await _saveProvider.SaveAsync();

            return _mapper.Map<EmployeeProfileDTO>(employeeProfile);
        }

        public async Task<EmployeeProfileDTO> GetEmployeeByIdAsync(Guid id) =>
            _mapper.Map<EmployeeProfileDTO>(await _employees.EagerSingleAsync(x => x.Id == id));

        public async Task<bool> AnyEmployeeByIdAsync(Guid id) =>
            await _employees.AnyEmployeeAsync(x => x.Id == id);

        public async Task<bool> AnyEmployeeByUserIdAsync(Guid userId) =>
            await _employees.AnyEmployeeAsync(x => x.UserId == userId);
    }
}