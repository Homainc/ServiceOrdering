using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ServiceOrderRepository : AbstractRepository<ServiceOrder>, IServiceOrderRepository
    {
        public ServiceOrderRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public override IQueryable<ServiceOrder> GetAll() => 
            Db.ServiceOrders.AsQueryable();
        public async Task<ServiceOrder> SingleByIdAsync(int id) => 
            await Db.ServiceOrders.SingleAsync(x => x.Id == id, Token);

        public async Task<bool> AnyOrderById(int id) =>
            await Db.ServiceOrders.AnyAsync(x => x.Id == id, Token);
    }
}
