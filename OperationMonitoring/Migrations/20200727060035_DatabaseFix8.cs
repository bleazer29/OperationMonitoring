using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class DatabaseFix8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InventoryNum",
                table: "Parts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryNum",
                table: "Equipment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserGUID",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryNum",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "InventoryNum",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "UserGUID",
                table: "Employees");
        }
    }
}
