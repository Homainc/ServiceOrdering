using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private IUserService UserService { get; }
        public AccountController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDto) => 
            new JsonResult(await UserService.CreateAsync(userDto));
    }
}
