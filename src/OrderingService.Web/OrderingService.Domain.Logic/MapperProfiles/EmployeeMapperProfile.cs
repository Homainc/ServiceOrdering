using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.MapperProfiles
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<EmployeeProfile, EmployeeProfileDTO>()
                .PreserveReferences().MaxDepth(2)
                .IncludeMembers(e => e.User, e => e.ServiceType)
                .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => src.ServiceType.Name));
            CreateMap<ServiceType, EmployeeProfileDTO>();
            CreateMap<User, EmployeeProfileDTO>();
        }
    }
}
