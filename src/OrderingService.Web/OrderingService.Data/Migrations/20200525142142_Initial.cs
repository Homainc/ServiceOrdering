using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderingService.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.UniqueConstraint("AK_Roles_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                    table.UniqueConstraint("AK_ServiceTypes_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    ImagePublicId = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    HashedPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_Email", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceTypeId = table.Column<int>(nullable: false),
                    ServiceCost = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    AverageRate = table.Column<double>(nullable: true),
                    ReviewsCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeProfiles_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(maxLength: 255, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    Rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_EmployeeProfiles_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    ServiceDetails = table.Column<string>(maxLength: 255, nullable: true),
                    BriefTask = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ContactPhone = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceOrders_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceOrders_EmployeeProfiles_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "user" },
                    { 2, "admin" }
                });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "it-specialist" },
                    { 2, "plumber" },
                    { 3, "guitarist" },
                    { 4, "mechanic" },
                    { 5, "teacher" },
                    { 6, "lawyer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "HashedPassword", "ImagePublicId", "LastName", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("c16c365e-6d32-49ef-bd4e-393f39e094e7"), "spritefok1@gmail.com", "Shawn", "AQAAAAEAACcQAAAAENJnS+mB9XUv6S9WVt0AtY/5/8gETZS0qOH2NrmFrfcQ0iWFxxbdCvCD/U7CA6v4AQ==", "estfjuxhdlgmfmnyartx", "Wildermuth", "+37533655993", 1 },
                    { new Guid("02d6ba1a-b720-4b25-a4f6-22ea23bec8a1"), "spritefok2@gmail.com", "Mike", "AQAAAAEAACcQAAAAEAb85gTVV/XMIGpaiMRpKliBzQniPtJI0WeYGrGMsXjTICTACywMtTtqnIJqGkxEhQ==", "estfjuxhdlgmfmnyartx", "Shinoda", "+37533636993", 1 },
                    { new Guid("1fc1558d-9d3f-40dc-a167-659d979312b4"), "spritefok3@gmail.com", "Chester", "AQAAAAEAACcQAAAAEIKWEnTX5Kb7+pk3Qp3/xzbzgPJLD+5mSX45ZZ+ZtlBLX69vRYkXcEE/2G4DJdVZbw==", "estfjuxhdlgmfmnyartx", "Bennington", "+37533636993", 1 },
                    { new Guid("6352141d-0bf6-43bd-b0d2-e59cb8d8eb51"), "spritefok4@gmail.com", "Philip", "AQAAAAEAACcQAAAAEL/Z1pdHmpO0jdlq/F89ZNGZb3ggIRsZFNTrDlOi+mPbR/vbU4gI8wUHnQZ/JQncmg==", "estfjuxhdlgmfmnyartx", "Khamitsevich", "+37533636993", 1 },
                    { new Guid("19d97a7d-f921-4ef0-b793-69a472ec3fe8"), "spritefok5@gmail.com", "Sam", "AQAAAAEAACcQAAAAEM8c7QRfNYx74BdZNYFNp5ZusQJRGvLXaWptKFbVeB0BAB8EOTRMjXEILwn2LUOivw==", "estfjuxhdlgmfmnyartx", "Robinson", "+37533636993", 1 },
                    { new Guid("7a677bc2-2353-4a8d-a5db-bf2f4a1d363c"), "spritefok6@gmail.com", "Kio", "AQAAAAEAACcQAAAAEKphy632WerhHjdw3R7vvdBYo1GcdbeCOE79pyuQKZRrEYTMGLJyfiVYiaJfu88YvA==", "estfjuxhdlgmfmnyartx", "Shima", "+37533636993", 1 },
                    { new Guid("b6b19251-9175-4c1c-972b-23b6e48fb4ee"), "spritefok7@gmail.com", "Yura", null, "estfjuxhdlgmfmnyartx", "Vasya", "+37533636993", 1 },
                    { new Guid("2b210afe-2f31-4405-98f7-54ff86dd7050"), "spritefok8@gmail.com", "Petya", null, "estfjuxhdlgmfmnyartx", "Jesus", "+37533636993", 1 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProfiles",
                columns: new[] { "Id", "AverageRate", "Description", "ReviewsCount", "ServiceCost", "ServiceTypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("c6d12c21-4549-4449-a6da-d839401e660d"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 55.65m, 1, new Guid("c16c365e-6d32-49ef-bd4e-393f39e094e7") },
                    { new Guid("c9eee64e-0b55-4b0b-bbf9-a5537b064862"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 100.12m, 2, new Guid("02d6ba1a-b720-4b25-a4f6-22ea23bec8a1") },
                    { new Guid("52d34401-3cf2-4c62-aef2-9e5f0a0868ed"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 5.93m, 3, new Guid("1fc1558d-9d3f-40dc-a167-659d979312b4") },
                    { new Guid("25cd173d-e771-4ef1-87f2-fe8b2361b7d6"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 25.65m, 4, new Guid("6352141d-0bf6-43bd-b0d2-e59cb8d8eb51") },
                    { new Guid("d170f8eb-d854-4905-92aa-489cd4cde041"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 75.3m, 5, new Guid("19d97a7d-f921-4ef0-b793-69a472ec3fe8") },
                    { new Guid("b42d1644-7871-478a-b50a-661b2f95fda9"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 143.4m, 6, new Guid("7a677bc2-2353-4a8d-a5db-bf2f4a1d363c") },
                    { new Guid("350a11f2-1388-4e47-82f1-e0cfb84dca86"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 45.3m, 6, new Guid("b6b19251-9175-4c1c-972b-23b6e48fb4ee") },
                    { new Guid("82c1075c-dafd-4716-b0e9-7979e492febe"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 84.94m, 6, new Guid("2b210afe-2f31-4405-98f7-54ff86dd7050") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfiles_ServiceTypeId",
                table: "EmployeeProfiles",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfiles_UserId",
                table: "EmployeeProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientId",
                table: "Reviews",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EmployeeId",
                table: "Reviews",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOrders_ClientId",
                table: "ServiceOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOrders_EmployeeId",
                table: "ServiceOrders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ServiceOrders");

            migrationBuilder.DropTable(
                name: "EmployeeProfiles");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
