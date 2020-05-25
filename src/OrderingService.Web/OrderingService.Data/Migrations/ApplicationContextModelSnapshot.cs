﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderingService.Data;

namespace OrderingService.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
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
                            Id = new Guid("c6d12c21-4549-4449-a6da-d839401e660d"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 55.65m,
                            ServiceTypeId = 1,
                            UserId = new Guid("c16c365e-6d32-49ef-bd4e-393f39e094e7")
                        },
                        new
                        {
                            Id = new Guid("c9eee64e-0b55-4b0b-bbf9-a5537b064862"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 100.12m,
                            ServiceTypeId = 2,
                            UserId = new Guid("02d6ba1a-b720-4b25-a4f6-22ea23bec8a1")
                        },
                        new
                        {
                            Id = new Guid("52d34401-3cf2-4c62-aef2-9e5f0a0868ed"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 5.93m,
                            ServiceTypeId = 3,
                            UserId = new Guid("1fc1558d-9d3f-40dc-a167-659d979312b4")
                        },
                        new
                        {
                            Id = new Guid("25cd173d-e771-4ef1-87f2-fe8b2361b7d6"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 25.65m,
                            ServiceTypeId = 4,
                            UserId = new Guid("6352141d-0bf6-43bd-b0d2-e59cb8d8eb51")
                        },
                        new
                        {
                            Id = new Guid("d170f8eb-d854-4905-92aa-489cd4cde041"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 75.3m,
                            ServiceTypeId = 5,
                            UserId = new Guid("19d97a7d-f921-4ef0-b793-69a472ec3fe8")
                        },
                        new
                        {
                            Id = new Guid("b42d1644-7871-478a-b50a-661b2f95fda9"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 143.4m,
                            ServiceTypeId = 6,
                            UserId = new Guid("7a677bc2-2353-4a8d-a5db-bf2f4a1d363c")
                        },
                        new
                        {
                            Id = new Guid("350a11f2-1388-4e47-82f1-e0cfb84dca86"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 45.3m,
                            ServiceTypeId = 6,
                            UserId = new Guid("b6b19251-9175-4c1c-972b-23b6e48fb4ee")
                        },
                        new
                        {
                            Id = new Guid("82c1075c-dafd-4716-b0e9-7979e492febe"),
                            Description = "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.",
                            ReviewsCount = 0,
                            ServiceCost = 84.94m,
                            ServiceTypeId = 6,
                            UserId = new Guid("2b210afe-2f31-4405-98f7-54ff86dd7050")
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

                    b.Property<string>("ImagePublicId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

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
                            Id = new Guid("c16c365e-6d32-49ef-bd4e-393f39e094e7"),
                            Email = "spritefok1@gmail.com",
                            FirstName = "Shawn",
                            HashedPassword = "AQAAAAEAACcQAAAAENJnS+mB9XUv6S9WVt0AtY/5/8gETZS0qOH2NrmFrfcQ0iWFxxbdCvCD/U7CA6v4AQ==",
                            ImagePublicId = "estfjuxhdlgmfmnyartx",
                            LastName = "Wildermuth",
                            PhoneNumber = "+37533655993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("02d6ba1a-b720-4b25-a4f6-22ea23bec8a1"),
                            Email = "spritefok2@gmail.com",
                            FirstName = "Mike",
                            HashedPassword = "AQAAAAEAACcQAAAAEAb85gTVV/XMIGpaiMRpKliBzQniPtJI0WeYGrGMsXjTICTACywMtTtqnIJqGkxEhQ==",
                            ImagePublicId = "estfjuxhdlgmfmnyartx",
                            LastName = "Shinoda",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("1fc1558d-9d3f-40dc-a167-659d979312b4"),
                            Email = "spritefok3@gmail.com",
                            FirstName = "Chester",
                            HashedPassword = "AQAAAAEAACcQAAAAEIKWEnTX5Kb7+pk3Qp3/xzbzgPJLD+5mSX45ZZ+ZtlBLX69vRYkXcEE/2G4DJdVZbw==",
                            ImagePublicId = "estfjuxhdlgmfmnyartx",
                            LastName = "Bennington",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("6352141d-0bf6-43bd-b0d2-e59cb8d8eb51"),
                            Email = "spritefok4@gmail.com",
                            FirstName = "Philip",
                            HashedPassword = "AQAAAAEAACcQAAAAEL/Z1pdHmpO0jdlq/F89ZNGZb3ggIRsZFNTrDlOi+mPbR/vbU4gI8wUHnQZ/JQncmg==",
                            ImagePublicId = "estfjuxhdlgmfmnyartx",
                            LastName = "Khamitsevich",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("19d97a7d-f921-4ef0-b793-69a472ec3fe8"),
                            Email = "spritefok5@gmail.com",
                            FirstName = "Sam",
                            HashedPassword = "AQAAAAEAACcQAAAAEM8c7QRfNYx74BdZNYFNp5ZusQJRGvLXaWptKFbVeB0BAB8EOTRMjXEILwn2LUOivw==",
                            ImagePublicId = "estfjuxhdlgmfmnyartx",
                            LastName = "Robinson",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("7a677bc2-2353-4a8d-a5db-bf2f4a1d363c"),
                            Email = "spritefok6@gmail.com",
                            FirstName = "Kio",
                            HashedPassword = "AQAAAAEAACcQAAAAEKphy632WerhHjdw3R7vvdBYo1GcdbeCOE79pyuQKZRrEYTMGLJyfiVYiaJfu88YvA==",
                            ImagePublicId = "estfjuxhdlgmfmnyartx",
                            LastName = "Shima",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("b6b19251-9175-4c1c-972b-23b6e48fb4ee"),
                            Email = "spritefok7@gmail.com",
                            FirstName = "Yura",
                            ImagePublicId = "estfjuxhdlgmfmnyartx",
                            LastName = "Vasya",
                            PhoneNumber = "+37533636993",
                            RoleId = 1
                        },
                        new
                        {
                            Id = new Guid("2b210afe-2f31-4405-98f7-54ff86dd7050"),
                            Email = "spritefok8@gmail.com",
                            FirstName = "Petya",
                            ImagePublicId = "estfjuxhdlgmfmnyartx",
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
