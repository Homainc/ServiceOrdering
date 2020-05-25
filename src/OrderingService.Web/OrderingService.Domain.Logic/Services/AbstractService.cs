using AutoMapper;
using OrderingService.Data.Code.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class AbstractService
    {
        protected readonly IMapper Mapper;
        protected readonly ISaveProvider SaveProvider;
        protected AbstractService(IMapper mapper, ISaveProvider saveProvider)
        {
            Mapper = mapper;
            SaveProvider = saveProvider;
        }
    }
}