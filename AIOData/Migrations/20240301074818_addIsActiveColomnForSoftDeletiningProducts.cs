using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIO.Data.Migrations
{
    public partial class addIsActiveColomnForSoftDeletiningProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<bool>(
                name: "IsAuctionClosed",
                table: "Products",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "The product's auction status.");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("5d857b0c-d5de-4b6a-9450-c130413cc91a"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, 12000m, "A car for sale", new DateTime(2024, 3, 8, 7, 48, 17, 492, DateTimeKind.Utc).AddTicks(6187), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, 10000m, new DateTime(2024, 3, 1, 7, 48, 17, 492, DateTimeKind.Utc).AddTicks(6185), "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("99b6adac-69f3-48d1-9891-42fd07f74b6d"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, 120m, "A bike for sale", new DateTime(2024, 3, 8, 7, 48, 17, 492, DateTimeKind.Utc).AddTicks(6220), "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, 100m, new DateTime(2024, 3, 1, 7, 48, 17, 492, DateTimeKind.Utc).AddTicks(6220), "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("bee5022d-489a-4561-8913-76d9b004f38c"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, 120000m, "A house for sale", new DateTime(2024, 3, 8, 7, 48, 17, 492, DateTimeKind.Utc).AddTicks(6224), "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, 100000m, new DateTime(2024, 3, 1, 7, 48, 17, 492, DateTimeKind.Utc).AddTicks(6224), "House" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5d857b0c-d5de-4b6a-9450-c130413cc91a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("99b6adac-69f3-48d1-9891-42fd07f74b6d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bee5022d-489a-4561-8913-76d9b004f38c"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAuctionClosed",
                table: "Products",
                type: "bit",
                nullable: false,
                comment: "The product's auction status.",
                oldClrType: typeof(bool),
                oldType: "bit");

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
    }
}
