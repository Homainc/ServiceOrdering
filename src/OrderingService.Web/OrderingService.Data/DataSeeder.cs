using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Code.Constants;
using OrderingService.Data.Models;

namespace OrderingService.Data
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();
            var admin = new User
            {
                Email = "spritefok@gmail.com",
                FirstName = "Shawn",
                LastName = "Wildermuth",
                PhoneNumber = "+37533655993",
                Id = Guid.NewGuid(),
                ImagePublicId = "default-employee",
                RoleId = RoleDefaults.AdminRoleId
            };
            admin.HashedPassword = hasher.HashPassword(admin, "123456@aA");
            builder.Entity<User>().HasData(admin);
        }
    }
}
