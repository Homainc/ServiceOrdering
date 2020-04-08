using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Web.Code.Filters
{
    public class UserExistsByEmailFilter : ActionFilterAttribute
    {
        private readonly IUserService _userService;
        public UserExistsByEmailFilter(IUserService userService) => _userService = userService;
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments["userDto"] is UserDTO user)
            {
                if (!await _userService.AnyUserByEmailAsync(user.Email))
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        ErrorMessage = $"User with email {user.Email} not found!"
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
