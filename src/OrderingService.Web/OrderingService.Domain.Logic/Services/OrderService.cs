using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrderingService.Common;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Code;
using OrderingService.Domain.Logic.Code.Exceptions;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Domain.Logic.Helpers;

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
            int pageNumber)
        {
            var query = _serviceOrders.GetAll()
                .Where(x => x.EmployeeId == employeeId);

            int total = query.Count();
            query = query.Paged(pageSize, pageNumber).OrderBy(x => x.Date);

            // TODO: Remove EF Core dependency
            return new PagedResult<OrderDTO>(
                await query.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToListAsync(), total, pageSize,
                pageNumber);
        }

        public async Task<IPagedResult<OrderDTO>> GetPagedOrdersByUserAsync(Guid userId, int pageSize,
            int pageNumber)
        {
            var query = _serviceOrders.GetAll()
                .Where(x => x.ClientId == userId);

            int total = query.Count();
            query = query.Paged(pageSize, pageNumber).OrderBy(x => x.Status);

            // TODO: Remove EF Core dependency
            return new PagedResult<OrderDTO>(
                await query.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToListAsync(), total, pageSize,
                pageNumber);
        }

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
