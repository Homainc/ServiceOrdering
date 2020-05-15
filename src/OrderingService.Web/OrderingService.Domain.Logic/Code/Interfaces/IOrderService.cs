using System;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateAsync(OrderCreateDto orderDto);
        Task<OrderDto> TakeOrderAsync(int orderDto);
        Task<OrderDto> DeclineOrderAsync(int orderDto);
        Task<OrderDto> ConfirmOrderCompletion(int orderDto);
        Task<OrderDto> DeleteAsync(int orderDto);
        Task<IPagedResult<OrderDto>> GetPagedOrdersByEmployeeAsync(Guid employeeId, int pageSize, int pageNumber);
        Task<IPagedResult<OrderDto>> GetPagedOrdersByUserAsync(Guid userId, int pageSize, int pageNumber);
    }
}
