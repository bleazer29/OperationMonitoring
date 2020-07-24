using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class InitMagration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    EquipmentId = table.Column<int>(nullable: false),
                    HistoryTypeId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    Commentary = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    EquipmentOperatingTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentHistory_HistoryTypes_HistoryTypeId",
                        column: x => x.HistoryTypeId,
                        principalTable: "HistoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HistoryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "MaintenanceHistory" });

            migrationBuilder.InsertData(
                table: "HistoryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "OrderHistory" });

            migrationBuilder.InsertData(
                table: "HistoryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "General" });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistory_HistoryTypeId",
                table: "EquipmentHistory",
                column: "HistoryTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentHistory");

            migrationBuilder.DropTable(
                name: "HistoryTypes");
        }
    }
}
