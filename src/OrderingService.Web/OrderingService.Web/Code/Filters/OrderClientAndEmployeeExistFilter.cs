using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Web.Code.Filters
{
    public class OrderClientAndEmployeeExistFilter : ActionFilterAttribute
    {
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        public OrderClientAndEmployeeExistFilter(IUserService userService, IEmployeeService employeeService)
        {
            _userService = userService;
            _employeeService = employeeService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["orderDto"] is OrderDTO order)
            {
                if(!(await _userService.AnyUserByIdAsync(new Guid(order.ClientId))))
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        ErrorMessage = $"Client with id {order.ClientId} not found!"
                    });
                }
                else if (!(await _employeeService.AnyEmployeeByIdAsync(new Guid(order.EmployeeId))))
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        ErrorMessage = $"Employee with id {order.EmployeeId} not found!"
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
