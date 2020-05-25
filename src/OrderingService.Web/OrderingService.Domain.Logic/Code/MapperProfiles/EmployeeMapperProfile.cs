using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.Code.MapperProfiles
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<EmployeeProfile, EmployeeProfileDtoBase>()
                .Include<EmployeeProfile, EmployeeProfileCreateDto>()
                .Include<EmployeeProfile, EmployeeProfileUpdateDto>()
                .Include<EmployeeProfile, EmployeeProfileDto>()
                .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => src.ServiceType.Name));

            CreateMap<EmployeeProfileDtoBase, EmployeeProfile>()
                .Include<EmployeeProfileCreateDto, EmployeeProfile>()
                .Include<EmployeeProfileUpdateDto, EmployeeProfile>()
                .Include<EmployeeProfileDto, EmployeeProfile>()
                .ForPath(dest => dest.ServiceType.Name, opt => opt.MapFrom(src => src.ServiceType));

            CreateMap<EmployeeProfile, EmployeeProfileCreateDto>().ReverseMap();
            CreateMap<EmployeeProfile, EmployeeProfileUpdateDto>().ReverseMap();
            CreateMap<EmployeeProfile, EmployeeProfileDto>().ReverseMap();
        }
    }
}
