using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class HistoryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartHistory_AssembliesHistory_AssemblyHistoryId",
                table: "PartHistory");

            migrationBuilder.DropTable(
                name: "AssembliesHistory");

            migrationBuilder.DropIndex(
                name: "IX_PartHistory_AssemblyHistoryId",
                table: "PartHistory");

            migrationBuilder.DropColumn(
                name: "AssemblyHistoryId",
                table: "PartHistory");

            migrationBuilder.AddColumn<int>(
                name: "AssembleId",
                table: "Parts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PartHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StatusFromId",
                table: "PartHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusToId",
                table: "PartHistory",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Assemblies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<int>(nullable: true),
                    PartId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assemblies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assemblies_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assemblies_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "NA");

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "RFU");

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "JF");

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Title",
                value: "WS");

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "Title",
                value: "SP");

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 6,
                column: "Title",
                value: "RP");

            migrationBuilder.InsertData(
                table: "EquipmentStatuses",
                columns: new[] { "Id", "Title" },
                values: new object[] { 7, "Scrap" });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_AssembleId",
                table: "Parts",
                column: "AssembleId");

            migrationBuilder.CreateIndex(
                name: "IX_PartHistory_StatusFromId",
                table: "PartHistory",
                column: "StatusFromId");

            migrationBuilder.CreateIndex(
                name: "IX_PartHistory_StatusToId",
                table: "PartHistory",
                column: "StatusToId");

            migrationBuilder.CreateIndex(
                name: "IX_Assemblies_EquipmentId",
                table: "Assemblies",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assemblies_PartId",
                table: "Assemblies",
                column: "PartId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Assemblies_AssembleId",
                table: "Parts",
                column: "AssembleId",
                principalTable: "Assemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartHistory_EquipmentStatuses_StatusFromId",
                table: "PartHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PartHistory_EquipmentStatuses_StatusToId",
                table: "PartHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Assemblies_AssembleId",
                table: "Parts");

            migrationBuilder.DropTable(
                name: "Assemblies");

            migrationBuilder.DropIndex(
                name: "IX_Parts_AssembleId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_PartHistory_StatusFromId",
                table: "PartHistory");

            migrationBuilder.DropIndex(
                name: "IX_PartHistory_StatusToId",
                table: "PartHistory");

            migrationBuilder.DeleteData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "AssembleId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PartHistory");

            migrationBuilder.DropColumn(
                name: "StatusFromId",
                table: "PartHistory");

            migrationBuilder.DropColumn(
                name: "StatusToId",
                table: "PartHistory");

            migrationBuilder.AddColumn<int>(
                name: "AssemblyHistoryId",
                table: "PartHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssembliesHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Commentary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocId = table.Column<int>(type: "int", nullable: true),
                    EquipmentId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssembliesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssembliesHistory_Docs_DocId",
                        column: x => x.DocId,
                        principalTable: "Docs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssembliesHistory_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssembliesHistory_Employees_UserId",
                        column: x => x.UserId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "RFU");

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "JF");

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "WS");

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Title",
                value: "SP");

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "Title",
                value: "RP");

            migrationBuilder.UpdateData(
                table: "EquipmentStatuses",
                keyColumn: "Id",
                keyValue: 6,
                column: "Title",
                value: "Scrap");

            migrationBuilder.CreateIndex(
                name: "IX_PartHistory_AssemblyHistoryId",
                table: "PartHistory",
                column: "AssemblyHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AssembliesHistory_DocId",
                table: "AssembliesHistory",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_AssembliesHistory_EquipmentId",
                table: "AssembliesHistory",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssembliesHistory_UserId",
                table: "AssembliesHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartHistory_AssembliesHistory_AssemblyHistoryId",
                table: "PartHistory",
                column: "AssemblyHistoryId",
                principalTable: "AssembliesHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
