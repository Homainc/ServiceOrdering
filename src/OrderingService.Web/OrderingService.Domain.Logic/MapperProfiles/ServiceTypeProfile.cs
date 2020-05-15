using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.MapperProfiles
{
    public class ServiceTypeProfile : Profile
    {
        public ServiceTypeProfile()
        {
            CreateMap<ServiceType, ServiceTypeDto>();
        }
    }
}
