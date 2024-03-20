using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIO.Data.Migrations
{
    public partial class CommentsAddedToEntityClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1141e879-da34-4c83-add0-7006c1dd06d6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("464ecab9-667e-4906-acae-8baacfa8fc5a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("faf41163-9dd7-4374-893a-8d4dc325526f"));

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                comment: "The application user entity.");

            migrationBuilder.AlterColumn<Guid>(
                name: "BuyerId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                comment: "The product's watcher identifier.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "The product's buyer identifier.");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Test",
                comment: "Application user's last name.",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Test");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Test",
                comment: "Application user's first name.",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Test");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Agents",
                type: "uniqueidentifier",
                nullable: false,
                comment: "User identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Bidder identifier");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("425e6acb-f7e8-42e2-82ad-ff70c31518b8"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, "A bike for sale", new DateTime(2024, 3, 27, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3500), "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", 100m, new DateTime(2024, 3, 20, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3500), "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("c05e3d7a-2ed6-4722-8405-659bd38cf5f6"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, "A car for sale", new DateTime(2024, 3, 27, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3476), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", 10000m, new DateTime(2024, 3, 20, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3474), "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("e9f76e18-a369-4005-8d34-826eb192816a"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, "A house for sale", new DateTime(2024, 3, 27, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3516), "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", 100000m, new DateTime(2024, 3, 20, 12, 15, 39, 396, DateTimeKind.Utc).AddTicks(3515), "House" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                oldComment: "The application user entity.");

            migrationBuilder.AlterColumn<Guid>(
                name: "BuyerId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                comment: "The product's buyer identifier.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "The product's watcher identifier.");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Test",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Test",
                oldComment: "Application user's last name.");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Test",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Test",
                oldComment: "Application user's first name.");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Agents",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Bidder identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "User identifier");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "IsActive", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("1141e879-da34-4c83-add0-7006c1dd06d6"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, "A bike for sale", new DateTime(2024, 3, 27, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(1054), "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, 100m, new DateTime(2024, 3, 20, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(1053), "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "IsActive", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("464ecab9-667e-4906-acae-8baacfa8fc5a"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, "A car for sale", new DateTime(2024, 3, 27, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(970), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, 10000m, new DateTime(2024, 3, 20, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(966), "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "IsActive", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("faf41163-9dd7-4374-893a-8d4dc325526f"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, "A house for sale", new DateTime(2024, 3, 27, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(1069), "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, 100000m, new DateTime(2024, 3, 20, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(1069), "House" });
        }
    }
}
