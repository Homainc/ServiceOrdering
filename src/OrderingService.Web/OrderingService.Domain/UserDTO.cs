using System;
namespace OrderingService.Domain
{
    public class UserDTO
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public  string ImageUrl { get; set; }
        public EmployeeProfileDTO EmployeeProfile { get; set; }
    }
}
