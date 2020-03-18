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
        public IActionResult GetEmployeeOrders(string id) =>
            OrderService.GetEmployeeOrders(id).ToHttpResponse();

        [HttpPost]
        public IActionResult Create(OrderDTO orderDto) =>
            OrderService.Create(orderDto).ToHttpResponse();

        [HttpPut("{id}")]
        public IActionResult Close(int id) =>
            OrderService.Close(id).ToHttpResponse();

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            OrderService.Delete(id).ToHttpResponse();
    }
}
