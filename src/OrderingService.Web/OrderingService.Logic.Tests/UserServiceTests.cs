using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;
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
            IResult<UserDTO> result;

            // Action
            using (var service = Initializers.FakeUserService("Can_create_user"))
            {
                result = service.CreateAsync(user, default).Result;
            }

            // Assert
            Assert.False(result.DidError);
            Assert.Equal(user.Email, result.Value.Email);
            Assert.Equal("user", result.Value.Role);
        }

        [Fact]
        public void Can_auth_user()
        {
            // Assign
            IResult<UserDTO> result;
            var user = Initializers.DefaultUser;

            // Action
            using (var service = Initializers.FakeUserService("Can_auth_user"))
            {
                service.CreateAsync(user, default).Wait();
                result = service.AuthenticateAsync(user, default).Result;
            }

            // Assert
            Assert.False(result.DidError);
            Assert.NotNull(result.Value.Token);
        }

        [Fact]
        public void Can_not_create_user_with_same_email()
        {
            // Assign
            IResult<UserDTO> result;
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

            // Action
            using(var service = Initializers.FakeUserService("Can_not_create_user_with_same_email"))
            {
                service.CreateAsync(user, default).Wait();
                result = service.CreateAsync(sameEmailUser, default).Result;
            }

            // Assert
            Assert.True(result.DidError);
        }

        [Fact]
        public void Can_update_user()
        {
            // Assign
            const string dbName = "Can_update_user";
            var user = Initializers.DefaultUser;
            IResult<UserDTO> result;
            using (var service = Initializers.FakeUserService(dbName))
            {
                result = service.CreateAsync(user, default).Result;
            }
            user = result.Value;

            // Action
            using (var service = Initializers.FakeUserService(dbName))
            {
                user.FirstName = "new first name";
                user.LastName = "new last name";
                user.ImageUrl = "new image url";
                user.Email = "email@new.com";
                result = service.UpdateProfileAsync(user, default).Result;
            }

            // Assert
            Assert.False(result.DidError);
            Assert.Equal(user.Email, result.Value.Email);
            Assert.Equal(user.LastName, result.Value.LastName);
            Assert.Equal(user.FirstName, result.Value.FirstName);
            Assert.Equal(user.ImageUrl, result.Value.ImageUrl);
        }
    }
}
