using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Exceptions;
using Xunit;

namespace OrderingService.Logic.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void Can_create_user()
        {
            // Assign
            var user = Initializers.DefaultUser;
            using var context = Initializers.FakeContext("Can_create_user");

            // Action
            var service = Initializers.FakeUserService(context);
            var createdUser = service.CreateAsync(user).Result;

            // Assert
            Assert.Equal(user.Email, createdUser.Email);
            Assert.Equal("user", createdUser.Role);
        }

        [Fact]
        public void Can_auth_user()
        {
            // Assign
            var user = Initializers.DefaultUser;
            using var context = Initializers.FakeContext("Can_auth_user");

            // Action
            var service = Initializers.FakeUserService(context);
            service.CreateAsync(user).Wait();
            var createdUser = service.AuthenticateAsync(user).Result;

            // Assert
            Assert.NotNull(createdUser.Token);
        }

        [Fact]
        public async void Can_not_create_user_with_same_email()
        {
            // Assign
            using var context = Initializers.FakeContext("Can_not_create_user_with_same_email");
            var user = Initializers.DefaultUser;
            var sameEmailUser = new UserDTO
            {
                FirstName = "tes",
                LastName = "sss",
                Password = "test1_pwD1@",
                PhoneNumber = "+121137890",
                Role = "user",
                Email = user.Email
            };

            // Assert
            await Assert.ThrowsAsync<LogicException>(async () => {
                var service = Initializers.FakeUserService(context);
                await service.CreateAsync(user);
                await service.CreateAsync(sameEmailUser);
            });
        }

        [Fact]
        public void Can_update_user()
        {
            // Assign
            const string dbName = "Can_update_user";
            var user = Initializers.DefaultUser;
            using(var context = Initializers.FakeContext(dbName)){
                var service = Initializers.FakeUserService(context);
                user = service.CreateAsync(user).Result;
            }

            // Action
            user.FirstName = "new first name";
            user.LastName = "new last name";
            user.ImageUrl = "new image url";
            user.Email = "email@new.com";
            UserDTO updatedUser;
            using(var context = Initializers.FakeContext(dbName)){
                var service = Initializers.FakeUserService(context);
                updatedUser = service.UpdateProfileAsync(user).Result;
            }

            // Assert
            Assert.Equal(user.Email, updatedUser.Email);
            Assert.Equal(user.LastName, updatedUser.LastName);
            Assert.Equal(user.FirstName, updatedUser.FirstName);
            Assert.Equal(user.ImageUrl, updatedUser.ImageUrl);
        }
    }
}
