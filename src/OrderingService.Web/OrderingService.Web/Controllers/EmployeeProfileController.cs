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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeProfileController : ControllerBase
    {
        private IEmployeeService EmployeeService { get; }

        public EmployeeProfileController(IEmployeeService employeeService) => EmployeeService = employeeService;

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployeesAsync(
            [FromQuery] string serviceName = null,
            [FromQuery] decimal? serviceMaxCost = null,
            [FromQuery] int pageSize = 5,
            [FromQuery] int pageNumber = 1,
            CancellationToken token = default) =>
            Ok(await EmployeeService.GetPagedEmployeesAsync(serviceName, serviceMaxCost, pageSize, pageNumber, token));

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
            employeeProfileDto.UserId = new Guid(User.Identity.Name);
            return Ok(await EmployeeService.CreateEmployeeAsync(employeeProfileDto, token));
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
            employeeProfileDto.UserId = new Guid(User.Identity.Name);
            return Ok(await EmployeeService.UpdateEmployeeAsync(employeeProfileDto, token));
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DeleteAsync([FromQuery] string id, CancellationToken token = default) => 
            Ok(await EmployeeService.DeleteEmployeeAsync(new Guid(id), token));
    }
}
