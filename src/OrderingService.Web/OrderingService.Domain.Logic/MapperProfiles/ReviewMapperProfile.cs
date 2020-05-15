using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.MapperProfiles
{
    public class ReviewMapperProfile : Profile
    {
        public ReviewMapperProfile()
        {
            CreateMap<ReviewCreateDto, Review>()
                .ReverseMap();
            CreateMap<ReviewDto, Review>()
                .IncludeBase<ReviewCreateDto, Review>()
                .ReverseMap();
        }
    }
}
