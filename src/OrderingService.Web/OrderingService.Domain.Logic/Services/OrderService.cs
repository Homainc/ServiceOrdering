using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork Database { get; }
        private ILogger<OrderService> Logger { get; }
        private IMapper Mapper { get; }
        public OrderService(IUnitOfWork db, ILogger<OrderService> logger, IMapper mapper)
        {
            Database = db;
            Logger = logger;
            Mapper = mapper;
        }

        public void Dispose() => Database.Dispose();

        public async Task<IResponse<OrderDTO>> CreateAsync(OrderDTO orderDto, CancellationToken token)
        {
            var orderEmployee = await Database.EmployeeProfiles.GetAll().SingleOrDefaultAsync(x => x.Id == orderDto.EmployeeId, token);
            if (orderEmployee == null)
            {
                var result = Response<OrderDTO>.ValidationError($"Employee with id {orderDto.EmployeeId} not found");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            var orderClient = await Database.Users.GetAll().SingleOrDefaultAsync(x => x.Id == orderDto.ClientId, token);
            if (orderClient == null)
            {
                var result = Response<OrderDTO>.ValidationError($"Client with id {orderDto.ClientId} not found");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            orderDto.Date = DateTime.Now;
            var order = Mapper.Map<ServiceOrder>(orderDto);
            await Database.ServiceOrders.CreateAsync(order, token);
            await Database.SaveAsync(token);

            Logger.LogInformation($"Order (Price: {order.Price}, Description: {order.Description}) was created");
            return Response<OrderDTO>.Success(Mapper.Map<OrderDTO>(order));
        }

        public async Task<IResponse<OrderDTO>> CloseAsync(int id, CancellationToken token)
        {
            var order = await Database.ServiceOrders.GetAll().SingleOrDefaultAsync(x => x.Id == id, token);
            if (order == null)
            {
                var result = Response<OrderDTO>.NotFound($"Order with id {id} not found");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            order.IsClosed = true;
            Database.ServiceOrders.Update(order);
            await Database.SaveAsync(token);

            Logger.LogInformation($"Order with id {id} was closed");
            return Response<OrderDTO>.Success(Mapper.Map<OrderDTO>(order));
        }

        public async Task<IResponse<OrderDTO>> DeleteAsync(int id, CancellationToken token)
        {
            var order = await Database.ServiceOrders.GetAll().SingleOrDefaultAsync(x => x.Id == id, token);
            if (order == null)
            {
                var result = Response<OrderDTO>.NotFound($"Order with id {id} not found");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            Database.ServiceOrders.Delete(order);
            await Database.SaveAsync(token);

            Logger.LogInformation($"Order with id {id} was deleted");
            return Response<OrderDTO>.Success(Mapper.Map<OrderDTO>(order));
        }

        public async Task<IResponse<IEnumerable<OrderDTO>>>
            GetEmployeeOrdersAsync(Guid employeeId, CancellationToken token) =>
            Response<IEnumerable<OrderDTO>>.Success(await Database.ServiceOrders.GetAll()
                .Where(x => x.EmployeeId == employeeId && !x.IsClosed)
                .ProjectTo<OrderDTO>(Mapper.ConfigurationProvider).ToListAsync(token));
    }
}
