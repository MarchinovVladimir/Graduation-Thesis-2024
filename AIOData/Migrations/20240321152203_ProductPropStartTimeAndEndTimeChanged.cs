using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIO.Data.Migrations
{
    public partial class ProductPropStartTimeAndEndTimeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("425e6acb-f7e8-42e2-82ad-ff70c31518b8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c05e3d7a-2ed6-4722-8405-659bd38cf5f6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e9f76e18-a369-4005-8d34-826eb192816a"));

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Products");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "The date when the product listing is created");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "The date when the product listing expires");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ExpirationDate", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("160b50d9-cec7-4d63-95d0-101b62cfe763"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, new DateTime(2024, 3, 21, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(948), "A house for sale", new DateTime(2024, 3, 28, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(948), "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", 100000m, "House" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ExpirationDate", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("6b17e4a6-74c0-4d20-8c06-6548ed1ab9cd"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, new DateTime(2024, 3, 21, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(931), "A car for sale", new DateTime(2024, 3, 28, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(933), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", 10000m, "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ExpirationDate", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("8baae914-89d2-4faf-9895-3bcd1d43163a"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, new DateTime(2024, 3, 21, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(944), "A bike for sale", new DateTime(2024, 3, 28, 15, 22, 2, 732, DateTimeKind.Utc).AddTicks(944), "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", 100m, "Bike" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Products");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "The product's end time.");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "The product's start time.");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "IsActive", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("425e6acb-f7e8-42e2-82ad-ff70c31518b8"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, "A bike for sale", new DateTime(2024, 3, 27, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3500), "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, 100m, new DateTime(2024, 3, 20, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3500), "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "IsActive", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("c05e3d7a-2ed6-4722-8405-659bd38cf5f6"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, "A car for sale", new DateTime(2024, 3, 27, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3476), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, 10000m, new DateTime(2024, 3, 20, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3474), "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "IsActive", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("e9f76e18-a369-4005-8d34-826eb192816a"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, "A house for sale", new DateTime(2024, 3, 27, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3516), "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, 100000m, new DateTime(2024, 3, 20, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3515), "House" });
        }
    }
}
