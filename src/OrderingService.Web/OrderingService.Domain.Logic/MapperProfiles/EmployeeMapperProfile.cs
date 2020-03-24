using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.MapperProfiles
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<EmployeeProfile, EmployeeProfileDTO>()
                .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => src.ServiceType.Name));

            CreateMap<EmployeeProfileDTO, EmployeeProfile>()
                .ForPath(dest => dest.ServiceType.Name, opt => opt.MapFrom(src => src.ServiceType));
        }
    }
}
