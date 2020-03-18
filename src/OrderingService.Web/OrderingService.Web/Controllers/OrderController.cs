using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderService OrderService { get; }
        public OrderController(IOrderService orderService)
        {
            OrderService = orderService;
        }

        [HttpGet("employee/{id}")]
        public IEnumerable<OrderDTO> GetEmployeeOrders(string id) =>
            OrderService.GetEmployeeOrders(id);

        [HttpPost]
        public IOperationResult Create(OrderDTO orderDto) =>
            OrderService.Create(orderDto);

        [HttpPut("{id}")]
        public IOperationResult Close(int id) =>
            OrderService.Close(id);

        [HttpDelete("{id}")]
        public IOperationResult Delete(int id) =>
            OrderService.Delete(id);
    }
}
