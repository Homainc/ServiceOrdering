using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.MapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDTO, User>(MemberList.Source)
                .ForMember(u => u.Id, opt => opt.Condition(u => u.Id != null))
                .ForPath(dest => dest.UserProfile.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.UserProfile.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.UserProfile.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForPath(dest => dest.UserProfile.Id, opt => opt.Ignore())
                .ReverseMap()
                .IncludeMembers(src => src.UserProfile, src => src.EmployeeProfile)
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserProfile.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserProfile.LastName))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserProfile.ImageUrl))
                .ForPath(dest => dest.EmployeeProfile.ServiceType,
                    opt => opt.MapFrom(src => src.EmployeeProfile.ServiceType))
                .ForPath(dest => dest.EmployeeProfile.ServiceCost,
                    opt => opt.MapFrom(src => src.EmployeeProfile.ServiceCost))
                .ForPath(dest => dest.EmployeeProfile.Description,
                    opt => opt.MapFrom(src => src.EmployeeProfile.Description));
            CreateMap<EmployeeProfile, UserDTO>();
            CreateMap<UserProfile, UserDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ReverseMap();
        }
    }
}
