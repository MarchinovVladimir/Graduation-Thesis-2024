using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIO.Data.Migrations
{
    public partial class TableNameAgentsSetToSellers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_AspNetUsers_UserId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Agents_AgentId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                table: "Agents");

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

            migrationBuilder.RenameTable(
                name: "Agents",
                newName: "Sellers");

            migrationBuilder.RenameIndex(
                name: "IX_Agents_UserId",
                table: "Sellers",
                newName: "IX_Sellers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("58d2ff18-9dd8-4ad8-a26c-866d7af9b5fa"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, new DateTime(2024, 3, 22, 9, 53, 33, 73, DateTimeKind.Utc).AddTicks(3958), "A bike for sale", "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", 100m, "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("72d063da-b55f-4acd-994b-c73dca06f8fc"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, new DateTime(2024, 3, 22, 9, 53, 33, 73, DateTimeKind.Utc).AddTicks(3941), "A car for sale", "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", 10000m, "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { new Guid("9c59f678-99a6-427b-bbfc-5e9cd172bf6f"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, new DateTime(2024, 3, 22, 9, 53, 33, 73, DateTimeKind.Utc).AddTicks(3962), "A house for sale", "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", 100000m, "House" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sellers_AgentId",
                table: "Products",
                column: "AgentId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_AspNetUsers_UserId",
                table: "Sellers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sellers_AgentId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_AspNetUsers_UserId",
                table: "Sellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers");

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

            migrationBuilder.RenameTable(
                name: "Sellers",
                newName: "Agents");

            migrationBuilder.RenameIndex(
                name: "IX_Sellers_UserId",
                table: "Agents",
                newName: "IX_Agents_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                table: "Agents",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "Title" },
                values: new object[] { new Guid("355976d8-bf02-4de5-a8d6-e31b3eb416c6"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, new DateTime(2024, 3, 21, 15, 33, 54, 535, DateTimeKind.Utc).AddTicks(4945), "A house for sale", "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, 100000m, "House" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "Title" },
                values: new object[] { new Guid("6f900726-f74e-4be9-bfe2-1a6792f9b834"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, new DateTime(2024, 3, 21, 15, 33, 54, 535, DateTimeKind.Utc).AddTicks(4942), "A bike for sale", "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, 100m, "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "Title" },
                values: new object[] { new Guid("f58e5e18-1cf1-4a43-96bf-3ea484472a48"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, new DateTime(2024, 3, 21, 15, 33, 54, 535, DateTimeKind.Utc).AddTicks(4930), "A car for sale", "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, 10000m, "Car" });

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_AspNetUsers_UserId",
                table: "Agents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Agents_AgentId",
                table: "Products",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
