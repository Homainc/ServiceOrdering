using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Web.Code;

namespace OrderingService.Web.Controllers
{
    public class AccountController : AbstractApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService) => _userService = userService;

        [HttpPost("auth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AuthAsync(
            [FromBody] [CustomizeValidator(RuleSet = "LogIn")]
            UserDTO userDto) => Ok(await _userService.AuthenticateAsync(userDto));

        [HttpPost("sign-up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUpAsync(
            [FromBody] [CustomizeValidator(RuleSet = "SignUp")]
            UserDTO userDto) => Ok(await _userService.SignUpAsync(userDto));

        [Authorize]
        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProfileAsync() =>
            Ok(await _userService.GetUserByIdAsync(new Guid(User.Identity.Name)));

        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateProfileAsync(
            [FromBody] [CustomizeValidator(RuleSet = "Update")]
            UserDTO userDto) => Ok(await _userService.UpdateProfileAsync(userDto));
    }
}
