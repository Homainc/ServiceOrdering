using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<EmployeeProfile> EmployeeProfiles { get; }
        IRepository<Review> Reviews { get; }
        IRepository<ServiceOrder> ServiceOrders { get; }
        IRepository<ServiceType> ServiceTypes { get; }
        IRepository<UserProfile> UserProfiles { get; }
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        void Save();

    }
}
