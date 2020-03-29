using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderingService.Data.Migrations
{
    public partial class ServiceOrderChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_EmployeeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOrders_Users_EmployeeId",
                table: "ServiceOrders");

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("4b217e2f-0429-472f-a6cf-67d482c8c718"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("646d021b-9921-4a52-9100-92acedcca03c"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("67fb0284-0e21-4fc1-8ea8-8553ac109cf8"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("80863d11-f8b1-4145-ab8c-37c66aa28da5"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("b612a262-9043-4d9e-9fe5-c1795ad6c603"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("cef61ca4-90b6-41bf-9079-ebf41d0fe722"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("d468cd9a-9525-4ce8-9a44-e511bc30d8a9"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("f983d32b-b0cc-4f94-92db-b763e7b4ef26"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("49c2c66a-2e92-48d7-b0de-9a2a278dac06"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("81ac393a-2598-4e01-8132-a4e93c6ebe80"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8574e5bb-49f5-453d-aaa9-8a4bc6c6625f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b65d5fae-6016-4b90-91a9-6fbdc91c545b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c7b4b5d6-bbb4-4079-9792-ec28a4988d66"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c822f0cc-24b7-496f-8647-c5ae48abfdb6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c99c153e-1b55-462d-92ae-36aa977f4275"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dd5104bd-b719-4299-adab-a520c12ed616"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServiceOrders");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "ServiceOrders");

            migrationBuilder.AddColumn<string>(
                name: "BriefTask",
                table: "ServiceOrders",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceDetails",
                table: "ServiceOrders",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ServiceOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "HashedPassword", "ImageUrl", "LastName", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("d9233d13-29a8-4d66-8aa1-6bd1850d0c12"), "spritefok1@gmail.com", "Shawn", "AQAAAAEAACcQAAAAEBKZwD6sax2lx441wu0fOKBdxN7WvSedaGr2RENTkjiCvcAWfUCeFbdgRJODHugWkQ==", "https://wildermuth.com/img/shawn-head.gif", "Wildermuth", "+37533655993", 1 },
                    { new Guid("e92b1200-2a7c-4419-8fd3-5370b1c96241"), "spritefok2@gmail.com", "Mike", "AQAAAAEAACcQAAAAEN+2jCgWhkHg1aqUlOu6G2P9Lebqjlg7DYfIx2edFbKC37JpPec7xEaNNb3fnTu3VA==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shinoda", "+37533636993", 1 },
                    { new Guid("42533eb5-4744-4da0-b00e-12e7131f7981"), "spritefok3@gmail.com", "Chester", "AQAAAAEAACcQAAAAEC2UP1+oKps2O08prAhchL3o0X8mZrBJkn0PxsJby4D5W13L4QY6s9JHBv9OR6ORng==", "https://wildermuth.com/img/shawn-head.gif", "Bennington", "+37533636993", 1 },
                    { new Guid("3be5ae3f-88fd-46b1-8f47-bcc7a4e2fe30"), "spritefok4@gmail.com", "Philip", "AQAAAAEAACcQAAAAEBLPKnR43pHfyANCbfV3bFFzbFJq092+mQ6NbJYGYSJL+5/ZXyxGuRFTKDRGw3RxuQ==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Khamitsevich", "+37533636993", 1 },
                    { new Guid("d97448b9-4e97-4a6a-a825-7f57d2a87c43"), "spritefok5@gmail.com", "Sam", "AQAAAAEAACcQAAAAEKtb15RF7Ag8az6Je2H3+FiAQ5Teo2kZ6cRl/UpQNcVtMNfL3WjOjq2Quzahe46KwQ==", "https://wildermuth.com/img/shawn-head.gif", "Robinson", "+37533636993", 1 },
                    { new Guid("caf1f720-7639-4eee-9be8-a6839055f826"), "spritefok6@gmail.com", "Kio", "AQAAAAEAACcQAAAAEKUCN78hhNrphUo7uvaOwhHyVn9V9YDWeyHsPLkFODorna6AhahJh/GiX+Fs41zzsQ==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shima", "+37533636993", 1 },
                    { new Guid("ca74fe44-30bc-4be2-8f49-e48dbff4e361"), "spritefok7@gmail.com", "Yura", null, "https://wildermuth.com/img/shawn-head.gif", "Vasya", "+37533636993", 1 },
                    { new Guid("ed763fc6-8f0c-4644-afb6-83d32f31b1d8"), "spritefok8@gmail.com", "Petya", null, "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Jesus", "+37533636993", 1 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProfiles",
                columns: new[] { "Id", "Description", "ServiceCost", "ServiceTypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("41829cad-5103-4b68-9297-6df34e65d5ee"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 55.65m, 1, new Guid("d9233d13-29a8-4d66-8aa1-6bd1850d0c12") },
                    { new Guid("258fbd9c-afdf-41cc-830e-d1c24a2593c9"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 100.12m, 2, new Guid("e92b1200-2a7c-4419-8fd3-5370b1c96241") },
                    { new Guid("c684cfe1-e49b-42d3-83ec-886229e0f723"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 5.93m, 3, new Guid("42533eb5-4744-4da0-b00e-12e7131f7981") },
                    { new Guid("ac5031c0-a51a-4ec8-9655-c365d55892e5"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 25.65m, 4, new Guid("3be5ae3f-88fd-46b1-8f47-bcc7a4e2fe30") },
                    { new Guid("ea43fcaf-0bde-4b47-8531-193f3f907987"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 75.3m, 5, new Guid("d97448b9-4e97-4a6a-a825-7f57d2a87c43") },
                    { new Guid("3f8f27b8-4339-4def-ab49-8f52a64578e3"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 143.4m, 6, new Guid("caf1f720-7639-4eee-9be8-a6839055f826") },
                    { new Guid("89611358-129d-48b4-8a78-502c5473f7a8"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 45.3m, 6, new Guid("ca74fe44-30bc-4be2-8f49-e48dbff4e361") },
                    { new Guid("f835be1c-9f57-4b10-8681-8c9db192b5c1"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 84.94m, 6, new Guid("ed763fc6-8f0c-4644-afb6-83d32f31b1d8") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_EmployeeProfiles_EmployeeId",
                table: "Reviews",
                column: "EmployeeId",
                principalTable: "EmployeeProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOrders_EmployeeProfiles_EmployeeId",
                table: "ServiceOrders",
                column: "EmployeeId",
                principalTable: "EmployeeProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_EmployeeProfiles_EmployeeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOrders_EmployeeProfiles_EmployeeId",
                table: "ServiceOrders");

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("258fbd9c-afdf-41cc-830e-d1c24a2593c9"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("3f8f27b8-4339-4def-ab49-8f52a64578e3"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("41829cad-5103-4b68-9297-6df34e65d5ee"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("89611358-129d-48b4-8a78-502c5473f7a8"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("ac5031c0-a51a-4ec8-9655-c365d55892e5"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("c684cfe1-e49b-42d3-83ec-886229e0f723"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("ea43fcaf-0bde-4b47-8531-193f3f907987"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("f835be1c-9f57-4b10-8681-8c9db192b5c1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3be5ae3f-88fd-46b1-8f47-bcc7a4e2fe30"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("42533eb5-4744-4da0-b00e-12e7131f7981"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ca74fe44-30bc-4be2-8f49-e48dbff4e361"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("caf1f720-7639-4eee-9be8-a6839055f826"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d9233d13-29a8-4d66-8aa1-6bd1850d0c12"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d97448b9-4e97-4a6a-a825-7f57d2a87c43"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e92b1200-2a7c-4419-8fd3-5370b1c96241"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ed763fc6-8f0c-4644-afb6-83d32f31b1d8"));

            migrationBuilder.DropColumn(
                name: "BriefTask",
                table: "ServiceOrders");

            migrationBuilder.DropColumn(
                name: "ServiceDetails",
                table: "ServiceOrders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ServiceOrders");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServiceOrders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "ServiceOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_EmployeeId",
                table: "Reviews",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOrders_Users_EmployeeId",
                table: "ServiceOrders",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
