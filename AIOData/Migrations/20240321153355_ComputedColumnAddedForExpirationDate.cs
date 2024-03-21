using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIO.Data.Migrations
{
    public partial class ComputedColumnAddedForExpirationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("160b50d9-cec7-4d63-95d0-101b62cfe763"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6b17e4a6-74c0-4d20-8c06-6548ed1ab9cd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8baae914-89d2-4faf-9895-3bcd1d43163a"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                computedColumnSql: "DATEADD(DAY, 7, CreatedOn)",
                comment: "The date when the product listing expires",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "The date when the product listing expires");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("355976d8-bf02-4de5-a8d6-e31b3eb416c6"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, new DateTime(2024, 3, 21, 15, 33, 54, 535, DateTimeKind.Utc).AddTicks(4945), "A house for sale", "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", 100000m, "House" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("6f900726-f74e-4be9-bfe2-1a6792f9b834"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, new DateTime(2024, 3, 21, 15, 33, 54, 535, DateTimeKind.Utc).AddTicks(4942), "A bike for sale", "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", 100m, "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("f58e5e18-1cf1-4a43-96bf-3ea484472a48"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, new DateTime(2024, 3, 21, 15, 33, 54, 535, DateTimeKind.Utc).AddTicks(4930), "A car for sale", "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", 10000m, "Car" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("355976d8-bf02-4de5-a8d6-e31b3eb416c6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6f900726-f74e-4be9-bfe2-1a6792f9b834"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f58e5e18-1cf1-4a43-96bf-3ea484472a48"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                comment: "The date when the product listing expires",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComputedColumnSql: "DATEADD(DAY, 7, CreatedOn)",
                oldComment: "The date when the product listing expires");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ExpirationDate", "ImageUrl", "IsActive", "Price", "Title" },
                values: new object[] { new Guid("160b50d9-cec7-4d63-95d0-101b62cfe763"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, new DateTime(2024, 3, 21, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(948), "A house for sale", new DateTime(2024, 3, 28, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(948), "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, 100000m, "House" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ExpirationDate", "ImageUrl", "IsActive", "Price", "Title" },
                values: new object[] { new Guid("6b17e4a6-74c0-4d20-8c06-6548ed1ab9cd"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, new DateTime(2024, 3, 21, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(931), "A car for sale", new DateTime(2024, 3, 28, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(933), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, 10000m, "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ExpirationDate", "ImageUrl", "IsActive", "Price", "Title" },
                values: new object[] { new Guid("8baae914-89d2-4faf-9895-3bcd1d43163a"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, new DateTime(2024, 3, 21, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(944), "A bike for sale", new DateTime(2024, 3, 28, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(944), "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, 100m, "Bike" });
        }
    }
}
