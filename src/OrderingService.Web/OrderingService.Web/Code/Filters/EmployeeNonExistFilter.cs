using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Web.Code.Filters
{
    public class EmployeeNonExistFilter : ActionFilterAttribute
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeNonExistFilter(IEmployeeService employeeService) => _employeeService = employeeService;
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["employeeProfileDto"] is EmployeeProfileDTO employee)
            {
                if (await _employeeService.AnyEmployeeByUserIdAsync(employee.UserId))
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        ErrorMessage = "Employee profile already exists!"
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
