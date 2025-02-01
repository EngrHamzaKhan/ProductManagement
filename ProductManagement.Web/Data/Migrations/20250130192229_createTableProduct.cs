using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class createTableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Laptop", 1000m, 10 },
                    { 2, "Smartphone", 700m, 15 },
                    { 3, "Tablet", 300m, 20 },
                    { 4, "Smartwatch", 150m, 50 },
                    { 5, "Headphones", 100m, 30 },
                    { 6, "Monitor", 200m, 25 },
                    { 7, "Keyboard", 50m, 40 },
                    { 8, "Mouse", 30m, 60 },
                    { 9, "Charger", 20m, 100 },
                    { 10, "Laptop Stand", 40m, 70 },
                    { 11, "Bluetooth Speaker", 120m, 35 },
                    { 12, "Wireless Mouse", 45m, 40 },
                    { 13, "External Hard Drive", 80m, 25 },
                    { 14, "USB Flash Drive", 15m, 90 },
                    { 15, "Graphics Card", 500m, 10 },
                    { 16, "CPU", 300m, 15 },
                    { 17, "Webcam", 80m, 50 },
                    { 18, "Router", 60m, 20 },
                    { 19, "AirPods", 150m, 30 },
                    { 20, "Portable Speaker", 70m, 45 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
