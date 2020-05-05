using System;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Web.Code;
using OrderingService.Web.Code.Interfaces;
using OrderingService.Web.Hubs;

namespace OrderingService.Web.Controllers
{
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class OrderController : AbstractApiController
    {
        private readonly IOrderService _orderService;
        private readonly INotificationService _notificationService;

        public OrderController(IOrderService orderService, INotificationService notificationService)
        {
            _orderService = orderService;
            _notificationService = notificationService;
        }

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] [CustomizeValidator(RuleSet = "Create")]
            OrderDTO orderDto)
        {
            var order = await _orderService.CreateAsync(orderDto);

            await _notificationService.NoticeByEmployeeIdAsync(order.EmployeeId,
                $"You've got new task \"{order.BriefTask}\"!");

            return Ok(order);
        }

        [HttpPut("take/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TakeAsync([FromRoute] int id)
        {
            var order = await _orderService.TakeOrderAsync(id);

            await _notificationService.NoticeByUserIdAsync(order.ClientId,
                $"Your order \"{order.BriefTask}\" was accepted by employee");
            
            return Ok(order);
        }

        [HttpPut("decline/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeclineAsync([FromRoute] int id)
        {
            var order = await _orderService.DeclineOrderAsync(id);

            await _notificationService.NoticeByUserIdAsync(order.ClientId,
                $"Your order \"{order.BriefTask}\" was declined by employee");

            return Ok(order);
        }

        [HttpPut("confirm/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmCompletionAsync([FromRoute] int id)
        {
            var order = await _orderService.ConfirmOrderCompletion(id);

            await _notificationService.NoticeByEmployeeIdAsync(order.EmployeeId,
                $"The client has confirmed completion of the task \"{order.BriefTask}\"");

            return Ok(order);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id) =>
            Ok(await _orderService.DeleteAsync(id));
    }
}
