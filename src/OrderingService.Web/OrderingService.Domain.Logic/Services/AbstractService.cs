using AutoMapper;
using OrderingService.Data.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class AbstractService
    {
        protected readonly IMapper _mapper;
        protected readonly ISaveProvider _saveProvider;
        public AbstractService(IMapper mapper, ISaveProvider saveProvider)
        {
            _mapper = mapper;
            _saveProvider = saveProvider;
        }
    }
}