using System;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Web.Code;
using OrderingService.Web.Code.Filters;

namespace OrderingService.Web.Controllers
{
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class OrderController : AbstractApiController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) => _orderService = orderService;

        [HttpGet("user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserOrdersAsync([FromRoute] string id, [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1) =>
            Ok(await _orderService.GetPagedOrdersByUserAsync(new Guid(id), pageSize, pageNumber));
        
        [HttpGet("employee/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployeeOrdersAsync([FromRoute] string id, [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1) =>
            Ok(await _orderService.GetPagedOrdersByEmployeeAsync(new Guid(id), pageSize, pageNumber));

        [HttpPost]
        [ServiceFilter(typeof(OrderClientAndEmployeeExistFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] [CustomizeValidator(RuleSet = "Create")]
            OrderDTO orderDto) => Ok(await _orderService.CreateAsync(orderDto));

        [HttpPut("take/{id:int}")]
        [ServiceFilter(typeof(OrderExistFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TakeAsync([FromRoute] int id) =>
            Ok(await _orderService.TakeOrderAsync(id));

        [HttpPut("decline/{id:int}")]
        [ServiceFilter(typeof(OrderExistFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeclineAsync([FromRoute] int id) =>
            Ok(await _orderService.DeclineOrderAsync(id));

        [HttpPut("confirm/{id:int}")]
        [ServiceFilter(typeof(OrderExistFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmCompletionAsync([FromRoute] int id) =>
            Ok(await _orderService.ConfirmOrderCompletion(id));

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(OrderExistFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id) =>
            Ok(await _orderService.DeleteAsync(id));
    }
}
