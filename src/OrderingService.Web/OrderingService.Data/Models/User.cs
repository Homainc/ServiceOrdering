using Microsoft.AspNetCore.Identity;

namespace OrderingService.Data.Models
{
    public class User : IdentityUser
    {
        public UserProfile UserProfile { get; set; }
        public EmployeeProfile EmployeeProfile { get; set; }
    }
}
