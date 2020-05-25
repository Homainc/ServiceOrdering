using OrderingService.Domain;
using Xunit;

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
            using var context = Initializers.FakeContext(dbName);
            var userService = Initializers.FakeUserService(context);
            var employeeService = Initializers.FakeEmployeeService(context);
            //employeeDto.User.Id = userService.SignUpAsync(employeeDto.User).Result.Id;
            
            // Action
            //var result = employeeService.CreateEmployeeAsync(employeeDto).Result;

            // Assert
            //Assert.Equal(employeeDto.Description, result.Description);
            //Assert.Equal(employeeDto.User.Id, result.User.Id);
            //Assert.Equal(employeeDto.ServiceCost, result.ServiceCost);
            //Assert.Equal(employeeDto.ServiceType.ToLower(), result.ServiceType);
        }

        [Fact]
        public void Can_update_employee_profile()
        {
            // Assign
            var employeeProfile = Initializers.DefaultEmployeeProfile;
            const string dbName = "Can_update_employee_profile";
            using (var context = Initializers.FakeContext(dbName))
            {
                var userService = Initializers.FakeUserService(context);
                //userService.CreateAsync(employeeProfile.User).Wait();
                var employeeService = Initializers.FakeEmployeeService(context);
                //employeeProfile.Id = employeeService.CreateEmployeeAsync(employeeProfile).Result.Id;
            }

            // Action
            employeeProfile.ServiceType = "top";
            employeeProfile.ServiceCost = 10;
            employeeProfile.Description = "best+test";
            EmployeeProfileDto result;
            using(var context = Initializers.FakeContext(dbName)){
                var employeeService = Initializers.FakeEmployeeService(context);
                //result = employeeService.UpdateEmployeeAsync(employeeProfile).Result;
            }

            // Assert
            //Assert.Equal(employeeProfile.ServiceType, result.ServiceType);
            //Assert.Equal(employeeProfile.ServiceCost, result.ServiceCost);
            //Assert.Equal(employeeProfile.Description, result.Description);
        }

        [Fact]
        public void Can_delete_employee_profile()
        {
            // Assign
            const string dbName = "Can_delete_employee_profile";
            var employeeProfile = Initializers.DefaultEmployeeProfile;
            using var context = Initializers.FakeContext(dbName);
            var userService = Initializers.FakeUserService(context);
            //userService.CreateAsync(employeeProfile.User).Wait();
            var employeeService = Initializers.FakeEmployeeService(context);
            //employeeProfile.Id = employeeService.CreateEmployeeAsync(employeeProfile).Result.Id;

            // Action
            var result = employeeService.DeleteEmployeeAsync(employeeProfile.Id).Result;

            // Assert
            // No exceptions
            Assert.Equal(employeeProfile.Id, result.Id);
        }
    }
}
