using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;

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
        public async Task<IActionResult> GetAllAsync(string serviceName, decimal? serviceMaxCost, CancellationToken token = default) =>
            await EmployeeService.FilterEmployeeProfilesAsync(serviceName, serviceMaxCost, token).ToHttpResponseAsync();

        [HttpPost]
        public async Task<IActionResult> CreateAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token = default)
        {
            employeeProfileDto.Id = new Guid(User.Identity.Name);
            return await EmployeeService.CreateEmployeeProfileAsync(employeeProfileDto, token).ToHttpResponseAsync();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(EmployeeProfileDTO employeeProfileDto, CancellationToken token = default)
        {
            employeeProfileDto.Id = new Guid(User.Identity.Name);
            return await EmployeeService.UpdateEmployeeServiceAsync(employeeProfileDto, token).ToHttpResponseAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id, CancellationToken token = default) => 
            await EmployeeService.DeleteEmployeeProfileAsync(new Guid(id), token).ToHttpResponseAsync();
    }
}
