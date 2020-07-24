using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class ModelFix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Counterparties_CounterpartyId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_CounterpartyId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "CounterpartyId",
                table: "OrderHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CounterpartyId",
                table: "OrderHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_CounterpartyId",
                table: "OrderHistory",
                column: "CounterpartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_Counterparties_CounterpartyId",
                table: "OrderHistory",
                column: "CounterpartyId",
                principalTable: "Counterparties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
