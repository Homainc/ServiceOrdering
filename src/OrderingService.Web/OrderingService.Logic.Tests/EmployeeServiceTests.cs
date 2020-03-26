using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;
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

        [Fact]
        public void Can_employee_paging_and_filtering()
        {
            // Assign
            const string dbName = "Can_employee_paging_filtering";
            string[] serviceTypes = {"IT-specialist", "plumber", "doctor", "sales rep", "nurse", "guitarist", "teacher", "engineer", "architect"};
            for (int i = 1; i < 10; i++)
            {
                var current = Initializers.DefaultEmployeeProfile;
                using (var userService = Initializers.FakeUserService(dbName))
                {
                    current.User.Email = $"test{i}@gmail.com";
                    current.UserId = userService.CreateAsync(current.User, default).Result.Value.Id;
                }
                using (var employeeService = Initializers.FakeEmployeeService(dbName))
                {
                    current.ServiceCost = i;
                    current.ServiceType = serviceTypes[i-1];
                    employeeService.CreateEmployeeAsync(current, default).Wait();
                }
            }

            // Action
            IPagedResult<EmployeeProfileDTO> result1, result2, result3;
            using (var employeeService = Initializers.FakeEmployeeService(dbName))
            {
                result1 = employeeService.GetPagedEmployeesAsync("nurse", 10, 1, 1, default).Result;
                result2 = employeeService.GetPagedEmployeesAsync(null, null, 3, 1, default).Result;
                result3 = employeeService.GetPagedEmployeesAsync(null, null, 5, 2, default).Result;
            }

            // Assert
            Assert.False(result1.DidError);
            Assert.Equal(1, result1.PagesCount);
            Assert.False(result2.DidError);
            Assert.Equal(3, result2.PagesCount);
            Assert.False(result3.DidError);
        }
    }
}
