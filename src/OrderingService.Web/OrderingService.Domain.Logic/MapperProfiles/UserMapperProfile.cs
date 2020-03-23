using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.MapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDTO, User>(MemberList.Source)
                .ForPath(dest => dest.Role.Name, opt => opt.MapFrom(src => src.Role))
                .ReverseMap()
                .IncludeMembers(src => src.EmployeeProfile)
                .ForPath(dest => dest.EmployeeProfile.ServiceType,
                    opt => opt.MapFrom(src => src.EmployeeProfile.ServiceType))
                .ForPath(dest => dest.EmployeeProfile.ServiceCost,
                    opt => opt.MapFrom(src => src.EmployeeProfile.ServiceCost))
                .ForPath(dest => dest.EmployeeProfile.Description,
                    opt => opt.MapFrom(src => src.EmployeeProfile.Description))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<EmployeeProfile, UserDTO>();
        }
    }
}
