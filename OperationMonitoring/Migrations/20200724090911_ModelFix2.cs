using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class ModelFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_Equipment_EquipmentId",
                table: "MaintenanceHistory");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceHistory_EquipmentId",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "MaintenanceHistory");

            migrationBuilder.AddColumn<bool>(
                name: "IsOpened",
                table: "MaintenanceHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PartId",
                table: "MaintenanceHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_PartId",
                table: "MaintenanceHistory",
                column: "PartId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_Parts_PartId",
                table: "MaintenanceHistory",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_Parts_PartId",
                table: "MaintenanceHistory");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceHistory_PartId",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "IsOpened",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "PartId",
                table: "MaintenanceHistory");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "MaintenanceHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_EquipmentId",
                table: "MaintenanceHistory",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_Equipment_EquipmentId",
                table: "MaintenanceHistory",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
