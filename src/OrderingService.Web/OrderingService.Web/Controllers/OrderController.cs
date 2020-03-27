using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderService OrderService { get; }
        public OrderController(IOrderService orderService) => OrderService = orderService;

        [HttpGet("employee/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployeeOrdersAsync([FromQuery] string id, [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1,
            CancellationToken token = default) =>
            Ok(await OrderService.GetPagedEmployeeOrdersAsync(new Guid(id), pageSize, pageNumber, token));

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "Create")]
            OrderDTO orderDto,
            CancellationToken token = default) => Ok(await OrderService.CreateAsync(orderDto, token));

        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CloseAsync([FromQuery] int id, CancellationToken token = default) =>
            Ok(await OrderService.CloseAsync(id, token));

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id, CancellationToken token = default) =>
            Ok(await OrderService.DeleteAsync(id, token));
    }
}
