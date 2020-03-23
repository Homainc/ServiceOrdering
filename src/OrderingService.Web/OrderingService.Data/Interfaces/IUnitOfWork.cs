﻿using System;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<EmployeeProfile> EmployeeProfiles { get; }
        IRepository<Review> Reviews { get; }
        IRepository<ServiceOrder> ServiceOrders { get; }
        IRepository<ServiceType> ServiceTypes { get; }
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        void Save();

    }
}
