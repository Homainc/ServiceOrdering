using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public AccountController(IUserService userService) =>
            UserService = userService;

        [HttpPost("auth")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Auth(UserDTO userDto, CancellationToken token = default)
        {
            IResponse<UserDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<UserDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            var result = await UserService.AuthenticateAsync(userDto, token);
            if (result.DidError)
            {
                response = new Response<UserDTO>(result.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<UserDTO>(result.Value);
            return Ok(response);
        }

        [HttpPost("sign-up")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SignUp(UserDTO userDto, CancellationToken token = default)
        {
            IResponse<UserDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<UserDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            var result = await UserService.SignUpAsync(userDto, token);
            if (result.DidError)
            {
                response = new Response<UserDTO>(result.ErrorMessage);
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
        public async Task<IActionResult> GetProfile(CancellationToken token = default)
        {
            IResponse<UserDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<UserDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            var result = await UserService.GetUserByIdAsync(new Guid(User.Identity.Name), token);
            if (result.DidError)
            {
                response = new Response<UserDTO>(result.ErrorMessage);
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
        public async Task<IActionResult> UpdateProfileAsync(UserDTO userDto, CancellationToken token = default)
        {
            IResponse<UserDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<UserDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            userDto.Id = new Guid(User.Identity.Name);
            var result = await UserService.UpdateProfileAsync(userDto, token);
            if (result.DidError)
            {
                response = new Response<UserDTO>(result.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<UserDTO>(result.Value);
            return Ok(response);
        }
    }
}
