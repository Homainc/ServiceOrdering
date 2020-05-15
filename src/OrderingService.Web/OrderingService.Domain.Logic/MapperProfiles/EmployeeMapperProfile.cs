using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.MapperProfiles
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<EmployeeProfile, EmployeeProfileDtoBase>()
                .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => src.ServiceType.Name));

            CreateMap<EmployeeProfileDtoBase, EmployeeProfile>()
                .ForPath(dest => dest.ServiceType.Name, opt => opt.MapFrom(src => src.ServiceType));

            CreateMap<EmployeeProfile, EmployeeProfileCreateDto>()
                .IncludeBase<EmployeeProfile, EmployeeProfileDtoBase>();

            CreateMap<EmployeeProfile, EmployeeProfileUpdateDto>()
                .IncludeBase<EmployeeProfile, EmployeeProfileDtoBase>();

            CreateMap<EmployeeProfile, EmployeeProfileDto>()
                .IncludeBase<EmployeeProfile, EmployeeProfileDtoBase>().ReverseMap();
        }
    }
}
