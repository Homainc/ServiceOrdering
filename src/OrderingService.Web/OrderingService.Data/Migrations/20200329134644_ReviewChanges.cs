using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderingService.Data.Migrations
{
    public partial class ReviewChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                table: "Reviews",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "HashedPassword", "ImageUrl", "LastName", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("5d884369-2e2a-47ee-8ff8-cf1e0db9072b"), "spritefok1@gmail.com", "Shawn", "AQAAAAEAACcQAAAAEMEtLnxwXCEUMcbtFKRYx38fbbr0sXkAK7JcUfssxLIwmUHOdJlHcshCkaWR3KxUrA==", "https://wildermuth.com/img/shawn-head.gif", "Wildermuth", "+37533655993", 1 },
                    { new Guid("672c519c-8245-48d1-812c-2cf1088cc3f0"), "spritefok2@gmail.com", "Mike", "AQAAAAEAACcQAAAAEEbPjB4JlEQMIBK5yK0aqAw/U1yohoMNUnJ9otx9VNMFryVQes+RfgSWOsCsXVSEhg==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shinoda", "+37533636993", 1 },
                    { new Guid("1da56e12-3634-42f1-8233-ad3dd54ac25e"), "spritefok3@gmail.com", "Chester", "AQAAAAEAACcQAAAAECE8f/+DjHlQ5JycCrBKiyEOmWFgIDgD57o/Cv0FuzABz3Iqc2Rw3qobslr3srv7Cw==", "https://wildermuth.com/img/shawn-head.gif", "Bennington", "+37533636993", 1 },
                    { new Guid("ae7e4c2c-4b99-4619-832d-c971da10f876"), "spritefok4@gmail.com", "Philip", "AQAAAAEAACcQAAAAEAMPwnar79QA+6A5timUv9BQqptoUosMC5ahOkgzqxwIT6lCBt77lVgoU4e9vzWTeA==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Khamitsevich", "+37533636993", 1 },
                    { new Guid("a1a695f2-86d1-4c02-a230-3bd6de47dc03"), "spritefok5@gmail.com", "Sam", "AQAAAAEAACcQAAAAEBrRAxzSzO9cgDQmKvE6jLIVpDPUgU+wQ7BO7wUjvzVrC1Ip+8NxP18Fzu/CYh80HQ==", "https://wildermuth.com/img/shawn-head.gif", "Robinson", "+37533636993", 1 },
                    { new Guid("7daa0aa7-8f57-4e5e-9f9e-d7176b6f9980"), "spritefok6@gmail.com", "Kio", "AQAAAAEAACcQAAAAEFSuemCUfxFloYfrEI325mx+oz6qfz0LQDEMHacDkoM4Yf8vtjTIdD91C0mqapgetA==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shima", "+37533636993", 1 },
                    { new Guid("ff3c9a53-3be9-44d9-a329-91a80a20abd7"), "spritefok7@gmail.com", "Yura", null, "https://wildermuth.com/img/shawn-head.gif", "Vasya", "+37533636993", 1 },
                    { new Guid("019b9c9b-daba-4197-9d42-214c697238a2"), "spritefok8@gmail.com", "Petya", null, "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Jesus", "+37533636993", 1 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProfiles",
                columns: new[] { "Id", "Description", "ServiceCost", "ServiceTypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("b664faa4-94a4-4daa-92cf-f4ff99964afc"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 55.65m, 1, new Guid("5d884369-2e2a-47ee-8ff8-cf1e0db9072b") },
                    { new Guid("546639e9-b3dd-405e-a7b0-198cb02f6026"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 100.12m, 2, new Guid("672c519c-8245-48d1-812c-2cf1088cc3f0") },
                    { new Guid("98bfc040-75f4-4fa9-8817-02b1403b038e"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 5.93m, 3, new Guid("1da56e12-3634-42f1-8233-ad3dd54ac25e") },
                    { new Guid("37e8db55-4c3d-4f14-be96-65a7853e264c"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 25.65m, 4, new Guid("ae7e4c2c-4b99-4619-832d-c971da10f876") },
                    { new Guid("0a60c267-d8a1-4f14-a228-fa32e2e1fa7d"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 75.3m, 5, new Guid("a1a695f2-86d1-4c02-a230-3bd6de47dc03") },
                    { new Guid("b6c25b1e-de22-4f44-91b8-afbde970415f"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 143.4m, 6, new Guid("7daa0aa7-8f57-4e5e-9f9e-d7176b6f9980") },
                    { new Guid("6e8241f7-dd03-4123-a1da-87bfea99b1b8"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 45.3m, 6, new Guid("ff3c9a53-3be9-44d9-a329-91a80a20abd7") },
                    { new Guid("55c396f7-2207-41b8-9f76-9d047093a345"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 84.94m, 6, new Guid("019b9c9b-daba-4197-9d42-214c697238a2") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("0a60c267-d8a1-4f14-a228-fa32e2e1fa7d"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("37e8db55-4c3d-4f14-be96-65a7853e264c"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("546639e9-b3dd-405e-a7b0-198cb02f6026"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("55c396f7-2207-41b8-9f76-9d047093a345"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("6e8241f7-dd03-4123-a1da-87bfea99b1b8"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("98bfc040-75f4-4fa9-8817-02b1403b038e"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("b664faa4-94a4-4daa-92cf-f4ff99964afc"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("b6c25b1e-de22-4f44-91b8-afbde970415f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("019b9c9b-daba-4197-9d42-214c697238a2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1da56e12-3634-42f1-8233-ad3dd54ac25e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5d884369-2e2a-47ee-8ff8-cf1e0db9072b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("672c519c-8245-48d1-812c-2cf1088cc3f0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7daa0aa7-8f57-4e5e-9f9e-d7176b6f9980"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1a695f2-86d1-4c02-a230-3bd6de47dc03"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ae7e4c2c-4b99-4619-832d-c971da10f876"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ff3c9a53-3be9-44d9-a329-91a80a20abd7"));

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Reviews");

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
        }
    }
}
