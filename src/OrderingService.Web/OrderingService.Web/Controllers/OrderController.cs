using System.Threading;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetEmployeeOrdersAsync(string id, CancellationToken token = default) =>
            await OrderService.GetEmployeeOrdersAsync(new System.Guid(id), token).ToHttpResponseAsync();

        [HttpPost]
        public async Task<IActionResult> CreateAsync(OrderDTO orderDto, CancellationToken token = default) =>
            await OrderService.CreateAsync(orderDto, token).ToHttpResponseAsync();

        [HttpPut("{id}")]
        public async Task<IActionResult> CloseAsync(int id, CancellationToken token = default) =>
            await OrderService.CloseAsync(id, token).ToHttpResponseAsync();

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken token = default) =>
            await OrderService.DeleteAsync(id, token).ToHttpResponseAsync();
    }
}
