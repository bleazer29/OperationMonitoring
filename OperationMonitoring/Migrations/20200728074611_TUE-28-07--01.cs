using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class TUE280701 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "StorageHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "PartHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "OrderHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "MaintenanceHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "EquipmentHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "StorageHistory");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "PartHistory");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "EquipmentHistory");
        }
    }
}
