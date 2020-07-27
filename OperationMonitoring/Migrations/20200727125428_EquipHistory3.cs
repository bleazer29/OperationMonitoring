using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class EquipHistory3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartHistory_EquipmentStatuses_StatusFromId",
                table: "PartHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PartHistory_EquipmentStatuses_StatusToId",
                table: "PartHistory");

            migrationBuilder.DropIndex(
                name: "IX_PartHistory_StatusFromId",
                table: "PartHistory");

            migrationBuilder.DropIndex(
                name: "IX_PartHistory_StatusToId",
                table: "PartHistory");

            migrationBuilder.DropColumn(
                name: "StatusFromId",
                table: "PartHistory");

            migrationBuilder.DropColumn(
                name: "StatusToId",
                table: "PartHistory");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PartHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartHistory_StatusId",
                table: "PartHistory",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartHistory_EquipmentStatuses_StatusId",
                table: "PartHistory",
                column: "StatusId",
                principalTable: "EquipmentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartHistory_EquipmentStatuses_StatusId",
                table: "PartHistory");

            migrationBuilder.DropIndex(
                name: "IX_PartHistory_StatusId",
                table: "PartHistory");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PartHistory");

            migrationBuilder.AddColumn<int>(
                name: "StatusFromId",
                table: "PartHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusToId",
                table: "PartHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartHistory_StatusFromId",
                table: "PartHistory",
                column: "StatusFromId");

            migrationBuilder.CreateIndex(
                name: "IX_PartHistory_StatusToId",
                table: "PartHistory",
                column: "StatusToId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartHistory_EquipmentStatuses_StatusFromId",
                table: "PartHistory",
                column: "StatusFromId",
                principalTable: "EquipmentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartHistory_EquipmentStatuses_StatusToId",
                table: "PartHistory",
                column: "StatusToId",
                principalTable: "EquipmentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
