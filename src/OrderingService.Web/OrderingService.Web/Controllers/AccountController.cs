using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IUserService UserService { get; }
        public AccountController(IUserService userService) =>
            UserService = userService;

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDto) => 
            (await UserService.CreateAsync(userDto)).ToHttpResponse();

        [HttpPost("auth")]
        public async Task<IActionResult> Auth(UserDTO userDto) =>
            (await UserService.AuthenticateAsync(userDto)).ToHttpResponse();

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(UserDTO userDto) =>
            (await UserService.SignUpAsync(userDto)).ToHttpResponse();
    }
}
