using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Web.Code.Filters
{
    public class ReviewClientAndEmployeeExistFilter : ActionFilterAttribute
    {
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;

        public ReviewClientAndEmployeeExistFilter(IUserService userService, IEmployeeService employeeService
        )
        {
            _userService = userService;
            _employeeService = employeeService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["reviewDto"] is ReviewDTO review)
            {
                if (!await _userService.AnyUserByIdAsync(review.ClientId))
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        ErrorMessage = $"Client with id {review.ClientId} not found!"
                    });
                }
                else if (!await _employeeService.AnyEmployeeByIdAsync(review.EmployeeId))
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        ErrorMessage = $"Employee with id {review.EmployeeId} not found!"
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
