using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIO.Data.Migrations
{
    public partial class AgentIdChangedToSellerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sellers_AgentId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("58d2ff18-9dd8-4ad8-a26c-866d7af9b5fa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("72d063da-b55f-4acd-994b-c73dca06f8fc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9c59f678-99a6-427b-bbfc-5e9cd172bf6f"));

            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "Products",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_AgentId",
                table: "Products",
                newName: "IX_Products_SellerId");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("43e9ec5b-49a4-4936-af29-34580a4f4b84"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, new DateTime(2024, 3, 22, 13, 52, 17, 95, DateTimeKind.Utc).AddTicks(6516), "A car for sale", "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", 10000m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("c243d11e-6393-44e1-b486-2b423454714d"), null, 3, new DateTime(2024, 3, 22, 13, 52, 17, 95, DateTimeKind.Utc).AddTicks(6535), "A house for sale", "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", 100000m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "House" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("cd2c9527-5691-45cd-b6f5-272706da21fb"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, new DateTime(2024, 3, 22, 13, 52, 17, 95, DateTimeKind.Utc).AddTicks(6532), "A bike for sale", "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", 100m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "Bike" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sellers_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sellers_SellerId",
                table: "Products");

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

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Products",
                newName: "AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                newName: "IX_Products_AgentId");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "Title" },
                values: new object[] { new Guid("58d2ff18-9dd8-4ad8-a26c-866d7af9b5fa"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, new DateTime(2024, 3, 22, 9, 53, 33, 73, DateTimeKind.Utc).AddTicks(3958), "A bike for sale", "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, 100m, "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "Title" },
                values: new object[] { new Guid("72d063da-b55f-4acd-994b-c73dca06f8fc"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, new DateTime(2024, 3, 22, 9, 53, 33, 73, DateTimeKind.Utc).AddTicks(3941), "A car for sale", "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, 10000m, "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "Title" },
                values: new object[] { new Guid("9c59f678-99a6-427b-bbfc-5e9cd172bf6f"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, new DateTime(2024, 3, 22, 9, 53, 33, 73, DateTimeKind.Utc).AddTicks(3962), "A house for sale", "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, 100000m, "House" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sellers_AgentId",
                table: "Products",
                column: "AgentId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
