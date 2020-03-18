using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public IOperationResult Create(OrderDTO orderDto)
        {
            var orderEmployee = Database.EmployeeProfiles.GetAll().SingleOrDefault(x => x.Id == orderDto.EmployeeId);
            if (orderEmployee == null)
            {
                var result = OperationResult.Error($"Employee with id {orderDto.EmployeeId} not found");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            var orderClient = Database.UserProfiles.GetAll().SingleOrDefault(x => x.Id == orderDto.ClientId);
            if (orderClient == null)
            {
                var result = OperationResult.Error($"Client with id {orderDto.ClientId} not found");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            orderDto.Date = DateTime.Now;
            var order = Mapper.Map<ServiceOrder>(orderDto);
            Database.ServiceOrders.Create(order);
            Database.Save();

            Logger.LogInformation($"Order (Price: {order.Price}, Description: {order.Description}) was created");
            return OperationResult.Success();
        }

        public IOperationResult Close(int id)
        {
            var order = Database.ServiceOrders.GetAll().SingleOrDefault(x => x.Id == id);
            if (order == null)
            {
                var result = OperationResult.Error($"Order with id {id} not found");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            order.IsClosed = true;
            Database.ServiceOrders.Update(order);
            Database.Save();

            Logger.LogInformation($"Order with id {id} was closed");
            return OperationResult.Success();
        }

        public IOperationResult Delete(int id)
        {
            var order = Database.ServiceOrders.GetAll().SingleOrDefault(x => x.Id == id);
            if (order == null)
            {
                var result = OperationResult.Error($"Order with id {id} not found");
                Logger.LogInformation(result.ErrorMessage);
                return result;
            }

            Database.ServiceOrders.Delete(order);
            Database.Save();

            Logger.LogInformation($"Order with id {id} was deleted");
            return OperationResult.Success();
        }

        public IEnumerable<OrderDTO> GetEmployeeOrders(string employeeId) => Database.ServiceOrders.GetAll()
            .Where(x => x.EmployeeId == employeeId && !x.IsClosed).ProjectTo<OrderDTO>(Mapper.ConfigurationProvider);
    }
}
