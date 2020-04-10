using System;
using OrderingService.Common;
using OrderingService.Domain;
using Xunit;

namespace OrderingService.Logic.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async void Can_create_order()
        {
            // Assign
            await using var context = Initializers.FakeContext("Can_create_order");
            var userService = Initializers.FakeUserService(context);
            var employeeService = Initializers.FakeEmployeeService(context);
            var orderService = Initializers.FakeOrderService(context);
            var employee = Initializers.DefaultEmployeeProfile;
            var client = Initializers.DefaultUser;
            client.Email = "tesfd55@gmail.com";
            client = await userService.CreateAsync(client);
            employee.User = await userService.CreateAsync(employee.User);
            employee.UserId = employee.User.Id;
            employee = await employeeService.CreateEmployeeAsync(employee);

            // Action
            var order = new OrderDTO
            {
                Address = "ffd",
                BriefTask = "l",
                ClientId = client.Id,
                EmployeeId = employee.Id,
                ContactPhone = "124",
                Date = DateTime.Now,
                Price = 12m,
                ServiceDetails = "d"
            };
            var createdOrder = await orderService.CreateAsync(order);

            // Assert
            Assert.Equal(OrderStatus.WaitingForEmplpoyee, createdOrder.Status);
            Assert.Equal(order.Address, createdOrder.Address);
            Assert.Equal(order.BriefTask, createdOrder.BriefTask);
            Assert.Equal(order.ServiceDetails, createdOrder.ServiceDetails);
            Assert.Equal(order.Date, createdOrder.Date);
            Assert.Equal(order.ContactPhone, createdOrder.ContactPhone);
            Assert.Equal(order.Price, createdOrder.Price);
        }
    }
}
