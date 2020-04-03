using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Web.Code;

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
            [FromQuery] int pageNumber = 1,
            CancellationToken token = default) =>
            Ok(await _orderService.GetPagedOrdersByUserAsync(new Guid(id), pageSize, pageNumber, token));
        
        [HttpGet("employee/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployeeOrdersAsync([FromRoute] string id, [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1,
            CancellationToken token = default) =>
            Ok(await _orderService.GetPagedEmployeeOrdersAsync(new Guid(id), pageSize, pageNumber, token));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "Create")]
            OrderDTO orderDto,
            CancellationToken token = default) => Ok(await _orderService.CreateAsync(orderDto, token));

        [HttpPut("take/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TakeAsync([FromRoute] int id, CancellationToken token = default) =>
            Ok(await _orderService.TakeOrderAsync(id, token));

        [HttpPut("decline/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeclineAsync([FromRoute] int id, CancellationToken token = default) =>
            Ok(await _orderService.DeclineOrderAsync(id, token));

        [HttpPut("confirm/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmCompletionAsync([FromRoute] int id, CancellationToken token = default) =>
            Ok(await _orderService.ConfirmOrderCompletion(id, token));

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken token = default) =>
            Ok(await _orderService.DeleteAsync(id, token));
    }
}
