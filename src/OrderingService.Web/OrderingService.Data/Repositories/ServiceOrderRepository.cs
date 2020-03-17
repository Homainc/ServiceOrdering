using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ServiceOrderRepository : IRepository<ServiceOrder>
    {
        private readonly ApplicationContext _db;
        public ServiceOrderRepository(ApplicationContext appContext)
        {
            _db = appContext;
        }

        public IQueryable<ServiceOrder> GetAll() => _db.ServiceOrders.AsQueryable();

        public void Create(ServiceOrder entity) => _db.Add(entity);

        public void Update(ServiceOrder entity) => _db.Entry(entity).State = EntityState.Modified;

        public void Delete(int id)
        {
            var serviceOrder = _db.ServiceOrders.Find(id);
            if (serviceOrder != null)
                _db.ServiceOrders.Remove(serviceOrder);
        }
    }
}
