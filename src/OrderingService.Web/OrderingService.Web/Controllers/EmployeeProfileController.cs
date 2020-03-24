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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeProfileController : ControllerBase
    {
        private IEmployeeService EmployeeService { get; }
        private  ILogger<EmployeeProfileController> Logger { get; }

        public EmployeeProfileController(IEmployeeService employeeService, ILogger<EmployeeProfileController> logger)
        {
            EmployeeService = employeeService;
            Logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployeesAsync(
            [FromQuery] string serviceName = null,
            [FromQuery] decimal? serviceMaxCost = null,
            [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1,
            CancellationToken token = default)
        {
            Logger.LogDebug("{0} has been invoked", nameof(GetEmployeesAsync));
            IPagedResponse<EmployeeProfileDTO> response;
            var result = await EmployeeService.GetPagedEmployeesAsync(serviceName, serviceMaxCost, pageSize, pageNumber, token);
            if (result.DidError)
            {
                response = new PagedResponse<EmployeeProfileDTO>(result.ErrorMessage);
                Logger.LogDebug("Validation errors occurred: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            response = new PagedResponse<EmployeeProfileDTO>(result.Value);
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
            Logger.LogDebug("{0} has been invoked", nameof(CreateAsync));
            IResponse<EmployeeProfileDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<EmployeeProfileDTO>(ModelState.GetErrorsString());
                Logger.LogDebug("Validation errors occurred: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            employeeProfileDto.Id = new Guid(User.Identity.Name);
            var result = await EmployeeService.CreateEmployeeAsync(employeeProfileDto, token);
            if (result.DidError)
            {
                response = new Response<EmployeeProfileDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
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
            Logger.LogDebug("{0} has been invoked", nameof(UpdateAsync));
            IResponse<EmployeeProfileDTO> response;
            if (!ModelState.IsValid)
            {
                response = new Response<EmployeeProfileDTO>(ModelState.GetErrorsString());
                Logger.LogDebug("Validation errors occurred: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            employeeProfileDto.Id = new Guid(User.Identity.Name);
            var result = await EmployeeService.UpdateEmployeeAsync(employeeProfileDto, token);
            if (result.DidError)
            {
                response = new Response<EmployeeProfileDTO>(result.ErrorMessage);
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
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
            Logger.LogDebug("{0} has been invoked", nameof(DeleteAsync));
            IResponse<EmployeeProfileDTO> response;
            var result = await EmployeeService.DeleteEmployeeAsync(new Guid(id), token);
            if (result.DidError)
            {
                response = new Response<EmployeeProfileDTO>(ModelState.GetErrorsString());
                Logger.LogDebug("BLL error occured: {0}", response.ErrorMessage);
                return BadRequest(response);
            }

            response = new Response<EmployeeProfileDTO>(result.Value);
            return Ok(response);
        }
    }
}
