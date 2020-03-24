using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic;
using OrderingService.Domain.Logic.Interfaces;
using OrderingService.Web.Interfaces;

namespace OrderingService.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeProfileController : ControllerBase
    {
        private IEmployeeService EmployeeService { get; }
        public EmployeeProfileController(IEmployeeService employeeService)
        {
            EmployeeService = employeeService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] string serviceName,
            [FromQuery] decimal? serviceMaxCost,
            CancellationToken token = default)
        {
            IPagedResult<EmployeeProfileDTO> response;
            var result = await EmployeeService.FilterEmployeeProfilesAsync(serviceName, serviceMaxCost, token);
            if (result.DidError)
            {
                response = new PagedResult<EmployeeProfileDTO>(result.ErrorMessage);
                return BadRequest(response);
            }

            response = new PagedResult<EmployeeProfileDTO>(result.Value);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "Create")]
            EmployeeProfileDTO employeeProfileDto,
            CancellationToken token = default)
        {
            IResponse<EmployeeProfileDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<EmployeeProfileDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            employeeProfileDto.Id = new Guid(User.Identity.Name);
            var result = await EmployeeService.CreateEmployeeProfileAsync(employeeProfileDto, token);
            if (result.DidError)
            {
                response = new Response<EmployeeProfileDTO>(result.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<EmployeeProfileDTO>(result.Value);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "Id,Create")]
            EmployeeProfileDTO employeeProfileDto,
            CancellationToken token = default)
        {
            IResponse<EmployeeProfileDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<EmployeeProfileDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            employeeProfileDto.Id = new Guid(User.Identity.Name);
            var result = await EmployeeService.UpdateEmployeeServiceAsync(employeeProfileDto, token);
            if (result.DidError)
            {
                response = new Response<EmployeeProfileDTO>(result.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<EmployeeProfileDTO>(result.Value);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DeleteAsync([FromQuery] string id, CancellationToken token = default)
        {
            IResponse<EmployeeProfileDTO> response;
            var result = await EmployeeService.DeleteEmployeeProfileAsync(new Guid(id), token);
            if (result.DidError)
            {
                response = new Response<EmployeeProfileDTO>(ModelState.GetErrorsString());
                return BadRequest(response);
            }

            response = new Response<EmployeeProfileDTO>(result.Value);
            return Ok(response);
        }
    }
}
