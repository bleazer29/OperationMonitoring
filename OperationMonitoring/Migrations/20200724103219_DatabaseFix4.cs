using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class DatabaseFix4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentHistory_Docs_DocId",
                table: "EquipmentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_Docs_DocId",
                table: "MaintenanceHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_MaintenanceTypes_MaintenanceTypeId",
                table: "MaintenanceHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_Parts_PartId",
                table: "MaintenanceHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Agreements_AgreementId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Docs_DocId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Equipment_EquipmentId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Wells_WellId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_AgreementId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_DocId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_EquipmentId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_WellId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceHistory_DocId",
                table: "MaintenanceHistory");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceHistory_MaintenanceTypeId",
                table: "MaintenanceHistory");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceHistory_PartId",
                table: "MaintenanceHistory");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentHistory_DocId",
                table: "EquipmentHistory");

            migrationBuilder.DropColumn(
                name: "AgreementId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "DateFinish",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "DocId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "OperationInfo",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "WellId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "DateFinish",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "DocId",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "IsOpened",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "MaintenanceTypeId",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "PartId",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "DocId",
                table: "EquipmentHistory");

            migrationBuilder.DropColumn(
                name: "EquipmentOperatingTime",
                table: "EquipmentHistory");

            migrationBuilder.AddColumn<int>(
                name: "OperatingTime",
                table: "OrderHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceId",
                table: "MaintenanceHistory",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "Assemblies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    MaintenanceTypeId = table.Column<int>(nullable: true),
                    IsOpened = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenance_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maintenance_MaintenanceTypes_MaintenanceTypeId",
                        column: x => x.MaintenanceTypeId,
                        principalTable: "MaintenanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgreementId = table.Column<int>(nullable: true),
                    WellId = table.Column<int>(nullable: true),
                    EquipmentId = table.Column<int>(nullable: true),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateFinish = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Agreements_AgreementId",
                        column: x => x.AgreementId,
                        principalTable: "Agreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Wells_WellId",
                        column: x => x.WellId,
                        principalTable: "Wells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_OrderId",
                table: "OrderHistory",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_MaintenanceId",
                table: "MaintenanceHistory",
                column: "MaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_EquipmentId",
                table: "Maintenance",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_MaintenanceTypeId",
                table: "Maintenance",
                column: "MaintenanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AgreementId",
                table: "Order",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_EquipmentId",
                table: "Order",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_WellId",
                table: "Order",
                column: "WellId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_Maintenance_MaintenanceId",
                table: "MaintenanceHistory",
                column: "MaintenanceId",
                principalTable: "Maintenance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_Order_OrderId",
                table: "OrderHistory",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_Maintenance_MaintenanceId",
                table: "MaintenanceHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Order_OrderId",
                table: "OrderHistory");

            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_OrderId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceHistory_MaintenanceId",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "OperatingTime",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "MaintenanceId",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "Assemblies");

            migrationBuilder.AddColumn<int>(
                name: "AgreementId",
                table: "OrderHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFinish",
                table: "OrderHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DocId",
                table: "OrderHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "OrderHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperationInfo",
                table: "OrderHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WellId",
                table: "OrderHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFinish",
                table: "MaintenanceHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DocId",
                table: "MaintenanceHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOpened",
                table: "MaintenanceHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceTypeId",
                table: "MaintenanceHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartId",
                table: "MaintenanceHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocId",
                table: "EquipmentHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentOperatingTime",
                table: "EquipmentHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_AgreementId",
                table: "OrderHistory",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_DocId",
                table: "OrderHistory",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_EquipmentId",
                table: "OrderHistory",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_WellId",
                table: "OrderHistory",
                column: "WellId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_DocId",
                table: "MaintenanceHistory",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_MaintenanceTypeId",
                table: "MaintenanceHistory",
                column: "MaintenanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_PartId",
                table: "MaintenanceHistory",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistory_DocId",
                table: "EquipmentHistory",
                column: "DocId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentHistory_Docs_DocId",
                table: "EquipmentHistory",
                column: "DocId",
                principalTable: "Docs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_Docs_DocId",
                table: "MaintenanceHistory",
                column: "DocId",
                principalTable: "Docs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_MaintenanceTypes_MaintenanceTypeId",
                table: "MaintenanceHistory",
                column: "MaintenanceTypeId",
                principalTable: "MaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_Parts_PartId",
                table: "MaintenanceHistory",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_Agreements_AgreementId",
                table: "OrderHistory",
                column: "AgreementId",
                principalTable: "Agreements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_Docs_DocId",
                table: "OrderHistory",
                column: "DocId",
                principalTable: "Docs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_Equipment_EquipmentId",
                table: "OrderHistory",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_Wells_WellId",
                table: "OrderHistory",
                column: "WellId",
                principalTable: "Wells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
