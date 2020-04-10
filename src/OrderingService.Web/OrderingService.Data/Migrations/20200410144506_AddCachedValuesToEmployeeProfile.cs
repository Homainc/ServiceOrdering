using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderingService.Data.Migrations
{
    public partial class AddCachedValuesToEmployeeProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("068225e8-34ba-4226-bd0b-66ec5c9e6fa3"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("15cb790d-99ac-43ce-a25a-ac505f76bc53"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("6eecbaa0-47b7-4bd7-ad5b-8720c80eb7db"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("70c1e72f-2c0f-4c32-a7d8-4213901f9d97"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("766ff5ec-dcfb-4454-8bf1-c802c556482b"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("998be124-11e3-424f-ad4c-8ff455dcba19"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("bedc4a7d-548e-4b49-a819-288ff60004eb"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("dffeefd9-f7f7-4ffe-add0-8f1173cd8e58"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("57877578-6c78-4c75-9443-86e0d738036c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f72f51d-4ab1-4c81-9bc4-e8f90e31d539"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74ad850e-508e-4e2a-95e1-cbd6fdba3cef"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ad6c3763-f62a-4649-aecd-b2f61766b6bb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b61764cd-adeb-41b4-82b9-ada7c3c07188"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b7ca5d21-5c9d-48bc-8b80-de97669335c6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4b50715-54c1-4570-aed0-a7f4831d2edd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e9a54770-1a4b-4683-89fe-b137b26e0fce"));

            migrationBuilder.AddColumn<double>(
                name: "AverageRate",
                table: "EmployeeProfiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewsCount",
                table: "EmployeeProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "HashedPassword", "ImageUrl", "LastName", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("5a73b1df-4de5-45b0-9b57-85029d6c15c8"), "spritefok1@gmail.com", "Shawn", "AQAAAAEAACcQAAAAEEwKCTa3AIXjFvV7Cjmx0WNTcwc03b0gOOwt7G/8sjNbEdqKU9JQ8fyQ9BSlRfSBMQ==", "https://wildermuth.com/img/shawn-head.gif", "Wildermuth", "+37533655993", 1 },
                    { new Guid("f2f0fb2e-5d78-442b-9bf8-fdbd324d7949"), "spritefok2@gmail.com", "Mike", "AQAAAAEAACcQAAAAEMI4nOdToYLDZSDXCMMYy1KY1uWsZEVjpdKwb+oi6BnhkLPp+JWbtzhwYRyyXgF5Qg==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shinoda", "+37533636993", 1 },
                    { new Guid("ecf42233-6f81-4cbf-a188-fb17a0c2b7f9"), "spritefok3@gmail.com", "Chester", "AQAAAAEAACcQAAAAEPUZO6usJsdpICuW8vJLnAQoUGx8j+WXWMSgLvrDeenlfyjlbTowOdkhaSKZGZYCgQ==", "https://wildermuth.com/img/shawn-head.gif", "Bennington", "+37533636993", 1 },
                    { new Guid("e63aa3cc-4e68-4ff9-b54c-ecea9cb7decd"), "spritefok4@gmail.com", "Philip", "AQAAAAEAACcQAAAAECP0xwuAudzBuTGPWWRbX1qIyLojnnC9j5yTZF5Reanm8hld4mlGh1dtlnERwSmrEg==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Khamitsevich", "+37533636993", 1 },
                    { new Guid("f292f5ac-0bcc-46b1-b23e-66892ecf60ca"), "spritefok5@gmail.com", "Sam", "AQAAAAEAACcQAAAAECFKDJCVG5AN7MSXEXHAJUgbG4auuK/FHgsFd8f+qRfBYIW3Vi2DNJrxKRQS4B7yng==", "https://wildermuth.com/img/shawn-head.gif", "Robinson", "+37533636993", 1 },
                    { new Guid("4a212982-6a7d-4312-900d-32c168f760e4"), "spritefok6@gmail.com", "Kio", "AQAAAAEAACcQAAAAEEY1zOGjYkBq3NYVU3P109Q9OfoJZXRkVnVCgunhozAiPBrENMhssGZl+cHBxJzNwQ==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shima", "+37533636993", 1 },
                    { new Guid("9206f461-5b66-40b6-84c5-78fc7a5fdbca"), "spritefok7@gmail.com", "Yura", null, "https://wildermuth.com/img/shawn-head.gif", "Vasya", "+37533636993", 1 },
                    { new Guid("cc4d48e1-97b8-471c-a455-a8b5f9426018"), "spritefok8@gmail.com", "Petya", null, "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Jesus", "+37533636993", 1 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProfiles",
                columns: new[] { "Id", "AverageRate", "Description", "ReviewsCount", "ServiceCost", "ServiceTypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("3e6ede08-a083-4e27-badb-29a6df07117b"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 55.65m, 1, new Guid("5a73b1df-4de5-45b0-9b57-85029d6c15c8") },
                    { new Guid("c46607e3-b8e2-4661-838c-517bd8b2e05f"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 100.12m, 2, new Guid("f2f0fb2e-5d78-442b-9bf8-fdbd324d7949") },
                    { new Guid("f97ab19b-7166-4386-b7cb-09cf9c1ae89d"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 5.93m, 3, new Guid("ecf42233-6f81-4cbf-a188-fb17a0c2b7f9") },
                    { new Guid("6e9019b3-c970-4c3b-ba29-4c103f729032"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 25.65m, 4, new Guid("e63aa3cc-4e68-4ff9-b54c-ecea9cb7decd") },
                    { new Guid("51134c3e-58c6-4ee5-b763-09e24949a829"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 75.3m, 5, new Guid("f292f5ac-0bcc-46b1-b23e-66892ecf60ca") },
                    { new Guid("4b6bd398-f1e7-4fbc-a109-c408a331fc2f"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 143.4m, 6, new Guid("4a212982-6a7d-4312-900d-32c168f760e4") },
                    { new Guid("f1ef3656-8cad-4f97-9cd6-f89d5ed8729c"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 45.3m, 6, new Guid("9206f461-5b66-40b6-84c5-78fc7a5fdbca") },
                    { new Guid("49e3225e-d527-45f3-9c85-0b41c9a50656"), null, "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 0, 84.94m, 6, new Guid("cc4d48e1-97b8-471c-a455-a8b5f9426018") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("3e6ede08-a083-4e27-badb-29a6df07117b"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("49e3225e-d527-45f3-9c85-0b41c9a50656"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("4b6bd398-f1e7-4fbc-a109-c408a331fc2f"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("51134c3e-58c6-4ee5-b763-09e24949a829"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("6e9019b3-c970-4c3b-ba29-4c103f729032"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("c46607e3-b8e2-4661-838c-517bd8b2e05f"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("f1ef3656-8cad-4f97-9cd6-f89d5ed8729c"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("f97ab19b-7166-4386-b7cb-09cf9c1ae89d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4a212982-6a7d-4312-900d-32c168f760e4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5a73b1df-4de5-45b0-9b57-85029d6c15c8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9206f461-5b66-40b6-84c5-78fc7a5fdbca"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cc4d48e1-97b8-471c-a455-a8b5f9426018"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e63aa3cc-4e68-4ff9-b54c-ecea9cb7decd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ecf42233-6f81-4cbf-a188-fb17a0c2b7f9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f292f5ac-0bcc-46b1-b23e-66892ecf60ca"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2f0fb2e-5d78-442b-9bf8-fdbd324d7949"));

            migrationBuilder.DropColumn(
                name: "AverageRate",
                table: "EmployeeProfiles");

            migrationBuilder.DropColumn(
                name: "ReviewsCount",
                table: "EmployeeProfiles");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "HashedPassword", "ImageUrl", "LastName", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("5f72f51d-4ab1-4c81-9bc4-e8f90e31d539"), "spritefok1@gmail.com", "Shawn", "AQAAAAEAACcQAAAAEPLxsUDahY37ru5M9YqM8oH0Rmm73aLSaqe2zG25nLfBWypEEaZUSRK7D/iovM90Mw==", "https://wildermuth.com/img/shawn-head.gif", "Wildermuth", "+37533655993", 1 },
                    { new Guid("e9a54770-1a4b-4683-89fe-b137b26e0fce"), "spritefok2@gmail.com", "Mike", "AQAAAAEAACcQAAAAEAW0s6//nDygUe+SVmGwBNNTfYPSpJx2DB0AYsA1jroSYGzT6NVLIQAAswYv9uAYnQ==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shinoda", "+37533636993", 1 },
                    { new Guid("b7ca5d21-5c9d-48bc-8b80-de97669335c6"), "spritefok3@gmail.com", "Chester", "AQAAAAEAACcQAAAAECd0vID3gcXn5ajHMnuwY3pTN9n8CFk1+/gfmHsYy/QE0XOY+RFsCQTYjIaXH1FKmQ==", "https://wildermuth.com/img/shawn-head.gif", "Bennington", "+37533636993", 1 },
                    { new Guid("74ad850e-508e-4e2a-95e1-cbd6fdba3cef"), "spritefok4@gmail.com", "Philip", "AQAAAAEAACcQAAAAEGa6B9gZzPOtkMh+aVHAHARMy2+WeV7193+5kYtExCBMw9i/nGbvYXXqVxw2tjCy2A==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Khamitsevich", "+37533636993", 1 },
                    { new Guid("b61764cd-adeb-41b4-82b9-ada7c3c07188"), "spritefok5@gmail.com", "Sam", "AQAAAAEAACcQAAAAEK8C29eOPjIZFVAvG1oHlrt6oNQ9KgxbnxpiAKGZ5RrJRDqWcF0pFOY/ShP/Eq2cUA==", "https://wildermuth.com/img/shawn-head.gif", "Robinson", "+37533636993", 1 },
                    { new Guid("ad6c3763-f62a-4649-aecd-b2f61766b6bb"), "spritefok6@gmail.com", "Kio", "AQAAAAEAACcQAAAAEBQ99L4ai5stlDCCyt5m2gDoFcQmnbUJs1yiF0luUHIQtalNorOPB//5awbAHMmqTQ==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shima", "+37533636993", 1 },
                    { new Guid("57877578-6c78-4c75-9443-86e0d738036c"), "spritefok7@gmail.com", "Yura", null, "https://wildermuth.com/img/shawn-head.gif", "Vasya", "+37533636993", 1 },
                    { new Guid("c4b50715-54c1-4570-aed0-a7f4831d2edd"), "spritefok8@gmail.com", "Petya", null, "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Jesus", "+37533636993", 1 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProfiles",
                columns: new[] { "Id", "Description", "ServiceCost", "ServiceTypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("068225e8-34ba-4226-bd0b-66ec5c9e6fa3"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 55.65m, 1, new Guid("5f72f51d-4ab1-4c81-9bc4-e8f90e31d539") },
                    { new Guid("bedc4a7d-548e-4b49-a819-288ff60004eb"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 100.12m, 2, new Guid("e9a54770-1a4b-4683-89fe-b137b26e0fce") },
                    { new Guid("766ff5ec-dcfb-4454-8bf1-c802c556482b"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 5.93m, 3, new Guid("b7ca5d21-5c9d-48bc-8b80-de97669335c6") },
                    { new Guid("998be124-11e3-424f-ad4c-8ff455dcba19"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 25.65m, 4, new Guid("74ad850e-508e-4e2a-95e1-cbd6fdba3cef") },
                    { new Guid("70c1e72f-2c0f-4c32-a7d8-4213901f9d97"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 75.3m, 5, new Guid("b61764cd-adeb-41b4-82b9-ada7c3c07188") },
                    { new Guid("15cb790d-99ac-43ce-a25a-ac505f76bc53"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 143.4m, 6, new Guid("ad6c3763-f62a-4649-aecd-b2f61766b6bb") },
                    { new Guid("6eecbaa0-47b7-4bd7-ad5b-8720c80eb7db"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 45.3m, 6, new Guid("57877578-6c78-4c75-9443-86e0d738036c") },
                    { new Guid("dffeefd9-f7f7-4ffe-add0-8f1173cd8e58"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 84.94m, 6, new Guid("c4b50715-54c1-4570-aed0-a7f4831d2edd") }
                });
        }
    }
}
