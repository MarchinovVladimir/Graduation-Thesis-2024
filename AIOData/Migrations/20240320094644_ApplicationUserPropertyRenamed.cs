using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIO.Data.Migrations
{
	public partial class ApplicationUserPropertyRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a84c7ea3-1f17-4be2-bc5b-ba87249dfa4d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b7ed216b-25cc-4bb9-ba4c-3b259ca7f8f7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d3762042-c9ed-4ad9-b7d0-23b791d9b822"));

            migrationBuilder.AlterTable(
                name: "Products",
                comment: "The product that is for sell.",
                oldComment: "The product that is being auctioned.");

            migrationBuilder.AlterTable(
                name: "Categories",
                comment: "The category of the product.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: true,
                comment: "The product's status.",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AgentId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                comment: "The product's seller identifier.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "The product's agent identifier.");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("1141e879-da34-4c83-add0-7006c1dd06d6"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, "A bike for sale", new DateTime(2024, 3, 27, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(1054), "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", 100m, new DateTime(2024, 3, 20, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(1053), "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("464ecab9-667e-4906-acae-8baacfa8fc5a"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, "A car for sale", new DateTime(2024, 3, 27, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(970), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", 10000m, new DateTime(2024, 3, 20, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(966), "Car" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "Description", "EndTime", "ImageUrl", "Price", "StartTime", "Title" },
                values: new object[] { new Guid("faf41163-9dd7-4374-893a-8d4dc325526f"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, "A house for sale", new DateTime(2024, 3, 27, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(1069), "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", 100000m, new DateTime(2024, 3, 20, 9, 46, 43, 698, DateTimeKind.Utc).AddTicks(1069), "House" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.AlterTable(
                name: "Products",
                comment: "The product that is being auctioned.",
                oldComment: "The product that is for sell.");

            migrationBuilder.AlterTable(
                name: "Categories",
                oldComment: "The category of the product.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true,
                oldComment: "The product's status.");

            migrationBuilder.AlterColumn<Guid>(
                name: "AgentId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                comment: "The product's agent identifier.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "The product's seller identifier.");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentBid",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                comment: "The product's current bid.");

            migrationBuilder.AddColumn<bool>(
                name: "IsAuctionClosed",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningBid",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                comment: "The product's opening bid.");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsActive", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("a84c7ea3-1f17-4be2-bc5b-ba87249dfa4d"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 2, 120m, "A bike for sale", new DateTime(2024, 3, 19, 7, 47, 34, 447, DateTimeKind.Utc).AddTicks(9543), "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", false, false, 100m, new DateTime(2024, 3, 12, 7, 47, 34, 447, DateTimeKind.Utc).AddTicks(9542), "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsActive", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("b7ed216b-25cc-4bb9-ba4c-3b259ca7f8f7"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), null, 3, 120000m, "A house for sale", new DateTime(2024, 3, 19, 7, 47, 34, 447, DateTimeKind.Utc).AddTicks(9559), "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg", false, false, 100000m, new DateTime(2024, 3, 12, 7, 47, 34, 447, DateTimeKind.Utc).AddTicks(9558), "House" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgentId", "BuyerId", "CategoryId", "CurrentBid", "Description", "EndTime", "ImageUrl", "IsActive", "IsAuctionClosed", "OpeningBid", "StartTime", "Title" },
                values: new object[] { new Guid("d3762042-c9ed-4ad9-b7d0-23b791d9b822"), new Guid("db47b449-630e-4857-bc80-34a6c3e8e822"), new Guid("ab6d096a-0ccc-49ae-2db2-08dc32d4f58a"), 1, 12000m, "A car for sale", new DateTime(2024, 3, 19, 7, 47, 34, 447, DateTimeKind.Utc).AddTicks(9514), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D", false, false, 10000m, new DateTime(2024, 3, 12, 7, 47, 34, 447, DateTimeKind.Utc).AddTicks(9512), "Car" });
        }
    }
}
