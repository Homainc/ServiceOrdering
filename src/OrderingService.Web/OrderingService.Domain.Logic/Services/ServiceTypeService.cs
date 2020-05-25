using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OrderingService.Data.Code.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code.Exceptions;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class ServiceTypeService : AbstractService, IServiceTypeService
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;

        public ServiceTypeService(IServiceTypeRepository serviceTypeRepository, IMapper mapper,
            ISaveProvider saveProvider) : base(mapper, saveProvider)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }

        public async Task<IEnumerable<ServiceTypeDto>> GetAllOrderedByProfilesCount() =>
            Mapper.Map<IEnumerable<ServiceTypeDto>>(await _serviceTypeRepository.GetAllOrderedByProfilesCount());

        public async Task<ServiceTypeDto> CreateAsync(ServiceTypeCreateDto item)
        {
            if(await _serviceTypeRepository.AnyServiceAsync(x => x.Name == item.Name))
                throw new FieldLogicException($"Service type with name {item.Name} already exists!", nameof(item.Name));

            var service = Mapper.Map<ServiceType>(item);
            
            _serviceTypeRepository.Create(service);
            await SaveProvider.SaveAsync();

            return Mapper.Map<ServiceTypeDto>(service);
        }

        public async Task<ServiceTypeDto> UpdateAsync(ServiceTypeDto item)
        {
            if (await _serviceTypeRepository.AnyServiceAsync(x => x.Name == item.Name))
                throw new FieldLogicException($"Service type with name {item.Name} already exists!", nameof(item.Name));

            var service = await GetByIdOrThrowAsync(item.Id);

            Mapper.Map(item, service);

            _serviceTypeRepository.Update(service);
            await SaveProvider.SaveAsync();

            return Mapper.Map<ServiceTypeDto>(service);
        }

        public async Task<ServiceTypeDto> DeleteAsync(int id)
        {
            var service = await GetByIdOrThrowAsync(id);
            
            _serviceTypeRepository.Delete(service);
            await SaveProvider.SaveAsync();

            return Mapper.Map<ServiceTypeDto>(service);
        }

        private async Task<ServiceType> GetByIdOrThrowAsync(int id)
            => await _serviceTypeRepository.GetByIdOrDefaultAsync(id) ??
               throw new NotFoundLogicException($"Service type with id {id} not found!", nameof(id));
    }
}
