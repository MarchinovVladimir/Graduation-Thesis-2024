using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIO.Data.Migrations
{
	public partial class LocationAreaEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<bool>(
                name: "IsSold",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Is the product sold?",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Is the product sold?");

            migrationBuilder.AddColumn<int>(
                name: "LocationAreaId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 1,
                comment: "The location area where the product is located.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                comment: "Category's unique identifier.",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "LocationAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The location area's unique identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The location area's name."),
                    PostCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "The location area's postal code.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationAreas", x => x.Id);
                },
                comment: "The location area where the product is located.");

            migrationBuilder.InsertData(
                table: "LocationAreas",
                columns: new[] { "Id", "Name", "PostCode" },
                values: new object[,]
                {
                    { 1, "Sofia", "1000" },
                    { 2, "Plovdiv", "4000" },
                    { 3, "Varna", "9000" },
                    { 4, "Burgas", "8000" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "LocationAreaId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("3785a220-daea-426c-8252-04308869924d"), null, 3, new DateTime(2024, 3, 27, 14, 0, 7, 683, DateTimeKind.Utc).AddTicks(277), "A house for sale", "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", 3, 100000m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "House" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "LocationAreaId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("c6b32035-158a-4f8f-9d38-d2c68c9ead97"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, new DateTime(2024, 3, 27, 14, 0, 7, 683, DateTimeKind.Utc).AddTicks(272), "A bike for sale", "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", 2, 100m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "LocationAreaId", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("d140b236-768f-4a5a-b16e-8c55c6409e47"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, new DateTime(2024, 3, 27, 14, 0, 7, 683, DateTimeKind.Utc).AddTicks(250), "A car for sale", "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", 1, 10000m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "Car" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_LocationAreaId",
                table: "Products",
                column: "LocationAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_LocationAreas_LocationAreaId",
                table: "Products",
                column: "LocationAreaId",
                principalTable: "LocationAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_LocationAreas_LocationAreaId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "LocationAreas");

            migrationBuilder.DropIndex(
                name: "IX_Products_LocationAreaId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3785a220-daea-426c-8252-04308869924d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c6b32035-158a-4f8f-9d38-d2c68c9ead97"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d140b236-768f-4a5a-b16e-8c55c6409e47"));

            migrationBuilder.DropColumn(
                name: "LocationAreaId",
                table: "Products");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSold",
                table: "Products",
                type: "bit",
                nullable: false,
                comment: "Is the product sold?",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Is the product sold?");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                comment: "Primary key",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Category's unique identifier.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "IsSold", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("60643e9a-cd79-4f54-8b47-621503aa5c31"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, new DateTime(2024, 3, 26, 16, 3, 23, 73, DateTimeKind.Utc).AddTicks(698), "A bike for sale", "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, false, 100m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "IsSold", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("8fde732a-5974-48a0-908f-738ad9acf222"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, new DateTime(2024, 3, 26, 16, 3, 23, 73, DateTimeKind.Utc).AddTicks(669), "A car for sale", "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, false, 10000m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BuyerId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "IsSold", "Price", "SellerId", "Title" },
                values: new object[] { new Guid("cf1c4ba1-e2c6-453c-9739-98797913211d"), null, 3, new DateTime(2024, 3, 26, 16, 3, 23, 73, DateTimeKind.Utc).AddTicks(702), "A house for sale", "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, false, 100000m, new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), "House" });
        }
    }
}
