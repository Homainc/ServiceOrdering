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

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.UserProfile)
                .WithOne(u => u.User)
                .HasForeignKey<UserProfile>(u => u.UserId);
            builder
                .HasOne(u => u.EmployeeProfile)
                .WithOne(e => e.User)
                .HasForeignKey<EmployeeProfile>(e => e.UserId);
        }
    }
}
