using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderingService.Data.Migrations
{
    public partial class UserAddServiceTypeForeign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfiles_ServiceTypes_ServiceTypeId",
                table: "EmployeeProfiles");

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

            migrationBuilder.AlterColumn<int>(
                name: "ServiceTypeId",
                table: "EmployeeProfiles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "HashedPassword", "ImageUrl", "LastName", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("321727a7-97ac-4ae8-b755-b3b59a8c0eb2"), "spritefok1@gmail.com", "Shawn", "AQAAAAEAACcQAAAAEH+mqxPoAWvfyA4HltBxkIldWwlQeF8Kk6u9bwMVck1F+5A9UEjLWCx53vqOb8DZYw==", "https://wildermuth.com/img/shawn-head.gif", "Wildermuth", "+37533655993", 1 },
                    { new Guid("76cbed99-153a-494f-9e43-460817d39759"), "spritefok2@gmail.com", "Mike", "AQAAAAEAACcQAAAAEJ8BZ2DIqE+JxbvUn0s9DRVPaKZVGEN6VU8aMwW5sAzi2W1Z6Jc/4QwJtd1yKfDs1Q==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shinoda", "+37533636993", 1 },
                    { new Guid("5b68b997-3760-492d-a406-c578083365d2"), "spritefok3@gmail.com", "Chester", "AQAAAAEAACcQAAAAEGz6NHtYbcQFVqBnfkUN71GxcNgqN+qsrBPI/CVTm5Zs1e/vF9LwV1aCsoxa+YP5Mw==", "https://wildermuth.com/img/shawn-head.gif", "Bennington", "+37533636993", 1 },
                    { new Guid("3abd0851-e2d8-4194-9e56-38fcdc595157"), "spritefok4@gmail.com", "Philip", "AQAAAAEAACcQAAAAEKsjxcozRl6xStybx2NmHM/GK814YVCYi5/QynJSxCBqEsaE7vHNyXiHjQh/9uAhpg==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Khamitsevich", "+37533636993", 1 },
                    { new Guid("87ce323a-0966-4702-8575-bf761e3494eb"), "spritefok5@gmail.com", "Sam", "AQAAAAEAACcQAAAAEMSDmwtGjPt17oRkrqeLi9tv2gw9RkAo+25R4RZW/ITtrRWO4kKTa91tu3fGbKUJnQ==", "https://wildermuth.com/img/shawn-head.gif", "Robinson", "+37533636993", 1 },
                    { new Guid("716ddda4-56f0-40f4-9f97-6ebc8b67e817"), "spritefok6@gmail.com", "Kio", "AQAAAAEAACcQAAAAEC4bzifYTZ8j3a2iVSM4CnyoXdbSxkgO32pXC11PaO+mZjXckUyG/j7BaQNJ1Z3lAw==", "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Shima", "+37533636993", 1 },
                    { new Guid("e150c3a2-c4b2-4ffe-92b5-7d9f2471da48"), "spritefok7@gmail.com", "Yura", null, "https://wildermuth.com/img/shawn-head.gif", "Vasya", "+37533636993", 1 },
                    { new Guid("1c4fc4f1-7651-4447-ab6f-74a225103d4f"), "spritefok8@gmail.com", "Petya", null, "https://res.cloudinary.com/dofujaj9p/image/upload/v1585154354/egirl_ge9khz.jpg", "Jesus", "+37533636993", 1 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProfiles",
                columns: new[] { "Id", "Description", "ServiceCost", "ServiceTypeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("12867d3b-a023-4c85-8edc-6530f65e8a91"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 55.65m, 1, new Guid("321727a7-97ac-4ae8-b755-b3b59a8c0eb2") },
                    { new Guid("672507e7-422c-4161-a638-71732244d185"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 100.12m, 2, new Guid("76cbed99-153a-494f-9e43-460817d39759") },
                    { new Guid("5d6e642a-18b7-412d-b923-22c866d5a48b"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 5.93m, 3, new Guid("5b68b997-3760-492d-a406-c578083365d2") },
                    { new Guid("4b36d95e-c98d-4486-8d2b-d658d22e2c5f"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 25.65m, 4, new Guid("3abd0851-e2d8-4194-9e56-38fcdc595157") },
                    { new Guid("d85b265a-71ea-4dcf-bcdb-af88fd9bb0b5"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 75.3m, 5, new Guid("87ce323a-0966-4702-8575-bf761e3494eb") },
                    { new Guid("1adcd2ef-7db7-4a4b-9898-138c9a2e71a8"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 143.4m, 6, new Guid("716ddda4-56f0-40f4-9f97-6ebc8b67e817") },
                    { new Guid("2b49af43-f47b-4706-a5f4-77f2eb281278"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 45.3m, 6, new Guid("e150c3a2-c4b2-4ffe-92b5-7d9f2471da48") },
                    { new Guid("096399e4-a6ab-4291-b91a-ee8b5b295a86"), "In Entity Framework before .NET Core, entity framework had a way to create seed data but that method had a number of issues so they decided not to bring it over to Entity Framework Core. Now that we're into version 2.1 of Entity Framework Core, they wanted to allow for a way to seed the data with certain types of data.", 84.94m, 6, new Guid("1c4fc4f1-7651-4447-ab6f-74a225103d4f") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfiles_ServiceTypes_ServiceTypeId",
                table: "EmployeeProfiles",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfiles_ServiceTypes_ServiceTypeId",
                table: "EmployeeProfiles");

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("096399e4-a6ab-4291-b91a-ee8b5b295a86"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("12867d3b-a023-4c85-8edc-6530f65e8a91"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("1adcd2ef-7db7-4a4b-9898-138c9a2e71a8"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("2b49af43-f47b-4706-a5f4-77f2eb281278"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("4b36d95e-c98d-4486-8d2b-d658d22e2c5f"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("5d6e642a-18b7-412d-b923-22c866d5a48b"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("672507e7-422c-4161-a638-71732244d185"));

            migrationBuilder.DeleteData(
                table: "EmployeeProfiles",
                keyColumn: "Id",
                keyValue: new Guid("d85b265a-71ea-4dcf-bcdb-af88fd9bb0b5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1c4fc4f1-7651-4447-ab6f-74a225103d4f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("321727a7-97ac-4ae8-b755-b3b59a8c0eb2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3abd0851-e2d8-4194-9e56-38fcdc595157"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5b68b997-3760-492d-a406-c578083365d2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("716ddda4-56f0-40f4-9f97-6ebc8b67e817"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76cbed99-153a-494f-9e43-460817d39759"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("87ce323a-0966-4702-8575-bf761e3494eb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e150c3a2-c4b2-4ffe-92b5-7d9f2471da48"));

            migrationBuilder.AlterColumn<int>(
                name: "ServiceTypeId",
                table: "EmployeeProfiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfiles_ServiceTypes_ServiceTypeId",
                table: "EmployeeProfiles",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
