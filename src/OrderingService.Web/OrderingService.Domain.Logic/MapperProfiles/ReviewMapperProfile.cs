using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.MapperProfiles
{
    public class ReviewMapperProfile : Profile
    {
        public ReviewMapperProfile()
        {
            CreateMap<ReviewDTO, Review>(MemberList.Source)
                .ReverseMap();
        }
    }
}
