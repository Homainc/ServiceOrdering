using System;
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
    [Authorize]
    public class EmployeeProfileController : AbstractApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeProfileController(IEmployeeService employeeService) => _employeeService = employeeService;

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute] string id, CancellationToken token = default) => 
            Ok(await _employeeService.GetEmployeeByIdAsync(new Guid(id), token));

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployeesAsync(
            [FromQuery] string serviceName = null,
            [FromQuery] decimal? serviceMaxCost = null,
            [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1,
            CancellationToken token = default) =>
            Ok(await _employeeService.GetPagedEmployeesAsync(serviceName, serviceMaxCost, pageSize, pageNumber, token));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "Create")]
            EmployeeProfileDTO employeeProfileDto,
            CancellationToken token = default)
        {
            employeeProfileDto.UserId = new Guid(User.Identity.Name);
            return Ok(await _employeeService.CreateEmployeeAsync(employeeProfileDto, token));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "Id,Create")]
            EmployeeProfileDTO employeeProfileDto,
            CancellationToken token = default)
        {
            employeeProfileDto.UserId = new Guid(User.Identity.Name);
            return Ok(await _employeeService.UpdateEmployeeAsync(employeeProfileDto, token));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromQuery] string id, CancellationToken token = default) => 
            Ok(await _employeeService.DeleteEmployeeAsync(new Guid(id), token));
    }
}
