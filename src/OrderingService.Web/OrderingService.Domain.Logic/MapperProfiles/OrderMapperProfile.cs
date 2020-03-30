using System;
using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.MapperProfiles
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<OrderDTO, ServiceOrder>()
                .ReverseMap();
            CreateMap<string, Guid>().ConvertUsing(s => Guid.Parse(s));
            CreateMap<Guid, string>().ConvertUsing(g => g.ToString());
        }
    }
}
