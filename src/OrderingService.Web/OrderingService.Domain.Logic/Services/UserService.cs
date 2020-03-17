using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _db;
        public UserService(IUnitOfWork db) => _db = db;
        public IOperationResult Create(UserDTO userDto)
        {
            // TODO: User registration
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> FilterEmployees(string serviceTypeName = null, decimal? serviceMaxCost = null)
        {
            var employees = _db.EmployeeProfiles.GetAll();
            if (serviceTypeName != null)
                employees = employees.Where(u => u.ServiceType.Name.Contains(serviceTypeName));
            if (serviceMaxCost != null)
                employees = employees.Where(u => u.ServiceCost <= serviceMaxCost);
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserDTO>()
                        .IncludeMembers(u => u.UserProfile, u => u.EmployeeProfile);
                    cfg.CreateMap<UserProfile, UserDTO>();
                    cfg.CreateMap<EmployeeProfile, EmployeeProfileDTO>();
                });
            var mapper = new Mapper(config);
            return mapper.Map<IQueryable<User>, IEnumerable<UserDTO>>(employees.Select(u => u.User));

        }

        public ClaimsIdentity Authenticate(UserDTO userDto)
        {
            // TODO: User authentication
            throw new NotImplementedException();
        }

        public IOperationResult AddEmployeeProfile(UserDTO userDto)
        {
            // TODO: Employee profile adding
            throw new NotImplementedException();
        }

        public IOperationResult DeleteEmployeeProfile(UserDTO userDto)
        {
            // TODO: Employee profile deleting
            throw new NotImplementedException();
        }

        public void Dispose() => _db.Dispose();
    }
}
