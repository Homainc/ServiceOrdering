using System;
using Microsoft.AspNetCore.Identity;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ApplicationUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _db;
        private IRepository<EmployeeProfile> _employeeProfileRepository;
        private IRepository<Review> _reviewRepository;
        private IRepository<ServiceOrder> _serviceOrderRepository;
        private IRepository<ServiceType> _serviceTypeRepository;
        private IRepository<UserProfile> _userProfileRepository;

        public IRepository<EmployeeProfile> EmployeeProfiles => _employeeProfileRepository ??= new EmployeeProfileRepository(_db);
        public IRepository<Review> Reviews => _reviewRepository ??= new ReviewRepository(_db);
        public IRepository<ServiceOrder> ServiceOrders => _serviceOrderRepository ??= new ServiceOrderRepository(_db);
        public IRepository<ServiceType> ServiceTypes => _serviceTypeRepository ??= new ServiceTypeRepository(_db);
        public IRepository<UserProfile> UserProfiles => _userProfileRepository ??= new UserProfileRepository(_db);

        public ApplicationUnitOfWork(ApplicationContext db)
        {
            _db = db;
        }

        public void Save() => _db.SaveChanges();

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
