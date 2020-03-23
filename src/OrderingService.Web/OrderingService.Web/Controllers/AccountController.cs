using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Create(UserDTO userDto, CancellationToken token = default) => 
            await UserService.CreateAsync(userDto, token).ToHttpResponseAsync();

        [HttpPost("auth")]
        public async Task<IActionResult> Auth(UserDTO userDto, CancellationToken token = default) =>
            await UserService.AuthenticateAsync(userDto, token).ToHttpResponseAsync();

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(UserDTO userDto, CancellationToken token = default) =>
            await UserService.SignUpAsync(userDto, token).ToHttpResponseAsync();

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(CancellationToken token = default) =>
            await UserService.GetUserByIdAsync(new Guid(User.Identity.Name), token).ToHttpResponseAsync();

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProfileAsync(UserDTO userDto, CancellationToken token = default)
        {
            userDto.Id = new Guid(User.Identity.Name);
            return await UserService.UpdateProfileAsync(userDto, token).ToHttpResponseAsync();
        }
    }
}
