using System;
using System.Threading.Tasks;
using AutoMapper;
using OrderingService.Common.Interfaces;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code;
using OrderingService.Domain.Logic.Code.Exceptions;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class EmployeeService : AbstractService, IEmployeeService
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IUserRepository userRepository,
            IServiceTypeRepository serviceTypeRepository, ISaveProvider saveProvider, IMapper mapper)
            : base(mapper, saveProvider)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
        }

        public async Task<IPagedResult<EmployeeProfileDto>> GetPagedEmployeesAsync(int pageSize, int pageNumber,
            string searchString, decimal? maxServiceCost, int? minAverageRate, int? serviceTypeId) =>
            (await _employeeRepository.GetPagedEmployeesAsync(pageSize, pageNumber, searchString, maxServiceCost, minAverageRate, serviceTypeId))
            .ToPagedDto<EmployeeProfileDto, EmployeeProfile>(Mapper);

        public async Task<EmployeeProfileDto> CreateEmployeeAsync(EmployeeProfileCreateDto employeeProfileDto)
        {
            if(!await _userRepository.AnyUserAsync(x => x.Id == employeeProfileDto.UserId))
                throw new NotFoundLogicException($"User with id {employeeProfileDto.UserId} not found!", nameof(employeeProfileDto.UserId));
            if(await _employeeRepository.AnyEmployeeAsync(x => x.UserId == employeeProfileDto.UserId))
                throw new LogicException($"Employee profile for user with id {employeeProfileDto.UserId} has been already created!");

            var employeeProfile = Mapper.Map<EmployeeProfile>(employeeProfileDto);
            employeeProfile.ServiceType = await _serviceTypeRepository.GetByNameOrCreateNewAsync(employeeProfileDto.ServiceType);

            _employeeRepository.Create(employeeProfile);
            await SaveProvider.SaveAsync();

            return Mapper.Map<EmployeeProfileDto>(employeeProfile);
        }

        public async Task<EmployeeProfileDto> UpdateEmployeeAsync(EmployeeProfileUpdateDto employeeProfileDto)
        {
            var employeeProfile = await GetEmployeeByIdOrThrowAsync(employeeProfileDto.Id);

            Mapper.Map(employeeProfileDto, employeeProfile);
            employeeProfile.ServiceType = await _serviceTypeRepository.GetByNameOrCreateNewAsync(employeeProfileDto.ServiceType);
            employeeProfile.ServiceTypeId = employeeProfile.ServiceType.Id;
            
            _employeeRepository.Update(employeeProfile);
            await SaveProvider.SaveAsync();

            return Mapper.Map<EmployeeProfileDto>(employeeProfile);
        }

        public async Task<EmployeeProfileDto> DeleteEmployeeAsync(Guid id)
        {
            var employeeProfile = await GetEmployeeByIdOrThrowAsync(id);

            _employeeRepository.Delete(employeeProfile);
            await SaveProvider.SaveAsync();

            return Mapper.Map<EmployeeProfileDto>(employeeProfile);
        }

        public async Task<Guid> GetUserIdByEmployeeIdAsync(Guid employeeId) => 
            await _employeeRepository.GetUserIdByEmployeeIdAsync(employeeId);

        public async Task<EmployeeProfileDto> GetEmployeeByIdAsync(Guid id) =>
            Mapper.Map<EmployeeProfileDto>(await GetEmployeeByIdOrThrowAsync(id));

        private async Task<EmployeeProfile> GetEmployeeByIdOrThrowAsync(Guid id) =>
            await _employeeRepository.GetEagerByIdOrDefaultAsync(id) ??
            throw new NotFoundLogicException($"Employee profile with id {id} not found!", nameof(id));
    }
}