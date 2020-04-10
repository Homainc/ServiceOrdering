using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Common;
using OrderingService.Common.Interfaces;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ServiceOrderRepository : AbstractRepository<ServiceOrder>, IServiceOrderRepository
    {
        public ServiceOrderRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) 
            : base(db, httpContextAccessor) { }

        public override IQueryable<ServiceOrder> GetAll() => 
            Db.ServiceOrders.AsQueryable();
        public async Task<ServiceOrder> GetByIdOrDefaultAsync(int id) => 
            await Db.ServiceOrders.SingleOrDefaultAsync(x => x.Id == id, Token);

        public async Task<IPagedResult<ServiceOrder>> GetPagedFilteredOrdersAsync(Expression<Func<ServiceOrder, bool>> filter, int pageSize, int pageNumber)
        {
            var query =
                from o in Db.ServiceOrders.Where(filter)
                orderby o.Status
                select new ServiceOrder
                {
                    Id = o.Id,
                    Status = o.Status,
                    ServiceDetails = o.ServiceDetails,
                    BriefTask = o.BriefTask,
                    Price = o.Price,
                    Date = o.Date,
                    Address = o.Address,
                    ContactPhone = o.ContactPhone,
                    EmployeeId = o.EmployeeId,
                    ClientId = o.ClientId
                };

            var total = query.Count();

            return new PagedResult<ServiceOrder>(await query.Paged(pageSize, pageNumber).ToListAsync(Token), total,
                pageSize, pageNumber);
        }
    }
}
