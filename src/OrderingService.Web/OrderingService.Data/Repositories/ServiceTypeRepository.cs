using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ServiceTypeRepository : IRepository<ServiceType>
    {
        private readonly ApplicationContext _db;
        public ServiceTypeRepository(ApplicationContext appContext)
        {
            _db = appContext;
        }

        public IQueryable<ServiceType> GetAll() => _db.ServiceTypes.AsQueryable();

        public void Create(ServiceType entity) => _db.Add(entity);

        public void Update(ServiceType entity) => _db.Entry(entity).State = EntityState.Modified;

        public void Delete(ServiceType entity)
        {
            var serviceType = _db.ServiceTypes.Find(entity.Id);
            if (serviceType != null)
                _db.ServiceTypes.Remove(serviceType);
        }
    }
}
