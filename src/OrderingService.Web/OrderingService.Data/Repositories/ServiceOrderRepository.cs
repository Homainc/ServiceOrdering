using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ServiceOrderRepository : AbstractRepository<ServiceOrder>
    {
        public ServiceOrderRepository(ApplicationContext db) : base(db) { }

        public override IQueryable<ServiceOrder> GetAll() => _db.ServiceOrders.AsNoTracking().AsQueryable();
    }
}
