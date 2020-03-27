using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task<OrderDTO> CreateAsync(OrderDTO orderDto, CancellationToken token);
        Task<OrderDTO> CloseAsync(int orderDto, CancellationToken token);
        Task<OrderDTO> DeleteAsync(int orderDto, CancellationToken token);

        Task<IPagedResult<OrderDTO>> GetPagedEmployeeOrdersAsync(Guid employeeId, int pageSize, int pageNumber, CancellationToken token);
    }
}
