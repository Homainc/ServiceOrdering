using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.Code.MapperProfiles
{
    public class ServiceTypeProfile : Profile
    {
        public ServiceTypeProfile()
        {
            CreateMap<ServiceType, ServiceTypeCreateDto>()
                .Include<ServiceType, ServiceTypeDto>()
                .ReverseMap();

            CreateMap<ServiceType, ServiceTypeDto>()
                .ReverseMap();
        }
    }
}
