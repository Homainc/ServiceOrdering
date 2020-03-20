﻿using System;
using System.Collections.Generic;
using System.Text;
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
        }
    }
}