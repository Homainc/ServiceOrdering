﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderingService.Data.EF;

namespace OrderingService.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

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
                            Id = new Guid("068225e8-34ba-4226-bd0b-66ec5c9e6fa3"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ServiceCost = 55.65m,
                            ServiceTypeId = 1,
                            UserId = new Guid("5f72f51d-4ab1-4c81-9bc4-e8f90e31d539")
                        },
                        new
                        {
                            Id = new Guid("bedc4a7d-548e-4b49-a819-288ff60004eb"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ServiceCost = 100.12m,
                            ServiceTypeId = 2,
                            UserId = new Guid("e9a54770-1a4b-4683-89fe-b137b26e0fce")
                        },
                        new
                        {
                            Id = new Guid("766ff5ec-dcfb-4454-8bf1-c802c556482b"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ServiceCost = 5.93m,
                            ServiceTypeId = 3,
                            UserId = new Guid("b7ca5d21-5c9d-48bc-8b80-de97669335c6")
                        },
                        new
                        {
                            Id = new Guid("998be124-11e3-424f-ad4c-8ff455dcba19"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ServiceCost = 25.65m,
                            ServiceTypeId = 4,
                            UserId = new Guid("74ad850e-508e-4e2a-95e1-cbd6fdba3cef")
                        },
                        new
                        {
                            Id = new Guid("70c1e72f-2c0f-4c32-a7d8-4213901f9d97"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ServiceCost = 75.3m,
                            ServiceTypeId = 5,
                            UserId = new Guid("b61764cd-adeb-41b4-82b9-ada7c3c07188")
                        },
                        new
                        {
                            Id = new Guid("15cb790d-99ac-43ce-a25a-ac505f76bc53"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ServiceCost = 143.4m,
                            ServiceTypeId = 6,
                            UserId = new Guid("ad6c3763-f62a-4649-aecd-b2f61766b6bb")
                        },
                        new
                        {
                            Id = new Guid("6eecbaa0-47b7-4bd7-ad5b-8720c80eb7db"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ServiceCost = 45.3m,
                            ServiceTypeId = 6,
                            UserId = new Guid("57877578-6c78-4c75-9443-86e0d738036c")
                        },
                        new
                        {
                            Id = new Guid("dffeefd9-f7f7-4ffe-add0-8f1173cd8e58"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ServiceCost = 84.94m,
                            ServiceTypeId = 6,
                            UserId = new Guid("c4b50715-54c1-4570-aed0-a7f4831d2edd")
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
                            Id = new Guid("5f72f51d-4ab1-4c81-9bc4-e8f90e31d539"),
                            Email = "spritefok1@gmail.com",
                            FirstName = "Shawn",
                            HashedPassword = "AQAAAAEAACcQAAAAEPLxsUDahY37ru5M9YqM8oH0Rmm73aLSaqe2zG25nLfBWypEEaZUSRK7D/iovM90Mw==",
                            ImageUrl = "https://wildermuth.com/img/shawn-head.gif",
                            LastName = "Wildermuth",
                            PhoneNumber = "+37533655993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("e9a54770-1a4b-4683-89fe-b137b26e0fce"),
                            Email = "spritefok2@gmail.com",
                            FirstName = "Mike",
                            HashedPassword = "AQAAAAEAACcQAAAAEAW0s6//nDygUe+SVmGwBNNTfYPSpJx2DB0AYsA1jroSYGzT6NVLIQAAswYv9uAYnQ==",
                            ImageUrl = "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg",
                            LastName = "Shinoda",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("b7ca5d21-5c9d-48bc-8b80-de97669335c6"),
                            Email = "spritefok3@gmail.com",
                            FirstName = "Chester",
                            HashedPassword = "AQAAAAEAACcQAAAAECd0vID3gcXn5ajHMnuwY3pTN9n8CFk1+/gfmHsYy/QE0XOY+RFsCQTYjIaXH1FKmQ==",
                            ImageUrl = "https://wildermuth.com/img/shawn-head.gif",
                            LastName = "Bennington",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("74ad850e-508e-4e2a-95e1-cbd6fdba3cef"),
                            Email = "spritefok4@gmail.com",
                            FirstName = "Philip",
                            HashedPassword = "AQAAAAEAACcQAAAAEGa6B9gZzPOtkMh+aVHAHARMy2+WeV7193+5kYtExCBMw9i/nGbvYXXqVxw2tjCy2A==",
                            ImageUrl = "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg",
                            LastName = "Khamitsevich",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("b61764cd-adeb-41b4-82b9-ada7c3c07188"),
                            Email = "spritefok5@gmail.com",
                            FirstName = "Sam",
                            HashedPassword = "AQAAAAEAACcQAAAAEK8C29eOPjIZFVAvG1oHlrt6oNQ9KgxbnxpiAKGZ5RrJRDqWcF0pFOY/ShP/Eq2cUA==",
                            ImageUrl = "https://wildermuth.com/img/shawn-head.gif",
                            LastName = "Robinson",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("ad6c3763-f62a-4649-aecd-b2f61766b6bb"),
                            Email = "spritefok6@gmail.com",
                            FirstName = "Kio",
                            HashedPassword = "AQAAAAEAACcQAAAAEBQ99L4ai5stlDCCyt5m2gDoFcQmnbUJs1yiF0luUHIQtalNorOPB//5awbAHMmqTQ==",
                            ImageUrl = "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg",
                            LastName = "Shima",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("57877578-6c78-4c75-9443-86e0d738036c"),
                            Email = "spritefok7@gmail.com",
                            FirstName = "Yura",
                            ImageUrl = "https://wildermuth.com/img/shawn-head.gif",
                            LastName = "Vasya",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("c4b50715-54c1-4570-aed0-a7f4831d2edd"),
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
