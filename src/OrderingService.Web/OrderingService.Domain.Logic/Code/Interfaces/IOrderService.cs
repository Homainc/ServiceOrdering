using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateAsync(OrderDTO orderDto, CancellationToken token);
        Task<OrderDTO> TakeOrderAsync(int orderDto, CancellationToken token);
        Task<OrderDTO> DeclineOrderAsync(int orderDto, CancellationToken token);
        Task<OrderDTO> ConfirmOrderCompletion(int orderDto, CancellationToken token);
        Task<OrderDTO> DeleteAsync(int orderDto, CancellationToken token);

        Task<IPagedResult<OrderDTO>> GetPagedEmployeeOrdersAsync(Guid employeeId, int pageSize, int pageNumber, CancellationToken token);
    }
}
