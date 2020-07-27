using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class EquipHistory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartId",
                table: "Stocks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_PartId",
                table: "Stocks",
                column: "PartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Parts_PartId",
                table: "Stocks",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Parts_PartId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_PartId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "PartId",
                table: "Stocks");
        }
    }
}
