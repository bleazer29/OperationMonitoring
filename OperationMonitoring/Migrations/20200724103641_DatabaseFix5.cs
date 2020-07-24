using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class DatabaseFix5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenance_Equipment_EquipmentId",
                table: "Maintenance");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenance_MaintenanceTypes_MaintenanceTypeId",
                table: "Maintenance");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_Maintenance_MaintenanceId",
                table: "MaintenanceHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Agreements_AgreementId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Equipment_EquipmentId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Wells_WellId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Order_OrderId",
                table: "OrderHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maintenance",
                table: "Maintenance");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Maintenance",
                newName: "Maintenances");

            migrationBuilder.RenameIndex(
                name: "IX_Order_WellId",
                table: "Orders",
                newName: "IX_Orders_WellId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_EquipmentId",
                table: "Orders",
                newName: "IX_Orders_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AgreementId",
                table: "Orders",
                newName: "IX_Orders_AgreementId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenance_MaintenanceTypeId",
                table: "Maintenances",
                newName: "IX_Maintenances_MaintenanceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenance_EquipmentId",
                table: "Maintenances",
                newName: "IX_Maintenances_EquipmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maintenances",
                table: "Maintenances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_Maintenances_MaintenanceId",
                table: "MaintenanceHistory",
                column: "MaintenanceId",
                principalTable: "Maintenances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Equipment_EquipmentId",
                table: "Maintenances",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_MaintenanceTypes_MaintenanceTypeId",
                table: "Maintenances",
                column: "MaintenanceTypeId",
                principalTable: "MaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_Orders_OrderId",
                table: "OrderHistory",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Agreements_AgreementId",
                table: "Orders",
                column: "AgreementId",
                principalTable: "Agreements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Equipment_EquipmentId",
                table: "Orders",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Wells_WellId",
                table: "Orders",
                column: "WellId",
                principalTable: "Wells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceHistory_Maintenances_MaintenanceId",
                table: "MaintenanceHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Equipment_EquipmentId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_MaintenanceTypes_MaintenanceTypeId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Orders_OrderId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Agreements_AgreementId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Equipment_EquipmentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Wells_WellId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maintenances",
                table: "Maintenances");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Maintenances",
                newName: "Maintenance");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_WellId",
                table: "Order",
                newName: "IX_Order_WellId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_EquipmentId",
                table: "Order",
                newName: "IX_Order_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AgreementId",
                table: "Order",
                newName: "IX_Order_AgreementId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenances_MaintenanceTypeId",
                table: "Maintenance",
                newName: "IX_Maintenance_MaintenanceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenances_EquipmentId",
                table: "Maintenance",
                newName: "IX_Maintenance_EquipmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maintenance",
                table: "Maintenance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenance_Equipment_EquipmentId",
                table: "Maintenance",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenance_MaintenanceTypes_MaintenanceTypeId",
                table: "Maintenance",
                column: "MaintenanceTypeId",
                principalTable: "MaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceHistory_Maintenance_MaintenanceId",
                table: "MaintenanceHistory",
                column: "MaintenanceId",
                principalTable: "Maintenance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Agreements_AgreementId",
                table: "Order",
                column: "AgreementId",
                principalTable: "Agreements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Equipment_EquipmentId",
                table: "Order",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Wells_WellId",
                table: "Order",
                column: "WellId",
                principalTable: "Wells",
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
    }
}
