using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class wqd4u : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageHistory_Parts_PartId",
                table: "StorageHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageHistory_Storages_StorageFromId",
                table: "StorageHistory");

            migrationBuilder.DropIndex(
                name: "IX_StorageHistory_PartId",
                table: "StorageHistory");

            migrationBuilder.DropIndex(
                name: "IX_StorageHistory_StorageFromId",
                table: "StorageHistory");

            migrationBuilder.DropColumn(
                name: "PartId",
                table: "StorageHistory");

            migrationBuilder.DropColumn(
                name: "StorageFromId",
                table: "StorageHistory");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "StorageHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "StorageHistory",
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

            migrationBuilder.CreateIndex(
                name: "IX_StorageHistory_StockId",
                table: "StorageHistory",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageHistory_Stocks_StockId",
                table: "StorageHistory",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageHistory_Stocks_StockId",
                table: "StorageHistory");

            migrationBuilder.DropIndex(
                name: "IX_StorageHistory_StockId",
                table: "StorageHistory");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "StorageHistory");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "StorageHistory");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "MaintenanceHistory");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "EquipmentHistory");

            migrationBuilder.AddColumn<int>(
                name: "PartId",
                table: "StorageHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StorageFromId",
                table: "StorageHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageHistory_PartId",
                table: "StorageHistory",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageHistory_StorageFromId",
                table: "StorageHistory",
                column: "StorageFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageHistory_Parts_PartId",
                table: "StorageHistory",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageHistory_Storages_StorageFromId",
                table: "StorageHistory",
                column: "StorageFromId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
