using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Web.Code.Filters
{
    public class UserExistsFilter : ActionFilterAttribute
    {
        private readonly IUserService _userService;
        public UserExistsFilter(IUserService userService) => _userService = userService;
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["id"] is Guid id)
            {
                if (!await _userService.AnyUserByIdAsync(id))
                {
                    context.Result = new NotFoundObjectResult(new
                    {
                        ErrorMessage = $"User with id {id} not found!"
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
