using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderingService.Data.Migrations
{
    public partial class OrderAndReviewExpanded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ServiceOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "ServiceOrders",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Rate",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "HashedPassword", "ImageUrl", "LastName", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("56d5d4c0-f660-45dc-80c9-ac7560c68444"), "spritefok1@gmail.com", "Shawn", "AQAAAAEAACcQAAAAEGN8b881h55AuyRSl6A5lA9H+JC5uOzEBgST5ONL38zFFzkmFDrAhe1GMzHqusGlCg==", "https://wildermuth.com/img/shawn-head.gif", "Wildermuth", "+37533655993", 1 },
                    { new Guid("5c9d8a55-75a7-456d-9587-18fe1f03e8e1"), "spritefok2@gmail.com", "Mike", "AQAAAAEAACcQAAAAEK5lYUJ0fJ4qSiPFPRECzRpAaLECB5+byp4ZxK1+dXyhIr/X+tDH9CAM7liRYLLjTA==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shinoda", "+37533636993", 1 },
                    { new Guid("5facbc29-a063-4662-9a53-9b7c54483792"), "spritefok3@gmail.com", "Chester", "AQAAAAEAACcQAAAAENrPmJAKuHZ/PNjdVKUSyz/jolJO17mGq6AkKm93H3vhdOppcIvYbJcO/ZoO5vi04g==", "https://wildermuth.com/img/shawn-head.gif", "Bennington", "+37533636993", 1 },
                    { new Guid("d7a1b12a-aa05-4c0e-b11b-a17ea8141d75"), "spritefok4@gmail.com", "Philip", "AQAAAAEAACcQAAAAECEebrhangJxcMDt8Bo/dw9cyDOZO2mCuJbbuBBIamXJpYTQ+zUfDBhcXZSEOlLohA==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Khamitsevich", "+37533636993", 1 },
                    { new Guid("88aecc17-f381-403c-99dc-311a56725a63"), "spritefok5@gmail.com", "Sam", "AQAAAAEAACcQAAAAEOTIDwbop7wFQr/JSj6+PEv7CRQiYkSqq7HpyHgfr7gWh+BLv1vQ30v2nYQtRnOiaQ==", "https://wildermuth.com/img/shawn-head.gif", "Robinson", "+37533636993", 1 },
                    { new Guid("594679c1-241d-4dbe-b1a6-307a2c477531"), "spritefok6@gmail.com", "Kio", "AQAAAAEAACcQAAAAECC6p5sWB11gG2FfkOCrSZp59vvuc2iH2L1GN1MmM1xwtKZ4RVe+n7B3EGPAz8gHOQ==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shima", "+37533636993", 1 },
                    { new Guid("7f0fac7a-2490-4c8e-804e-b4942f2b90ea"), "spritefok7@gmail.com", "Yura", null, "https://wildermuth.com/img/shawn-head.gif", "Vasya", "+37533636993", 1 },
                    { new Guid("d0d1b0b8-0033-42b9-a800-daecc05277ae"), "spritefok8@gmail.com", "Petya", null, "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Jesus", "+37533636993", 1 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProfiles",
                columns: new[] { "Id", "Description", "ServiceCost", "ServiceTypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("54fbd4e7-8d93-4d34-9cb1-c5abaa8559fe"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 55.65m, 1, new Guid("56d5d4c0-f660-45dc-80c9-ac7560c68444") },
                    { new Guid("4220f113-22a8-46ab-828e-900be3e2356d"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 100.12m, 2, new Guid("5c9d8a55-75a7-456d-9587-18fe1f03e8e1") },
                    { new Guid("f046f928-cb81-4c82-8bdd-a79ed18a4b7e"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 5.93m, 3, new Guid("5facbc29-a063-4662-9a53-9b7c54483792") },
                    { new Guid("856acf14-008c-4fb0-8c55-e022161e7442"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 25.65m, 4, new Guid("d7a1b12a-aa05-4c0e-b11b-a17ea8141d75") },
                    { new Guid("2e444113-02ae-4067-9f96-2d1030d6109c"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 75.3m, 5, new Guid("88aecc17-f381-403c-99dc-311a56725a63") },
                    { new Guid("84a05df2-8559-4fb8-bb3b-1a98c9bf9197"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 143.4m, 6, new Guid("594679c1-241d-4dbe-b1a6-307a2c477531") },
                    { new Guid("262bc294-d8fc-49cb-9968-65bbd045effa"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 45.3m, 6, new Guid("7f0fac7a-2490-4c8e-804e-b4942f2b90ea") },
                    { new Guid("d088406d-0ce4-4073-8af2-4745dfc8a44e"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 84.94m, 6, new Guid("d0d1b0b8-0033-42b9-a800-daecc05277ae") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("262bc294-d8fc-49cb-9968-65bbd045effa"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("2e444113-02ae-4067-9f96-2d1030d6109c"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("4220f113-22a8-46ab-828e-900be3e2356d"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("54fbd4e7-8d93-4d34-9cb1-c5abaa8559fe"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("84a05df2-8559-4fb8-bb3b-1a98c9bf9197"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("856acf14-008c-4fb0-8c55-e022161e7442"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("d088406d-0ce4-4073-8af2-4745dfc8a44e"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("f046f928-cb81-4c82-8bdd-a79ed18a4b7e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("56d5d4c0-f660-45dc-80c9-ac7560c68444"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("594679c1-241d-4dbe-b1a6-307a2c477531"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5c9d8a55-75a7-456d-9587-18fe1f03e8e1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5facbc29-a063-4662-9a53-9b7c54483792"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7f0fac7a-2490-4c8e-804e-b4942f2b90ea"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88aecc17-f381-403c-99dc-311a56725a63"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d0d1b0b8-0033-42b9-a800-daecc05277ae"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d7a1b12a-aa05-4c0e-b11b-a17ea8141d75"));

            migrationBuilder.DropColumn(
                name: "Address",
                table: "ServiceOrders");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "ServiceOrders");

            migrationBuilder.AlterColumn<float>(
                name: "Rate",
                table: "Reviews",
                type: "real",
                nullable: false,
                oldClrType: typeof(int));

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
    }
}
