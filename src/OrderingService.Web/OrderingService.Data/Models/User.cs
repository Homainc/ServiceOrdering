using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderingService.Data.Models
{
    public class User : IdentityUser
    {
        public UserProfile UserProfile { get; set; }
        public EmployeeProfile EmployeeProfile { get; set; }
    }
}
