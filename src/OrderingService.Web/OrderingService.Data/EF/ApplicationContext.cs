﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Models;

namespace OrderingService.Data.EF
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var roles = new IdentityRole[]
            {
                new IdentityRole { Name = "USER" , NormalizedName = "USER" },
                new IdentityRole { Name = "ADMIN", NormalizedName = "ADMIN" },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}