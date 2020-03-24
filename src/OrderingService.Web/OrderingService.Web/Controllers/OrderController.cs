using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;
using OrderingService.Web.Interfaces;

namespace OrderingService.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderService OrderService { get; }
        private ILogger<OrderController> Logger { get; }
        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            OrderService = orderService;
            Logger = logger;
        }

        [HttpGet("employee/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployeeOrdersAsync([FromQuery] string id, [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1,
            CancellationToken token = default)
        {
            Logger.LogDebug("{0} has been invoked", nameof(GetEmployeeOrdersAsync));
            IPagedResponse<OrderDTO> response;
            var result = await OrderService.GetPagedEmployeeOrdersAsync(new Guid(id), pageSize, pageNumber, token);
            if (result.DidError)
            {
                response = new PagedResponse<OrderDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            response = new PagedResponse<OrderDTO>(result.Value);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "Create")]
            OrderDTO orderDto,
            CancellationToken token = default)
        {
            Logger.LogDebug("{0} has been invoked", nameof(CreateAsync));
            IResponse<OrderDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<OrderDTO>(ModelState.GetErrorsString());
                Logger.LogDebug("Validation errors occurred: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            var result = await OrderService.CreateAsync(orderDto, token);
            if (result.DidError)
            {
                response = new Response<OrderDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<OrderDTO>(result.Value);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CloseAsync([FromQuery] int id, CancellationToken token = default)
        {
            Logger.LogDebug("{0} has been invoked", nameof(CloseAsync));
            IResponse<OrderDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<OrderDTO>(ModelState.GetErrorsString());
                Logger.LogDebug("Validation errors occurred: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            var result = await OrderService.CloseAsync(id, token);
            if (result.DidError)
            {
                response = new Response<OrderDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<OrderDTO>(result.Value);
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id, CancellationToken token = default)
        {
            Logger.LogDebug("{0} has been invoked", nameof(DeleteAsync));
            IResponse<OrderDTO> response;
            var result = await OrderService.DeleteAsync(id, token);
            if (result.DidError)
            {
                response = new Response<OrderDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<OrderDTO>(result.Value);
            return Ok(response);
        }
    }
}
