using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ShoppingCartItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ShoppingCart",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Firstname", "Lastname", "UpdatedOnUtc" },
                values: new object[] { new Guid("998bb1d2-eca1-4055-a51f-69b2e0bbeefd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "John", "Doe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ShoppingCartItem");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingCart");
        }
    }
}
