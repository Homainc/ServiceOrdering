using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Models;

namespace OrderingService.Data
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();
            var serviceTypes = new []
            {
                new ServiceType
                {
                    Id = 1,
                    Name = "it-specialist",
                },
                new ServiceType
                {
                    Id = 2,
                    Name = "plumber",
                },
                new ServiceType
                {
                    Id = 3,
                    Name = "guitarist",
                },
                new ServiceType
                {
                    Id = 4,
                    Name = "mechanic",
                },
                new ServiceType
                {
                    Id = 5,
                    Name = "teacher",
                },
                new ServiceType
                {
                    Id = 6,
                    Name = "lawyer",
                }
            };
            var users = new []
            {
                new User
                {
                    Email = "spritefok1@gmail.com",
                    FirstName = "Shawn",
                    LastName = "Wildermuth",
                    PhoneNumber = "+37533655993",
                    Id = Guid.NewGuid(),
                    ImagePublicId = "estfjuxhdlgmfmnyartx",
                    RoleId = 1
                },
                new User
                {
                    Email = "spritefok2@gmail.com",
                    FirstName = "Mike",
                    LastName = "Shinoda",
                    PhoneNumber = "+37533636993",
                    Id = Guid.NewGuid(),
                    ImagePublicId = "estfjuxhdlgmfmnyartx",
                    RoleId = 1
                },
                new User
                {
                    Email = "spritefok3@gmail.com",
                    FirstName = "Chester",
                    LastName = "Bennington",
                    PhoneNumber = "+37533636993",
                    Id = Guid.NewGuid(),
                    ImagePublicId = "estfjuxhdlgmfmnyartx",
                    RoleId = 1
                },
                new User
                {
                    Email = "spritefok4@gmail.com",
                    FirstName = "Philip",
                    LastName = "Khamitsevich",
                    PhoneNumber = "+37533636993",
                    Id = Guid.NewGuid(),
                    ImagePublicId = "estfjuxhdlgmfmnyartx",
                    RoleId = 1
                },
                new User
                {
                    Email = "spritefok5@gmail.com",
                    FirstName = "Sam",
                    LastName = "Robinson",
                    PhoneNumber = "+37533636993",
                    Id = Guid.NewGuid(),
                    ImagePublicId = "estfjuxhdlgmfmnyartx",
                    RoleId = 1
                },
                new User
                {
                    Email = "spritefok6@gmail.com",
                    FirstName = "Kio",
                    LastName = "Shima",
                    PhoneNumber = "+37533636993",
                    Id = Guid.NewGuid(),
                    ImagePublicId = "estfjuxhdlgmfmnyartx",
                    RoleId = 1
                },
                new User
                {
                    Email = "spritefok7@gmail.com",
                    FirstName = "Yura",
                    LastName = "Vasya",
                    PhoneNumber = "+37533636993",
                    Id = Guid.NewGuid(),
                    ImagePublicId = "estfjuxhdlgmfmnyartx",
                    RoleId = 1
                },
                new User
                {
                    Email = "spritefok8@gmail.com",
                    FirstName = "Petya",
                    LastName = "Jesus",
                    PhoneNumber = "+37533636993",
                    Id = Guid.NewGuid(),
                    ImagePublicId = "estfjuxhdlgmfmnyartx",
                    RoleId = 1
                }
            };
            for (int i = 0; i < 6; i++)
            {
                users[i].HashedPassword = hasher.HashPassword(users[i], "123456@aA");
                users[i].Role = null;
            }

            builder.Entity<ServiceType>().HasData(serviceTypes);
            builder.Entity<User>().HasData(users);
            builder.Entity<EmployeeProfile>().HasData(
                new
                {
                    Id = Guid.NewGuid(),
                    Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                    ServiceCost = 55.65m,
                    ServiceTypeId = serviceTypes[0].Id,
                    UserId = users[0].Id,
                    ReviewsCount = 0
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                    ServiceCost = 100.12m,
                    ServiceTypeId = serviceTypes[1].Id,
                    UserId = users[1].Id,
                    ReviewsCount = 0
                     
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                    ServiceCost = 5.93m,
                    ServiceTypeId = serviceTypes[2].Id,
                    UserId = users[2].Id,
                    ReviewsCount = 0
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                    ServiceCost = 25.65m,
                    ServiceTypeId = serviceTypes[3].Id,
                    UserId = users[3].Id,
                    ReviewsCount = 0
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                    ServiceCost = 75.3m,
                    ServiceTypeId = serviceTypes[4].Id,
                    UserId = users[4].Id,
                    ReviewsCount = 0
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                    ServiceCost = 143.4m,
                    ServiceTypeId = serviceTypes[5].Id,
                    UserId = users[5].Id,
                    ReviewsCount = 0
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                    ServiceCost = 45.3m,
                    ServiceTypeId = serviceTypes[5].Id,
                    UserId = users[6].Id,
                    ReviewsCount = 0
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                    ServiceCost = 84.94m,
                    ServiceTypeId = serviceTypes[5].Id,
                    UserId = users[7].Id,
                    ReviewsCount = 0
                });
        }
    }
}
