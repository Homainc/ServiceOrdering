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
                    ImagePublicId = table.Column<string>(maxLength: 30, nullable: true),
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
                    { new Guid("07fb6c39-35fa-4f31-91f0-a83a0198b0f1"), "spritefok1@gmail.com", "Shawn", "AQAAAAEAACcQAAAAEE9uPJ9Fye+dO5EUHAjutsFrfTLEgMn5Z0vySx71oiKIhSaodzI7ZAOSehJ/sskkEQ==", "estfjuxhdlgmfmnyartx", "Wildermuth", "+37533655993", 1 },
                    { new Guid("582a2200-b1b1-4de4-a806-702ad6a60379"), "spritefok2@gmail.com", "Mike", "AQAAAAEAACcQAAAAEF2d7fAFuSuRtaRws6qDZ0mePzHBPMx1Gk3JhmJsUJl4pXOMlbGqKcv4AI9Z6nUK0w==", "estfjuxhdlgmfmnyartx", "Shinoda", "+37533636993", 1 },
                    { new Guid("db94c1b9-1a1f-4b72-934e-8ab4ead63561"), "spritefok3@gmail.com", "Chester", "AQAAAAEAACcQAAAAEKjNqnTlkEbVXyFMwejhn7Q/JeusX4dy7yjg9UM1ZZ2dfHL5AplQKgFaDHPbFDDpLQ==", "estfjuxhdlgmfmnyartx", "Bennington", "+37533636993", 1 },
                    { new Guid("074306d5-d3cf-44cc-a425-084119128280"), "spritefok4@gmail.com", "Philip", "AQAAAAEAACcQAAAAEElwW+AU6Wb4ZE/ypp71JG82v57A3jNv4QcvWLcoyFUj78EuJ+DLTFnnC8vbHmAL3g==", "estfjuxhdlgmfmnyartx", "Khamitsevich", "+37533636993", 1 },
                    { new Guid("4d6fe70c-ebff-41a9-843c-d7d702ebeed9"), "spritefok5@gmail.com", "Sam", "AQAAAAEAACcQAAAAEDWgvHuiO/++i1W8uAb5x0LwZBdhaytO5gUDTexgqy4Ga7q5mRaH2y5WVjG1AaRflw==", "estfjuxhdlgmfmnyartx", "Robinson", "+37533636993", 1 },
                    { new Guid("498ba669-7677-49fc-a7aa-b91e458ae7a7"), "spritefok6@gmail.com", "Kio", "AQAAAAEAACcQAAAAEAe0KV54J4OBWb1rkRHXSzsLSNZNs/lA0Udj6vmXzAbSwWlBP2OS2bFnYXmOgACa5w==", "estfjuxhdlgmfmnyartx", "Shima", "+37533636993", 1 },
                    { new Guid("12c957fc-8a20-49a9-8a34-133f03fd88d1"), "spritefok7@gmail.com", "Yura", null, "estfjuxhdlgmfmnyartx", "Vasya", "+37533636993", 1 },
                    { new Guid("d7c086ea-819b-4102-aa9e-e1c4be3e5635"), "spritefok8@gmail.com", "Petya", null, "estfjuxhdlgmfmnyartx", "Jesus", "+37533636993", 1 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProfiles",
                columns: new[] { "Id", "AverageRate", "Description", "ReviewsCount", "ServiceCost", "ServiceTypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("6b55069e-4cc8-4fd7-b397-e405e89789bf"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 55.65m, 1, new Guid("07fb6c39-35fa-4f31-91f0-a83a0198b0f1") },
                    { new Guid("6712dae2-24bf-44a7-b614-c37b7866981e"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 100.12m, 2, new Guid("582a2200-b1b1-4de4-a806-702ad6a60379") },
                    { new Guid("ef620f01-b410-435a-80db-7099637ef324"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 5.93m, 3, new Guid("db94c1b9-1a1f-4b72-934e-8ab4ead63561") },
                    { new Guid("a4ee2e89-2955-4273-b401-d952eb2f319b"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 25.65m, 4, new Guid("074306d5-d3cf-44cc-a425-084119128280") },
                    { new Guid("6f111764-0acc-4ab5-b4d8-d1a0185b8910"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 75.3m, 5, new Guid("4d6fe70c-ebff-41a9-843c-d7d702ebeed9") },
                    { new Guid("fce2f323-4375-4106-a2c5-ec0ba8796b2d"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 143.4m, 6, new Guid("498ba669-7677-49fc-a7aa-b91e458ae7a7") },
                    { new Guid("2b542293-0814-4d87-ab14-2548818078ef"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 45.3m, 6, new Guid("12c957fc-8a20-49a9-8a34-133f03fd88d1") },
                    { new Guid("fb600e52-4bbc-4161-adc0-57d79f0880d6"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 84.94m, 6, new Guid("d7c086ea-819b-4102-aa9e-e1c4be3e5635") }
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
