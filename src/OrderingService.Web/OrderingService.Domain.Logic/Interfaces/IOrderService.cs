using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IOrderService : IDisposable
    {
        IOperationResult Create(OrderDTO orderDto);
        IOperationResult Close(int orderDto);
        IOperationResult Delete(int orderDto);

        IEnumerable<OrderDTO> GetEmployeeOrders(string employeeId);
    }
}
