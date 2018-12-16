using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFridge.Data.Migrations
{
    public partial class ShoppingItemChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ShoppingItems");

            migrationBuilder.AddColumn<bool>(
                name: "WasBought",
                table: "ShoppingItems",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasBought",
                table: "ShoppingItems");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ShoppingItems",
                nullable: false,
                defaultValue: 0);
        }
    }
}
