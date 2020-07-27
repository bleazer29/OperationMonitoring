using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class DatabaseFix7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentHistory_Employees_UserId",
                table: "EquipmentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_Employees_UserId",
                table: "MaintenanceHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Employees_UserId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageHistory_Docs_DocId",
                table: "StorageHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageHistory_Employees_UserId",
                table: "StorageHistory");

            migrationBuilder.DropIndex(
                name: "IX_StorageHistory_DocId",
                table: "StorageHistory");

            migrationBuilder.DropIndex(
                name: "IX_StorageHistory_UserId",
                table: "StorageHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_UserId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceHistory_UserId",
                table: "MaintenanceHistory");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentHistory_UserId",
                table: "EquipmentHistory");

            migrationBuilder.DropColumn(
                name: "DocId",
                table: "StorageHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StorageHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EquipmentHistory");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Storages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "StorageHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Stocks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeliveryLocation",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimateDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "OrderHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CounterpartyId",
                table: "Maintenances",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimateDate",
                table: "Maintenances",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleId",
                table: "Maintenances",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Maintenances",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ReturnStorageId",
                table: "Maintenances",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "MaintenanceHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "EquipmentHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageHistory_AuthorId",
                table: "StorageHistory",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_EquipmentId",
                table: "Stocks",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ResponsibleId",
                table: "Orders",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_AuthorId",
                table: "OrderHistory",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_CounterpartyId",
                table: "Maintenances",
                column: "CounterpartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_ResponsibleId",
                table: "Maintenances",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_ReturnStorageId",
                table: "Maintenances",
                column: "ReturnStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_AuthorId",
                table: "MaintenanceHistory",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistory_AuthorId",
                table: "EquipmentHistory",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentHistory_Employees_AuthorId",
                table: "EquipmentHistory",
                column: "AuthorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_Employees_AuthorId",
                table: "MaintenanceHistory",
                column: "AuthorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Counterparties_CounterpartyId",
                table: "Maintenances",
                column: "CounterpartyId",
                principalTable: "Counterparties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Employees_ResponsibleId",
                table: "Maintenances",
                column: "ResponsibleId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Storages_ReturnStorageId",
                table: "Maintenances",
                column: "ReturnStorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_Employees_AuthorId",
                table: "OrderHistory",
                column: "AuthorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_ResponsibleId",
                table: "Orders",
                column: "ResponsibleId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Equipment_EquipmentId",
                table: "Stocks",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageHistory_Employees_AuthorId",
                table: "StorageHistory",
                column: "AuthorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentHistory_Employees_AuthorId",
                table: "EquipmentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_Employees_AuthorId",
                table: "MaintenanceHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Counterparties_CounterpartyId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Employees_ResponsibleId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Storages_ReturnStorageId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Employees_AuthorId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_ResponsibleId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Equipment_EquipmentId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageHistory_Employees_AuthorId",
                table: "StorageHistory");

            migrationBuilder.DropIndex(
                name: "IX_StorageHistory_AuthorId",
                table: "StorageHistory");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_EquipmentId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ResponsibleId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_AuthorId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_CounterpartyId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_ResponsibleId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_ReturnStorageId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceHistory_AuthorId",
                table: "MaintenanceHistory");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentHistory_AuthorId",
                table: "EquipmentHistory");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "StorageHistory");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryLocation",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EstimateDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "CounterpartyId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "EstimateDate",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "ReturnStorageId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "EquipmentHistory");

            migrationBuilder.AddColumn<int>(
                name: "DocId",
                table: "StorageHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StorageHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OrderHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MaintenanceHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "EquipmentHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageHistory_DocId",
                table: "StorageHistory",
                column: "DocId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageHistory_UserId",
                table: "StorageHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_UserId",
                table: "OrderHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_UserId",
                table: "MaintenanceHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistory_UserId",
                table: "EquipmentHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentHistory_Employees_UserId",
                table: "EquipmentHistory",
                column: "UserId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_Employees_UserId",
                table: "MaintenanceHistory",
                column: "UserId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_Employees_UserId",
                table: "OrderHistory",
                column: "UserId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageHistory_Docs_DocId",
                table: "StorageHistory",
                column: "DocId",
                principalTable: "Docs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageHistory_Employees_UserId",
                table: "StorageHistory",
                column: "UserId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
