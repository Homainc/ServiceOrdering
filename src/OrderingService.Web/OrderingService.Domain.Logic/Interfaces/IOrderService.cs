using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task<IResponse<OrderDTO>> CreateAsync(OrderDTO orderDto, CancellationToken token);
        Task<IResponse<OrderDTO>> CloseAsync(int orderDto, CancellationToken token);
        Task<IResponse<OrderDTO>> DeleteAsync(int orderDto, CancellationToken token);

        Task<IResponse<IEnumerable<OrderDTO>>> GetEmployeeOrdersAsync(Guid employeeId, CancellationToken token);
    }
}
