﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderingService.Data.EF;

namespace OrderingService.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20200410144506_AddCachedValuesToEmployeeProfile")]
    partial class AddCachedValuesToEmployeeProfile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderingService.Data.Models.EmployeeProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("AverageRate")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("ReviewsCount")
                        .HasColumnType("int");

                    b.Property<decimal>("ServiceCost")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("ServiceTypeId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ServiceTypeId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("EmployeeProfiles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3e6ede08-a083-4e27-badb-29a6df07117b"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 55.65m,
                            ServiceTypeId = 1,
                            UserId = new Guid("5a73b1df-4de5-45b0-9b57-85029d6c15c8")
                        },
                        new
                        {
                            Id = new Guid("c46607e3-b8e2-4661-838c-517bd8b2e05f"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 100.12m,
                            ServiceTypeId = 2,
                            UserId = new Guid("f2f0fb2e-5d78-442b-9bf8-fdbd324d7949")
                        },
                        new
                        {
                            Id = new Guid("f97ab19b-7166-4386-b7cb-09cf9c1ae89d"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 5.93m,
                            ServiceTypeId = 3,
                            UserId = new Guid("ecf42233-6f81-4cbf-a188-fb17a0c2b7f9")
                        },
                        new
                        {
                            Id = new Guid("6e9019b3-c970-4c3b-ba29-4c103f729032"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 25.65m,
                            ServiceTypeId = 4,
                            UserId = new Guid("e63aa3cc-4e68-4ff9-b54c-ecea9cb7decd")
                        },
                        new
                        {
                            Id = new Guid("51134c3e-58c6-4ee5-b763-09e24949a829"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 75.3m,
                            ServiceTypeId = 5,
                            UserId = new Guid("f292f5ac-0bcc-46b1-b23e-66892ecf60ca")
                        },
                        new
                        {
                            Id = new Guid("4b6bd398-f1e7-4fbc-a109-c408a331fc2f"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 143.4m,
                            ServiceTypeId = 6,
                            UserId = new Guid("4a212982-6a7d-4312-900d-32c168f760e4")
                        },
                        new
                        {
                            Id = new Guid("f1ef3656-8cad-4f97-9cd6-f89d5ed8729c"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 45.3m,
                            ServiceTypeId = 6,
                            UserId = new Guid("9206f461-5b66-40b6-84c5-78fc7a5fdbca")
                        },
                        new
                        {
                            Id = new Guid("49e3225e-d527-45f3-9c85-0b41c9a50656"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 84.94m,
                            ServiceTypeId = 6,
                            UserId = new Guid("cc4d48e1-97b8-471c-a455-a8b5f9426018")
                        });
                });

            modelBuilder.Entity("OrderingService.Data.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("OrderingService.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "user"
                        },
                        new
                        {
                            Id = 2,
                            Name = "admin"
                        });
                });

            modelBuilder.Entity("OrderingService.Data.Models.ServiceOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BriefTask")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("ServiceDetails")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ServiceOrders");
                });

            modelBuilder.Entity("OrderingService.Data.Models.ServiceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("ServiceTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "it-specialist"
                        },
                        new
                        {
                            Id = 2,
                            Name = "plumber"
                        },
                        new
                        {
                            Id = 3,
                            Name = "guitarist"
                        },
                        new
                        {
                            Id = 4,
                            Name = "mechanic"
                        },
                        new
                        {
                            Id = 5,
                            Name = "teacher"
                        },
                        new
                        {
                            Id = 6,
                            Name = "lawyer"
                        });
                });

            modelBuilder.Entity("OrderingService.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5a73b1df-4de5-45b0-9b57-85029d6c15c8"),
                            Email = "spritefok1@gmail.com",
                            FirstName = "Shawn",
                            HashedPassword = "AQAAAAEAACcQAAAAEEwKCTa3AIXjFvV7Cjmx0WNTcwc03b0gOOwt7G/8sjNbEdqKU9JQ8fyQ9BSlRfSBMQ==",
                            ImageUrl = "https://wildermuth.com/img/shawn-head.gif",
                            LastName = "Wildermuth",
                            PhoneNumber = "+37533655993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("f2f0fb2e-5d78-442b-9bf8-fdbd324d7949"),
                            Email = "spritefok2@gmail.com",
                            FirstName = "Mike",
                            HashedPassword = "AQAAAAEAACcQAAAAEMI4nOdToYLDZSDXCMMYy1KY1uWsZEVjpdKwb+oi6BnhkLPp+JWbtzhwYRyyXgF5Qg==",
                            ImageUrl = "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg",
                            LastName = "Shinoda",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("ecf42233-6f81-4cbf-a188-fb17a0c2b7f9"),
                            Email = "spritefok3@gmail.com",
                            FirstName = "Chester",
                            HashedPassword = "AQAAAAEAACcQAAAAEPUZO6usJsdpICuW8vJLnAQoUGx8j+WXWMSgLvrDeenlfyjlbTowOdkhaSKZGZYCgQ==",
                            ImageUrl = "https://wildermuth.com/img/shawn-head.gif",
                            LastName = "Bennington",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("e63aa3cc-4e68-4ff9-b54c-ecea9cb7decd"),
                            Email = "spritefok4@gmail.com",
                            FirstName = "Philip",
                            HashedPassword = "AQAAAAEAACcQAAAAECP0xwuAudzBuTGPWWRbX1qIyLojnnC9j5yTZF5Reanm8hld4mlGh1dtlnERwSmrEg==",
                            ImageUrl = "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg",
                            LastName = "Khamitsevich",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("f292f5ac-0bcc-46b1-b23e-66892ecf60ca"),
                            Email = "spritefok5@gmail.com",
                            FirstName = "Sam",
                            HashedPassword = "AQAAAAEAACcQAAAAECFKDJCVG5AN7MSXEXHAJUgbG4auuK/FHgsFd8f+qRfBYIW3Vi2DNJrxKRQS4B7yng==",
                            ImageUrl = "https://wildermuth.com/img/shawn-head.gif",
                            LastName = "Robinson",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("4a212982-6a7d-4312-900d-32c168f760e4"),
                            Email = "spritefok6@gmail.com",
                            FirstName = "Kio",
                            HashedPassword = "AQAAAAEAACcQAAAAEEY1zOGjYkBq3NYVU3P109Q9OfoJZXRkVnVCgunhozAiPBrENMhssGZl+cHBxJzNwQ==",
                            ImageUrl = "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg",
                            LastName = "Shima",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("9206f461-5b66-40b6-84c5-78fc7a5fdbca"),
                            Email = "spritefok7@gmail.com",
                            FirstName = "Yura",
                            ImageUrl = "https://wildermuth.com/img/shawn-head.gif",
                            LastName = "Vasya",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("cc4d48e1-97b8-471c-a455-a8b5f9426018"),
                            Email = "spritefok8@gmail.com",
                            FirstName = "Petya",
                            ImageUrl = "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg",
                            LastName = "Jesus",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("OrderingService.Data.Models.EmployeeProfile", b =>
                {
                    b.HasOne("OrderingService.Data.Models.ServiceType", "ServiceType")
                        .WithMany()
                        .HasForeignKey("ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderingService.Data.Models.User", "User")
                        .WithOne("EmployeeProfile")
                        .HasForeignKey("OrderingService.Data.Models.EmployeeProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrderingService.Data.Models.Review", b =>
                {
                    b.HasOne("OrderingService.Data.Models.User", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .IsRequired();

                    b.HasOne("OrderingService.Data.Models.EmployeeProfile", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderingService.Data.Models.ServiceOrder", b =>
                {
                    b.HasOne("OrderingService.Data.Models.User", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OrderingService.Data.Models.EmployeeProfile", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrderingService.Data.Models.User", b =>
                {
                    b.HasOne("OrderingService.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}