using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderingService.Data.Migrations
{
    public partial class InitialState : Migration
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
                    ImageUrl = table.Column<string>(maxLength: 200, nullable: true),
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
                    ServiceTypeId = table.Column<int>(nullable: true),
                    ServiceCost = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeProfiles_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    EmployeeId = table.Column<Guid>(nullable: false)
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
                        name: "FK_Reviews_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false)
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
                        name: "FK_ServiceOrders_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                columns: new[] { "Id", "Email", "FirstName", "HashedPassword", "ImageUrl", "LastName", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("8574e5bb-49f5-453d-aaa9-8a4bc6c6625f"), "spritefok1@gmail.com", "Shawn", "AQAAAAEAACcQAAAAEHfyUuzPJckcFaNeerprpJFKtqeeRU24b/hG4hRpn2N7tnPv8EGmc0x4hdGDfhClHg==", "https://wildermuth.com/img/shawn-head.gif", "Wildermuth", "+37533655993", 1 },
                    { new Guid("c7b4b5d6-bbb4-4079-9792-ec28a4988d66"), "spritefok2@gmail.com", "Mike", "AQAAAAEAACcQAAAAEPcnlJfbE2Ophl+GwXQWB9hp/oIKXeRrgzTtwdOnu6vMmyPj0NpU/xKBl4Oca6tSMQ==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shinoda", "+37533636993", 1 },
                    { new Guid("49c2c66a-2e92-48d7-b0de-9a2a278dac06"), "spritefok3@gmail.com", "Chester", "AQAAAAEAACcQAAAAECQhE+TeCcXzSWaOv18JSObh3hrNNDvHo4+Qp/TbiP2+FPzo9bNu2N1HkNwURROfDg==", "https://wildermuth.com/img/shawn-head.gif", "Bennington", "+37533636993", 1 },
                    { new Guid("81ac393a-2598-4e01-8132-a4e93c6ebe80"), "spritefok4@gmail.com", "Philip", "AQAAAAEAACcQAAAAEOYr9u2IqkTpvuJLMBq/oJ7bmlBaSt7q2oRK8wRaTy+DTj+sjMtet7tVm+GTnxQtvA==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Khamitsevich", "+37533636993", 1 },
                    { new Guid("dd5104bd-b719-4299-adab-a520c12ed616"), "spritefok5@gmail.com", "Sam", "AQAAAAEAACcQAAAAEPmaebIP6+GJ3kSnDxzYbJw76Vrff4U6+ogEcmwRpz8V1MvIN5SZEwlLVRdFXXKDhA==", "https://wildermuth.com/img/shawn-head.gif", "Robinson", "+37533636993", 1 },
                    { new Guid("b65d5fae-6016-4b90-91a9-6fbdc91c545b"), "spritefok6@gmail.com", "Kio", "AQAAAAEAACcQAAAAEPrquX31WC0kXSSyB/pinNpYLILHPZX7INGClYusq3cP+Sh4HgjC6b2+/NU5ZncEbQ==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shima", "+37533636993", 1 },
                    { new Guid("c822f0cc-24b7-496f-8647-c5ae48abfdb6"), "spritefok7@gmail.com", "Yura", null, "https://wildermuth.com/img/shawn-head.gif", "Vasya", "+37533636993", 1 },
                    { new Guid("c99c153e-1b55-462d-92ae-36aa977f4275"), "spritefok8@gmail.com", "Petya", null, "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Jesus", "+37533636993", 1 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProfiles",
                columns: new[] { "Id", "Description", "ServiceCost", "ServiceTypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("646d021b-9921-4a52-9100-92acedcca03c"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 55.65m, 1, new Guid("8574e5bb-49f5-453d-aaa9-8a4bc6c6625f") },
                    { new Guid("b612a262-9043-4d9e-9fe5-c1795ad6c603"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 100.12m, 2, new Guid("c7b4b5d6-bbb4-4079-9792-ec28a4988d66") },
                    { new Guid("4b217e2f-0429-472f-a6cf-67d482c8c718"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 5.93m, 3, new Guid("49c2c66a-2e92-48d7-b0de-9a2a278dac06") },
                    { new Guid("67fb0284-0e21-4fc1-8ea8-8553ac109cf8"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 25.65m, 4, new Guid("81ac393a-2598-4e01-8132-a4e93c6ebe80") },
                    { new Guid("d468cd9a-9525-4ce8-9a44-e511bc30d8a9"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 75.3m, 5, new Guid("dd5104bd-b719-4299-adab-a520c12ed616") },
                    { new Guid("f983d32b-b0cc-4f94-92db-b763e7b4ef26"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 143.4m, 6, new Guid("b65d5fae-6016-4b90-91a9-6fbdc91c545b") },
                    { new Guid("cef61ca4-90b6-41bf-9079-ebf41d0fe722"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 45.3m, 6, new Guid("c822f0cc-24b7-496f-8647-c5ae48abfdb6") },
                    { new Guid("80863d11-f8b1-4145-ab8c-37c66aa28da5"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 84.94m, 6, new Guid("c99c153e-1b55-462d-92ae-36aa977f4275") }
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
                name: "EmployeeProfiles");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ServiceOrders");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
