using System;
using System.Threading.Tasks;
using AutoMapper;
using OrderingService.Common;
using OrderingService.Common.Interfaces;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code;
using OrderingService.Domain.Logic.Code.Exceptions;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class OrderService : AbstractService, IOrderService
    {
        private readonly IServiceOrderRepository _serviceOrders;
        
        public OrderService(IServiceOrderRepository serviceOrders, IMapper mapper, ISaveProvider saveProvider)
            :base(mapper, saveProvider)
        {
            _serviceOrders = serviceOrders;
        }
        public async Task<OrderDTO> CreateAsync(OrderDTO orderDto)
        {
            var order = _mapper.Map<ServiceOrder>(orderDto);

            _serviceOrders.Create(order);
            await _saveProvider.SaveAsync();

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> TakeOrderAsync(int id) =>
            await SetOrderStatusAsync(id, OrderStatus.InProgress);

        public async Task<OrderDTO> DeclineOrderAsync(int id) =>
            await SetOrderStatusAsync(id, OrderStatus.Declined);

        public async Task<OrderDTO> ConfirmOrderCompletion(int id) =>
            await SetOrderStatusAsync(id, OrderStatus.Done);

        public async Task<OrderDTO> DeleteAsync(int id)
        {
            var order = await _serviceOrders.SingleByIdAsync(id);

            _serviceOrders.Delete(order);
            await _saveProvider.SaveAsync();

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<IPagedResult<OrderDTO>> GetPagedOrdersByEmployeeAsync(Guid employeeId, int pageSize,
            int pageNumber) =>
            (await _serviceOrders.GetPagedFilteredOrdersAsync(x => x.EmployeeId == employeeId, pageSize, pageNumber))
            .ToPagedDto<OrderDTO, ServiceOrder>(_mapper);

        public async Task<IPagedResult<OrderDTO>> GetPagedOrdersByUserAsync(Guid userId, int pageSize,
            int pageNumber) =>
            (await _serviceOrders.GetPagedFilteredOrdersAsync(x => x.ClientId == userId, pageSize, pageNumber))
            .ToPagedDto<OrderDTO, ServiceOrder>(_mapper);

        private async Task<OrderDTO> SetOrderStatusAsync(int id, OrderStatus status)
        {
            var order = await _serviceOrders.SingleByIdAsync(id);
            switch(status){
                case OrderStatus.Declined when order.Status == OrderStatus.WaitingForEmplpoyee:
                    break;
                case OrderStatus.InProgress when order.Status == OrderStatus.WaitingForEmplpoyee:
                    break;
                case OrderStatus.Done when order.Status == OrderStatus.InProgress:
                    break;
                default:
                    throw new LogicException($"Unable to change status from {order.Status} to {status}");
            }

            order.Status = status;
            await _saveProvider.SaveAsync();

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<bool> AnyOrderByIdAsync(int id) =>
            await _serviceOrders.AnyOrderById(id);
    }
}
