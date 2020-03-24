using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;
using OrderingService.Web.Interfaces;

namespace OrderingService.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IUserService UserService { get; }
        private ILogger<AccountController> Logger { get; }

        public AccountController(IUserService userService, ILogger<AccountController> logger)
        {
            UserService = userService;
            Logger = logger;
        }

        [HttpPost("auth")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AuthAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "LogIn")]
            UserDTO userDto, 
            CancellationToken token = default)
        {
            Logger.LogDebug("{0} has been invoked", nameof(AuthAsync));
            IResponse<UserDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<UserDTO>(ModelState.GetErrorsString());
                Logger.LogDebug("Validation errors occurred: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            var result = await UserService.AuthenticateAsync(userDto, token);
            if (result.DidError)
            {
                response = new Response<UserDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<UserDTO>(result.Value);
            return Ok(response);
        }

        [HttpPost("sign-up")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SignUpAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "Create")]
            UserDTO userDto,
            CancellationToken token = default)
        {
            Logger.LogDebug("{0} has been invoked", nameof(SignUpAsync));
            IResponse<UserDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<UserDTO>(ModelState.GetErrorsString());
                Logger.LogDebug("Validation errors occurred: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            var result = await UserService.SignUpAsync(userDto, token);
            if (result.DidError)
            {
                response = new Response<UserDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<UserDTO>(result.Value);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("profile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetProfileAsync(CancellationToken token = default)
        {
            Logger.LogDebug("{0} has been invoked", nameof(GetProfileAsync));
            IResponse<UserDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<UserDTO>(ModelState.GetErrorsString());
                Logger.LogDebug("Validation errors occurred: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            var result = await UserService.GetUserByIdAsync(new Guid(User.Identity.Name), token);
            if (result.DidError)
            {
                response = new Response<UserDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<UserDTO>(result.Value);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateProfileAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "Id,Create")]
            UserDTO userDto,
            CancellationToken token = default)
        {
            Logger.LogDebug("{0} has been invoked", nameof(UpdateProfileAsync));
            IResponse<UserDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<UserDTO>(ModelState.GetErrorsString());
                Logger.LogDebug("Validation errors occurred: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            userDto.Id = new Guid(User.Identity.Name);
            var result = await UserService.UpdateProfileAsync(userDto, token);
            if (result.DidError)
            {
                response = new Response<UserDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<UserDTO>(result.Value);
            return Ok(response);
        }
    }
}
