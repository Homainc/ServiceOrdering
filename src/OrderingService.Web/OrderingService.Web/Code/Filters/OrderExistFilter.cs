using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Web.Code.Filters
{
    public class OrderExistFilter : ActionFilterAttribute
    {
        private readonly IOrderService _orderService;
        public OrderExistFilter(IOrderService orderService) => _orderService = orderService;
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["id"] is int id)
            {
                if (!(await _orderService.AnyOrderByIdAsync(id)))
                {
                    context.Result = new NotFoundObjectResult(new
                    {
                        ErrorMessage = $"Order with id {id} not found!"
                    });
                }
                else
                    await next();
            }
            else
                await next();
        }
    }
}
