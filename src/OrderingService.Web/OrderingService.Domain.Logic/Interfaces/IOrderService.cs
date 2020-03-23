using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IOrderService : IDisposable
    {
        IResponse<OrderDTO> Create(OrderDTO orderDto);
        IResponse<OrderDTO> Close(int orderDto);
        IResponse<OrderDTO> Delete(int orderDto);

        IResponse<IEnumerable<OrderDTO>> GetEmployeeOrders(Guid employeeId);
    }
}
