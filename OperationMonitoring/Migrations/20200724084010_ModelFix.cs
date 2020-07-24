using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class ModelFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VendorCode",
                table: "Nomenclatures",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocId",
                table: "Agreements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_DocId",
                table: "Agreements",
                column: "DocId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreements_Docs_DocId",
                table: "Agreements",
                column: "DocId",
                principalTable: "Docs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreements_Docs_DocId",
                table: "Agreements");

            migrationBuilder.DropIndex(
                name: "IX_Agreements_DocId",
                table: "Agreements");

            migrationBuilder.DropColumn(
                name: "VendorCode",
                table: "Nomenclatures");

            migrationBuilder.DropColumn(
                name: "DocId",
                table: "Agreements");
        }
    }
}
