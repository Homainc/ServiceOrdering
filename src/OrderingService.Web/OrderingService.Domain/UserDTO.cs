using System;

namespace OrderingService.Domain
{
    public class UserDtoBase
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
    }

    public class UserCreateDto : UserDtoBase
    {
        public string Password { get; set; }
    }

    public class UserDto : UserDtoBase
    {
        public Guid Id { get; set; }
        public EmployeeProfileDto EmployeeProfile { get; set; }
    }

    public class UserAuthDto : UserDto
    {
        public string Token { get; set; }
    }
}
