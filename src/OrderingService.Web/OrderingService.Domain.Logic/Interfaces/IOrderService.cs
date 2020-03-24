using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task<IResult<OrderDTO>> CreateAsync(OrderDTO orderDto, CancellationToken token);
        Task<IResult<OrderDTO>> CloseAsync(int orderDto, CancellationToken token);
        Task<IResult<OrderDTO>> DeleteAsync(int orderDto, CancellationToken token);

        Task<IPagedResult<OrderDTO>> GetEmployeeOrdersAsync(Guid employeeId, CancellationToken token);
    }
}
