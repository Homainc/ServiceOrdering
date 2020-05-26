using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace OrderingService.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public  int RoleId { get; set; }
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePublicId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string HashedPassword { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public EmployeeProfile EmployeeProfile { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>{
        public void Configure(EntityTypeBuilder<User> builder){
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName)
                .IsRequired().HasMaxLength(30);
            builder.Property(x => x.LastName)
                .IsRequired().HasMaxLength(30);
            builder.Property(x => x.ImagePublicId)
                .HasMaxLength(50)
                .IsRequired(false);
            builder.HasAlternateKey(x => x.Email);
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.HasOne(x => x.EmployeeProfile)
                .WithOne(x => x.User)
                .HasForeignKey<EmployeeProfile>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.RefreshToken)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
