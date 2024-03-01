using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIO.Data.Migrations
{
    public partial class fixCreatingProductDateValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2a94b425-27ce-4762-955a-c10ecc3c99d1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5cd1ee0c-fb36-46a8-812e-d8afb6fe7a05"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9ec851e5-edf6-4a97-bc2d-030773be9646"));

            migrationBuilder.AlterTable(
                name: "Products",
                comment: "The product that is being auctioned.");

            migrationBuilder.AlterTable(
                name: "Agents",
                comment: "Agent entity. Represents the user who sells products.");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "The product's title.",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "The product's start time.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "OpeningBid",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                comment: "The product's opening bid.",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<bool>(
                name: "IsAuctionClosed",
                table: "Products",
                type: "bit",
                nullable: false,
                comment: "The product's auction status.",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                comment: "The product's image URL.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                comment: "The product's end time.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "The product's description.",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentBid",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                comment: "The product's current bid.",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                comment: "The product's category identifier.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "BuyerId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                comment: "The product's buyer identifier.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AgentId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                comment: "The product's agent identifier.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                comment: "The product's unique identifier.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Category name",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                comment: "Primary key",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Agents",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Bidder identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Agents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Agent phone number",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Agents",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Agent identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("54cf4523-bbeb-4aea-adc1-8c34b801cfc2"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, 12000m, "A car for sale", new DateTime(2024, 3, 8, 7, 21, 55, 89, DateTimeKind.Utc).AddTicks(3002), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, 10000m, new DateTime(2024, 3, 1, 7, 21, 55, 89, DateTimeKind.Utc).AddTicks(3000), "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("ec029585-765d-4f2d-8c19-7ed3c9090dbc"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, 120m, "A bike for sale", new DateTime(2024, 3, 8, 7, 21, 55, 89, DateTimeKind.Utc).AddTicks(3031), "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, 100m, new DateTime(2024, 3, 1, 7, 21, 55, 89, DateTimeKind.Utc).AddTicks(3031), "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("f00af202-3817-40bf-a786-ded511a0f8d9"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, 120000m, "A house for sale", new DateTime(2024, 3, 8, 7, 21, 55, 89, DateTimeKind.Utc).AddTicks(3037), "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, 100000m, new DateTime(2024, 3, 1, 7, 21, 55, 89, DateTimeKind.Utc).AddTicks(3037), "House" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("54cf4523-bbeb-4aea-adc1-8c34b801cfc2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ec029585-765d-4f2d-8c19-7ed3c9090dbc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f00af202-3817-40bf-a786-ded511a0f8d9"));

            migrationBuilder.AlterTable(
                name: "Products",
                oldComment: "The product that is being auctioned.");

            migrationBuilder.AlterTable(
                name: "Agents",
                oldComment: "Agent entity. Represents the user who sells products.");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "The product's title.");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()",
                oldComment: "The product's start time.");

            migrationBuilder.AlterColumn<decimal>(
                name: "OpeningBid",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldComment: "The product's opening bid.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAuctionClosed",
                table: "Products",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "The product's auction status.");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldComment: "The product's image URL.");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "The product's end time.");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "The product's description.");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentBid",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldComment: "The product's current bid.");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The product's category identifier.");

            migrationBuilder.AlterColumn<Guid>(
                name: "BuyerId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "The product's buyer identifier.");

            migrationBuilder.AlterColumn<Guid>(
                name: "AgentId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "The product's agent identifier.");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "The product's unique identifier.");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Category name");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Agents",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Bidder identifier");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Agents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Agent phone number");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Agents",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Agent identifier");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("2a94b425-27ce-4762-955a-c10ecc3c99d1"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, 120m, "A bike for sale", new DateTime(2024, 2, 28, 12, 45, 59, 523, DateTimeKind.Utc).AddTicks(6005), "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, 100m, new DateTime(2024, 2, 21, 12, 45, 59, 523, DateTimeKind.Utc).AddTicks(6004), "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("5cd1ee0c-fb36-46a8-812e-d8afb6fe7a05"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, 12000m, "A car for sale", new DateTime(2024, 2, 28, 12, 45, 59, 523, DateTimeKind.Utc).AddTicks(5944), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, 10000m, new DateTime(2024, 2, 21, 12, 45, 59, 523, DateTimeKind.Utc).AddTicks(5939), "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("9ec851e5-edf6-4a97-bc2d-030773be9646"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, 120000m, "A house for sale", new DateTime(2024, 2, 28, 12, 45, 59, 523, DateTimeKind.Utc).AddTicks(6017), "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, 100000m, new DateTime(2024, 2, 21, 12, 45, 59, 523, DateTimeKind.Utc).AddTicks(6017), "House" });
        }
    }
}
