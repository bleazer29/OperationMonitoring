using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class EquipHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentHistory_EquipmentStatuses_StatusFromId",
                table: "EquipmentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentHistory_EquipmentStatuses_StatusToId",
                table: "EquipmentHistory");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentHistory_StatusFromId",
                table: "EquipmentHistory");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentHistory_StatusToId",
                table: "EquipmentHistory");

            migrationBuilder.DropColumn(
                name: "StatusFromId",
                table: "EquipmentHistory");

            migrationBuilder.DropColumn(
                name: "StatusToId",
                table: "EquipmentHistory");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "EquipmentHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistory_StatusId",
                table: "EquipmentHistory",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentHistory_EquipmentStatuses_StatusId",
                table: "EquipmentHistory",
                column: "StatusId",
                principalTable: "EquipmentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentHistory_EquipmentStatuses_StatusId",
                table: "EquipmentHistory");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentHistory_StatusId",
                table: "EquipmentHistory");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "EquipmentHistory");

            migrationBuilder.AddColumn<int>(
                name: "StatusFromId",
                table: "EquipmentHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusToId",
                table: "EquipmentHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistory_StatusFromId",
                table: "EquipmentHistory",
                column: "StatusFromId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistory_StatusToId",
                table: "EquipmentHistory",
                column: "StatusToId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentHistory_EquipmentStatuses_StatusFromId",
                table: "EquipmentHistory",
                column: "StatusFromId",
                principalTable: "EquipmentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentHistory_EquipmentStatuses_StatusToId",
                table: "EquipmentHistory",
                column: "StatusToId",
                principalTable: "EquipmentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
