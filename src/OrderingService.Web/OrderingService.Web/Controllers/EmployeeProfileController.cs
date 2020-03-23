﻿using System;
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
        public IActionResult GetAll(string serviceName, decimal? serviceMaxCost) =>
            EmployeeService.FilterEmployeeProfiles(serviceName, serviceMaxCost).ToHttpResponse();

        [HttpPost]
        public IActionResult Create(EmployeeProfileDTO employeeProfileDto)
        {
            employeeProfileDto.Id = new Guid(User.Identity.Name);
            return EmployeeService.CreateEmployeeProfile(employeeProfileDto).ToHttpResponse();
        }

        [HttpPut]
        public IActionResult Update(EmployeeProfileDTO employeeProfileDto)
        {
            employeeProfileDto.Id = new Guid(User.Identity.Name);
            return EmployeeService.UpdateEmployeeService(employeeProfileDto).ToHttpResponse();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id) => 
            EmployeeService.DeleteEmployeeProfile(new Guid(id)).ToHttpResponse();
    }
}
