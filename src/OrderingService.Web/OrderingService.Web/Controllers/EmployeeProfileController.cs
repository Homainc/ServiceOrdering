using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeProfileController : ControllerBase
    {
        private IEmployeeService EmployeeService { get; }
        public EmployeeProfileController(IEmployeeService employeeService)
        {
            EmployeeService = employeeService;
        }

        [HttpGet]
        public IEnumerable<EmployeeProfileDTO> GetAll(string serviceName, decimal? serviceMaxCost) =>
            EmployeeService.FilterEmployeeProfiles(serviceName, serviceMaxCost);

        [HttpPost]
        public IOperationResult Create(EmployeeProfileDTO employeeProfileDto) => 
            EmployeeService.CreateEmployeeProfile(employeeProfileDto);

        [HttpPut]
        public IOperationResult Update(EmployeeProfileDTO employeeProfileDto) =>
            EmployeeService.UpdateEmployeeService(employeeProfileDto);

        [HttpDelete("{id}")]
        public IOperationResult Delete(string id) => 
            EmployeeService.DeleteEmployeeProfile(id);
    }
}
