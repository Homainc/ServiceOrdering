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
                .IncludeMembers(src => src.UserProfile)
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserProfile.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserProfile.LastName))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserProfile.ImageUrl));
            CreateMap<UserProfile, UserDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.EmployeeProfile, opt=>opt.MapFrom(src => src.User.EmployeeProfile))
                .ReverseMap();
        }
    }
}
