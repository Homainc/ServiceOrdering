using System;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateAsync(OrderDTO orderDto);
        Task<OrderDTO> TakeOrderAsync(int orderDto);
        Task<OrderDTO> DeclineOrderAsync(int orderDto);
        Task<OrderDTO> ConfirmOrderCompletion(int orderDto);
        Task<OrderDTO> DeleteAsync(int orderDto);
        Task<IPagedResult<OrderDTO>> GetPagedOrdersByEmployeeAsync(Guid employeeId, int pageSize, int pageNumber);
        Task<IPagedResult<OrderDTO>> GetPagedOrdersByUserAsync(Guid userId, int pageSize, int pageNumber);
        Task<bool> AnyOrderByIdAsync(int id);
    }
}
