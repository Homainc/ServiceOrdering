using System;
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
        public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute] string id) => 
            Ok(await _employeeService.GetEmployeeByIdAsync(new Guid(id)));

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployeesAsync(
            [FromQuery] string searchString = null,
            [FromQuery] int? serviceTypeId = null,
            [FromQuery] decimal? maxServiceCost = null,
            [FromQuery] int? minAverageRate = null,
            [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1) =>
            Ok(await _employeeService.GetPagedEmployeesAsync(pageSize, pageNumber, searchString, maxServiceCost, minAverageRate, serviceTypeId));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAsync(
            [FromBody]
            [CustomizeValidator(RuleSet = "Create")]
            EmployeeProfileDTO employeeProfileDto)
        {
            employeeProfileDto.UserId = new Guid(User.Identity.Name);
            return Ok(await _employeeService.CreateEmployeeAsync(employeeProfileDto));  
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute]
            string id,
            [FromBody]
            [CustomizeValidator(RuleSet = "Id,Create")]
            EmployeeProfileDTO employeeProfileDto)
        {
            employeeProfileDto.Id = new Guid(id);
            return Ok(await _employeeService.UpdateEmployeeAsync(employeeProfileDto));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id) => 
            Ok(await _employeeService.DeleteEmployeeAsync(new Guid(id)));
    }
}
