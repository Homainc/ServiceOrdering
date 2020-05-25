using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Code.Interfaces
{
    public interface IServiceOrderRepository : IRepository<ServiceOrder>
    {
        Task<ServiceOrder> GetByIdOrDefaultAsync(int id);
        Task<IPagedResult<ServiceOrder>> GetPagedFilteredOrdersAsync(Expression<Func<ServiceOrder, bool>> filter, int pageSize, int pageNumber);
    }
}
