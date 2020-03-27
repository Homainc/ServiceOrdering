using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IUserService UserService { get; }

        public AccountController(IUserService userService) => UserService = userService;

        [HttpPost("auth")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AuthAsync(
            [FromBody] [CustomizeValidator(RuleSet = "LogIn")]
            UserDTO userDto,
            CancellationToken token = default) => Ok(await UserService.AuthenticateAsync(userDto, token));

        [HttpPost("sign-up")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SignUpAsync(
            [FromBody] [CustomizeValidator(RuleSet = "SignUp")]
            UserDTO userDto,
            CancellationToken token = default) => Ok(await UserService.SignUpAsync(userDto, token));

        [Authorize]
        [HttpGet("profile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetProfileAsync(CancellationToken token = default) =>
            Ok(await UserService.GetUserByIdAsync(new Guid(User.Identity.Name), token));

        [Authorize]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateProfileAsync(
            [FromBody] [CustomizeValidator(RuleSet = "Id,SignUp")]
            UserDTO userDto,
            CancellationToken token = default) => Ok(await UserService.UpdateProfileAsync(userDto, token));
    }
}
