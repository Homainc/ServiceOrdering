using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ServiceOrderRepository : AbstractRepository<ServiceOrder>
    {
        public ServiceOrderRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public override IQueryable<ServiceOrder> GetAll() => Db.ServiceOrders.AsQueryable();
    }
}
