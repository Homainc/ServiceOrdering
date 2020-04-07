using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Web.Code.Filters
{
    public class EmployeeExistFilter : ActionFilterAttribute
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeExistFilter(IEmployeeService employeeService) => _employeeService = employeeService;
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["id"] is string idStr)
            {
                var guid = new Guid(idStr);
                if (!(await _employeeService.AnyEmployeeByIdAsync(guid)))
                {
                    context.Result = new NotFoundObjectResult(new
                    {
                        ErrorMessage = $"Employee with id {idStr} not found!"
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
