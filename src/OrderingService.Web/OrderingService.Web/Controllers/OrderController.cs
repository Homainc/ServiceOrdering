using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;
using OrderingService.Web.Interfaces;

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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployeeOrdersAsync([FromQuery] string id, CancellationToken token = default)
        {
            IPagedResponse<OrderDTO> response;
            var result = await OrderService.GetEmployeeOrdersAsync(new System.Guid(id), token);
            if (result.DidError)
            {
                response = new PagedResponse<OrderDTO>(result.ErrorMessage);
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
            IResponse<OrderDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<OrderDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            var result = await OrderService.CreateAsync(orderDto, token);
            if (result.DidError)
            {
                response = new Response<OrderDTO>(result.ErrorMessage);
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
            IResponse<OrderDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<OrderDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            var result = await OrderService.CloseAsync(id, token);
            if (result.DidError)
            {
                response = new Response<OrderDTO>(result.ErrorMessage);
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
            IResponse<OrderDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<OrderDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            var result = await OrderService.DeleteAsync(id, token);
            if (result.DidError)
            {
                response = new Response<OrderDTO>(result.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<OrderDTO>(result.Value);
            return Ok(response);
        }
    }
}
