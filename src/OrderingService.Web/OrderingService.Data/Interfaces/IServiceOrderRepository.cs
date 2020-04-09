using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IServiceOrderRepository : IRepository<ServiceOrder>
    {
        Task<ServiceOrder> SingleByIdAsync(int id);
        Task<bool> AnyOrderById(int id);
        Task<IPagedResult<ServiceOrder>> GetPagedFilteredOrdersAsync(Expression<Func<ServiceOrder, bool>> filter, int pageSize, int pageNumber);
    }
}
