using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;
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
            employeeDto.User.Id = userService.SignUpAsync(employeeDto.User, default).Result.Id;
            
            // Action
            var result = employeeService.CreateEmployeeAsync(employeeDto, default).Result;

            // Assert
            Assert.Equal(employeeDto.Description, result.Description);
            Assert.Equal(employeeDto.User.Id, result.User.Id);
            Assert.Equal(employeeDto.ServiceCost, result.ServiceCost);
            Assert.Equal(employeeDto.ServiceType.ToLower(), result.ServiceType);
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
                userService.CreateAsync(employeeProfile.User, default).Wait();
                var employeeService = Initializers.FakeEmployeeService(context);
                employeeProfile.Id = employeeService.CreateEmployeeAsync(employeeProfile, default).Result.Id;
            }

            // Action
            employeeProfile.ServiceType = "top";
            employeeProfile.ServiceCost = 10;
            employeeProfile.Description = "best+test";
            EmployeeProfileDTO result;
            using(var context = Initializers.FakeContext(dbName)){
                var employeeService = Initializers.FakeEmployeeService(context);
                result = employeeService.UpdateEmployeeAsync(employeeProfile, default).Result;
            }

            // Assert
            Assert.Equal(employeeProfile.ServiceType, result.ServiceType);
            Assert.Equal(employeeProfile.ServiceCost, result.ServiceCost);
            Assert.Equal(employeeProfile.Description, result.Description);
        }

        [Fact]
        public void Can_delete_employee_profile()
        {
            // Assign
            const string dbName = "Can_delete_employee_profile";
            var employeeProfile = Initializers.DefaultEmployeeProfile;
            using var context = Initializers.FakeContext(dbName);
            var userService = Initializers.FakeUserService(context);
            userService.CreateAsync(employeeProfile.User, default).Wait();
            var employeeService = Initializers.FakeEmployeeService(context);
            employeeProfile.Id = employeeService.CreateEmployeeAsync(employeeProfile, default).Result.Id;

            // Action
            var result = employeeService.DeleteEmployeeAsync(employeeProfile.Id, default).Result;

            // Assert
            // No exceptions
            Assert.Equal(employeeProfile.Id, result.Id);
        }

        [Fact]
        public void Can_employee_paging_and_filtering()
        {
            // Assign
            const string dbName = "Can_employee_paging_filtering";
            string[] serviceTypes = {"IT-specialist", "plumber", "doctor", "sales rep", "nurse", "guitarist", "teacher", "engineer", "architect"};
            using var context = Initializers.FakeContext(dbName);
            var userService = Initializers.FakeUserService(context);
            var employeeService = Initializers.FakeEmployeeService(context);
            for (int i = 1; i < 10; i++)
            {
                var current = Initializers.DefaultEmployeeProfile;
                current.User.Email = $"test{i}@gmail.com";
                current.UserId = userService.CreateAsync(current.User, default).Result.Id;
                current.ServiceCost = i;
                current.ServiceType = serviceTypes[i-1];
                employeeService.CreateEmployeeAsync(current, default).Wait();
            }

            // Action
            var result1 = employeeService.GetPagedEmployeesAsync("nurse", 10, 1, 1, default).Result;
            var result2 = employeeService.GetPagedEmployeesAsync(null, null, 3, 1, default).Result;
            var result3 = employeeService.GetPagedEmployeesAsync(null, null, 5, 2, default).Result;

            // Assert
            Assert.Equal(1, result1.PagesCount);
            Assert.Equal(6, result2.PagesCount);
        }
    }
}
