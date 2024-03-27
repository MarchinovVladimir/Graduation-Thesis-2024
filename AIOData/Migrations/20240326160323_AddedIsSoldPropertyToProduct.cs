using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIO.Data.Migrations
{
    public partial class AddedIsSoldPropertyToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("43e9ec5b-49a4-4936-af29-34580a4f4b84"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c243d11e-6393-44e1-b486-2b423454714d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cd2c9527-5691-45cd-b6f5-272706da21fb"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: true,
                comment: "Is product listing active?",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true,
                oldComment: "The product's status.");

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Is the product sold?");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsSold", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("60643e9a-cd79-4f54-8b47-621503aa5c31"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, new DateTime(2024, 3, 26, 16, 3, 23, 73, DateTimeKind.Utc).AddTicks(698), "A bike for sale", "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, 100m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsSold", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("8fde732a-5974-48a0-908f-738ad9acf222"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, new DateTime(2024, 3, 26, 16, 3, 23, 73, DateTimeKind.Utc).AddTicks(669), "A car for sale", "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, 10000m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsSold", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("cf1c4ba1-e2c6-453c-9739-98797913211d"), null, 3, new DateTime(2024, 3, 26, 16, 3, 23, 73, DateTimeKind.Utc).AddTicks(702), "A house for sale", "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, 100000m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "House" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("60643e9a-cd79-4f54-8b47-621503aa5c31"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8fde732a-5974-48a0-908f-738ad9acf222"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cf1c4ba1-e2c6-453c-9739-98797913211d"));

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Products");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: true,
                comment: "The product's status.",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true,
                oldComment: "Is product listing active?");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("43e9ec5b-49a4-4936-af29-34580a4f4b84"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, new DateTime(2024, 3, 22, 13, 52, 17, 95, DateTimeKind.Utc).AddTicks(6516), "A car for sale", "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, 10000m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("c243d11e-6393-44e1-b486-2b423454714d"), null, 3, new DateTime(2024, 3, 22, 13, 52, 17, 95, DateTimeKind.Utc).AddTicks(6535), "A house for sale", "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, 100000m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "House" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("cd2c9527-5691-45cd-b6f5-272706da21fb"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, new DateTime(2024, 3, 22, 13, 52, 17, 95, DateTimeKind.Utc).AddTicks(6532), "A bike for sale", "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, 100m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "Bike" });
        }
    }
}
