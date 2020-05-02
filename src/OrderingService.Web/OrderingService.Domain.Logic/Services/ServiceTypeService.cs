using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class ServiceTypeService : AbstractService, IServiceTypeService
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        
        public ServiceTypeService(IServiceTypeRepository serviceTypeRepository, IMapper mapper, ISaveProvider saveProvider) : base(mapper, saveProvider)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }

        public async Task<IEnumerable<ServiceTypeDTO>> GetAllOrderedByProfilesCount() =>
            _mapper.Map<IEnumerable<ServiceTypeDTO>>(await _serviceTypeRepository.GetAllOrderedByProfilesCount());
    }
}
