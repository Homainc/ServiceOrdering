using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Web.Code;
using OrderingService.Web.Models;

namespace OrderingService.Web.Controllers
{
    public class AccountController : AbstractApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService) => _userService = userService;

        [HttpPost("auth")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserAuthDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> AuthAsync(
            [FromBody] UserLoginModel loginModel) =>
            Ok(await _userService.AuthenticateAsync(loginModel.UserEmail, loginModel.UserPassword));

        [HttpPost("sign-up")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserAuthDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> SignUpAsync(
            [FromBody] UserCreateDto userDto) => Ok(await _userService.SignUpAsync(userDto));

        [Authorize]
        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProfileAsync() =>
            Ok(await _userService.GetUserByIdAsync(new Guid(User.Identity.Name)));

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateProfileAsync(
            [FromRoute] Guid id,
            [FromBody] UserDto userDto)
        {
            userDto.Id = id;
            return Ok(await _userService.UpdateProfileAsync(userDto));
        }
    }
}
