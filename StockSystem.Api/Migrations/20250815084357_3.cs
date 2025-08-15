using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("998bb1d2-eca1-4055-a51f-69b2e0bbeefd"));

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ShoppingCartItem",
                newName: "PricePerUnit");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ShoppingCart",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Firstname", "Lastname", "UpdatedOnUtc" },
                values: new object[] { new Guid("1ebdb93d-b4d0-46b8-8205-662359cbe15e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "John", "Doe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_UserId",
                table: "ShoppingCart",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_User_UserId",
                table: "ShoppingCart",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_User_UserId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_UserId",
                table: "ShoppingCart");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1ebdb93d-b4d0-46b8-8205-662359cbe15e"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingCart");

            migrationBuilder.RenameColumn(
                name: "PricePerUnit",
                table: "ShoppingCartItem",
                newName: "Price");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Firstname", "Lastname", "UpdatedOnUtc" },
                values: new object[] { new Guid("998bb1d2-eca1-4055-a51f-69b2e0bbeefd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "John", "Doe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
