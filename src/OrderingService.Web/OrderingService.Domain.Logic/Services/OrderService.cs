using System;
using System.Linq;
using System.Threading;
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
        private readonly IRepository<ServiceOrder> _serviceOrders;
        private readonly IRepository<EmployeeProfile> _employees;
        private readonly IRepository<User> _users;

        public OrderService(IRepository<ServiceOrder> serviceOrders, IRepository<EmployeeProfile> employees, 
            IRepository<User> users, IMapper mapper, ISaveProvider saveProvider)
            :base(mapper, saveProvider)
        {
            _serviceOrders = serviceOrders;
            _employees = employees;
            _users = users;
        }
        public async Task<OrderDTO> CreateAsync(OrderDTO orderDto, CancellationToken token)
        {
            var order = _mapper.Map<ServiceOrder>(orderDto);
            var orderEmployee = await _employees.GetAll().SingleOrDefaultAsync(x => x.Id == order.EmployeeId, token);
            if (orderEmployee == null)
                throw new LogicException($"Employee with id {orderDto.EmployeeId} not found");

            var orderClient = await _users.GetAll().SingleOrDefaultAsync(x => x.Id == order.ClientId, token);
            if (orderClient == null)
                throw new LogicException($"Client with id {orderDto.ClientId} not found");

            _serviceOrders.Create(order);
            await _saveProvider.SaveAsync(token);

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> TakeOrderAsync(int id, CancellationToken token)
        {
            var order = await GetOrderByIdOrThrow(id, token);

            if(order.Status != OrderStatus.WaitingForEmplpoyee)
                throw new LogicException("Order have already taken/declined");
            order.Status = OrderStatus.InProgress;

            await _saveProvider.SaveAsync(token);
            
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> DeclineOrderAsync(int id, CancellationToken token)
        {
            var order = await GetOrderByIdOrThrow(id, token);

            if(order.Status != OrderStatus.WaitingForEmplpoyee)
                throw new LogicException("Order have already taken/declined");
            order.Status = OrderStatus.Declined;

            await _saveProvider.SaveAsync(token);
            
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> ConfirmOrderCompletion(int id, CancellationToken token)
        {
            var order = await GetOrderByIdOrThrow(id, token);

            if(order.Status != OrderStatus.InProgress)
                throw new LogicException($"Can't confirm order on that stage ({order.Status})");
            order.Status = OrderStatus.Done;

            await _saveProvider.SaveAsync(token);
            
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> DeleteAsync(int id, CancellationToken token)
        {
            var order = await GetOrderByIdOrThrow(id, token);

            await _saveProvider.SaveAsync(token);

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<IPagedResult<OrderDTO>> GetPagedEmployeeOrdersAsync(Guid employeeId, int pageSize,
            int pageNumber, CancellationToken token)
        {
            var query = _serviceOrders.GetAll()
                .Where(x => x.EmployeeId == employeeId);

            int total = query.Count();
            query = query.Paged(pageSize, pageNumber).OrderBy(x => x.Date);

            return new PagedResult<OrderDTO>(
                await query.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToListAsync(token), total, pageSize,
                pageNumber);
        }

        public async Task<IPagedResult<OrderDTO>> GetPagedOrdersByUserAsync(Guid userId, int pageSize,
            int pageNumber, CancellationToken token)
        {
            var query = _serviceOrders.GetAll()
                .Where(x => x.ClientId == userId);

            int total = query.Count();
            query = query.Paged(pageSize, pageNumber).OrderBy(x => x.Status);

            return new PagedResult<OrderDTO>(
                await query.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToListAsync(token), total, pageSize,
                pageNumber);
        }

        private async Task<ServiceOrder> GetOrderByIdOrThrow(int id, CancellationToken token)
        {
            var order = await _serviceOrders.GetAll().SingleOrDefaultAsync(x => x.Id == id, token);
            if (order == null)
                throw new LogicNotFoundException($"Order with id {id} not found");
            return order;
        }
    }
}
