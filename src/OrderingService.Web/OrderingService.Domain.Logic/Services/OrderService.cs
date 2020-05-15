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
        private readonly IServiceOrderRepository _serviceOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public OrderService(IServiceOrderRepository serviceOrderRepository, IUserRepository userRepository,
            IEmployeeRepository employeeRepository, IMapper mapper, ISaveProvider saveProvider)
            : base(mapper, saveProvider)
        {
            _serviceOrderRepository = serviceOrderRepository;
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<OrderDto> CreateAsync(OrderCreateDto orderDto)
        {
            if (!await _employeeRepository.AnyEmployeeAsync(x => x.Id == orderDto.EmployeeId))
                throw new NotFoundLogicException($"Employee with id {orderDto.EmployeeId} not found!", nameof(orderDto.EmployeeId));
            if (!await _userRepository.AnyUserAsync(x => x.Id == orderDto.ClientId))
                throw new NotFoundLogicException($"Client with id {orderDto.ClientId} not found!", nameof(orderDto.ClientId));

            var order = Mapper.Map<ServiceOrder>(orderDto);
            order.Date = DateTime.Now;

            _serviceOrderRepository.Create(order);
            await SaveProvider.SaveAsync();

            return Mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> TakeOrderAsync(int id) =>
            await SetOrderStatusAsync(id, OrderStatus.InProgress);

        public async Task<OrderDto> DeclineOrderAsync(int id) =>
            await SetOrderStatusAsync(id, OrderStatus.Declined);

        public async Task<OrderDto> ConfirmOrderCompletion(int id) =>
            await SetOrderStatusAsync(id, OrderStatus.Done);

        public async Task<OrderDto> DeleteAsync(int id)
        {
            var order = await GetOrderByIdOrThrowAsync(id);

            _serviceOrderRepository.Delete(order);
            await SaveProvider.SaveAsync();

            return Mapper.Map<OrderDto>(order);
        }

        public async Task<IPagedResult<OrderDto>> GetPagedOrdersByEmployeeAsync(Guid employeeId, int pageSize,
            int pageNumber) =>
            (await _serviceOrderRepository.GetPagedFilteredOrdersAsync(x => x.EmployeeId == employeeId, pageSize, pageNumber))
            .ToPagedDto<OrderDto, ServiceOrder>(Mapper);

        public async Task<IPagedResult<OrderDto>> GetPagedOrdersByUserAsync(Guid userId, int pageSize,
            int pageNumber) =>
            (await _serviceOrderRepository.GetPagedFilteredOrdersAsync(x => x.ClientId == userId, pageSize, pageNumber))
            .ToPagedDto<OrderDto, ServiceOrder>(Mapper);

        private async Task<OrderDto> SetOrderStatusAsync(int id, OrderStatus status)
        {
            var order = await GetOrderByIdOrThrowAsync(id);
            switch(status){
                case OrderStatus.Declined when order.Status == OrderStatus.WaitingForEmployee:
                    break;
                case OrderStatus.InProgress when order.Status == OrderStatus.WaitingForEmployee:
                    break;
                case OrderStatus.Done when order.Status == OrderStatus.InProgress:
                    break;
                default:
                    throw new LogicException($"Unable to change status from {order.Status} to {status}");
            }

            order.Status = status;
            await SaveProvider.SaveAsync();

            return Mapper.Map<OrderDto>(order);
        }

        private async Task<ServiceOrder> GetOrderByIdOrThrowAsync(int id) =>
            await _serviceOrderRepository.GetByIdOrDefaultAsync(id) ??
            throw new NotFoundLogicException($"Service order with id {id} not found!", nameof(id));
    }
}
