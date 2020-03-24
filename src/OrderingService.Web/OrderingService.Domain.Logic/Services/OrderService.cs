using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;
using OrderingService.Domain.Logic.Helpers;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork Database { get; }
        private IMapper Mapper { get; }
        public OrderService(IUnitOfWork db, IMapper mapper)
        {
            Database = db;
            Mapper = mapper;
        }

        public void Dispose() => Database.Dispose();

        public async Task<IResult<OrderDTO>> CreateAsync(OrderDTO orderDto, CancellationToken token)
        {
            var orderEmployee = await Database.EmployeeProfiles.GetAll().SingleOrDefaultAsync(x => x.Id == orderDto.EmployeeId, token);
            if (orderEmployee == null)
                return new Result<OrderDTO>($"Employee with id {orderDto.EmployeeId} not found");

            var orderClient = await Database.Users.GetAll().SingleOrDefaultAsync(x => x.Id == orderDto.ClientId, token);
            if (orderClient == null)
                return new Result<OrderDTO>($"Client with id {orderDto.ClientId} not found");

            orderDto.Date = DateTime.Now;
            var order = Mapper.Map<ServiceOrder>(orderDto);
            await Database.ServiceOrders.CreateAsync(order, token);
            await Database.SaveAsync(token);

            return new Result<OrderDTO>(Mapper.Map<OrderDTO>(order));
        }

        public async Task<IResult<OrderDTO>> CloseAsync(int id, CancellationToken token)
        {
            var order = await Database.ServiceOrders.GetAll().SingleOrDefaultAsync(x => x.Id == id, token);
            if (order == null)
                return new Result<OrderDTO>($"Order with id {id} not found");

            order.IsClosed = true;
            Database.ServiceOrders.Update(order);
            await Database.SaveAsync(token);

            return new Result<OrderDTO>(Mapper.Map<OrderDTO>(order));
        }

        public async Task<IResult<OrderDTO>> DeleteAsync(int id, CancellationToken token)
        {
            var order = await Database.ServiceOrders.GetAll().SingleOrDefaultAsync(x => x.Id == id, token);
            if (order == null)
                return new Result<OrderDTO>($"Order with id {id} not found");

            Database.ServiceOrders.Delete(order);
            await Database.SaveAsync(token);

            return new Result<OrderDTO>(Mapper.Map<OrderDTO>(order));
        }

        public async Task<IPagedResult<OrderDTO>> GetPagedEmployeeOrdersAsync(Guid employeeId, int pageSize,
            int pageNumber, CancellationToken token)
        {
            var query = Database.ServiceOrders.GetAll()
                .Where(x => x.EmployeeId == employeeId && !x.IsClosed);

            int total = query.Count();
            query = query.Paged(pageSize, pageNumber);

            return new PagedResult<OrderDTO>(
                await query.ProjectTo<OrderDTO>(Mapper.ConfigurationProvider).ToListAsync(token), total, pageSize,
                pageNumber);
        }
    }
}
