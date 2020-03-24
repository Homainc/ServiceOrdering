using System;
using AutoMapper;
using OrderingService.Data.Models;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace OrderingService.Logic.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void Can_create_employee_profile()
        {
            // Assign
            var dbName = "Can_create_employee_profile";
            var employeeDto = Initializers.DefaultEmployeeProfile;
            using var userService = Initializers.FakeUserService(dbName);
            using var employeeService = Initializers.FakeEmployeeService(dbName);
            employeeDto.User.Id = userService.SignUpAsync(employeeDto.User, default).Result.Value.Id;
            
            // Action
            var result = employeeService.CreateEmployeeAsync(employeeDto, default).Result;

            // Assert
            Assert.False(result.DidError);
            Assert.Equal(employeeDto.Description, result.Value.Description);
            Assert.Equal(employeeDto.User.Id, result.Value.User.Id);
            Assert.Equal(employeeDto.ServiceCost, result.Value.ServiceCost);
            Assert.Equal(employeeDto.ServiceType.ToLower(), result.Value.ServiceType);
        }

        [Fact]
        public void Can_update_employee_profile()
        {
            // Assign
            var employeeProfile = Initializers.DefaultEmployeeProfile;
            const string dbName = "Can_update_employee_profile";
            using (var userService = Initializers.FakeUserService(dbName))
            {
                userService.CreateAsync(employeeProfile.User, default).Wait();
            }
            using (var employeeService = Initializers.FakeEmployeeService(dbName))
            {
                employeeProfile.Id = employeeService.CreateEmployeeAsync(employeeProfile, default).Result.Value.Id;
            }

            // Action
            employeeProfile.ServiceType = "top";
            employeeProfile.ServiceCost = 10;
            employeeProfile.Description = "best+test";
            using var employeeService2 = Initializers.FakeEmployeeService(dbName);
            var result = employeeService2.UpdateEmployeeAsync(employeeProfile, default).Result;

            // Assert
            Assert.False(result.DidError);
            Assert.Equal(employeeProfile.ServiceType, result.Value.ServiceType);
            Assert.Equal(employeeProfile.ServiceCost, result.Value.ServiceCost);
            Assert.Equal(employeeProfile.Description, result.Value.Description);
        }

        [Fact]
        public void Can_delete_employee_profile()
        {
            // Assign
            const string dbName = "Can_delete_employee_profile";
            var employeeProfile = Initializers.DefaultEmployeeProfile;
            using (var userService = Initializers.FakeUserService(dbName))
            {
                userService.CreateAsync(employeeProfile.User, default).Wait();
            }
            using (var employeeService = Initializers.FakeEmployeeService(dbName))
            {
                employeeProfile.Id = employeeService.CreateEmployeeAsync(employeeProfile, default).Result.Value.Id;
            }

            // Action
            using var employeeService2 = Initializers.FakeEmployeeService(dbName);
            var result = employeeService2.DeleteEmployeeAsync(employeeProfile.Id, default).Result;

            // Assert
            Assert.False(result.DidError);
        }
    }
}
