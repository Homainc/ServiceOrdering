using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.MapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserCreateDto, User>()
                .ForPath(dest => dest.Role.Name, opt => opt.MapFrom(src => src.Role));
            CreateMap<UserDto, User>()
                .ForPath(dest => dest.Role.Name, opt => opt.MapFrom(src => src.Role));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<User, UserAuthDto>().IncludeBase<User, UserDto>();
        }
    }
}
