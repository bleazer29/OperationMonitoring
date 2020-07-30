using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class UsageType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_UsageType_UsageTypeId",
                table: "Specifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsageType",
                table: "UsageType");

            migrationBuilder.RenameTable(
                name: "UsageType",
                newName: "UsageTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsageTypes",
                table: "UsageTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_UsageTypes_UsageTypeId",
                table: "Specifications",
                column: "UsageTypeId",
                principalTable: "UsageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_UsageTypes_UsageTypeId",
                table: "Specifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsageTypes",
                table: "UsageTypes");

            migrationBuilder.RenameTable(
                name: "UsageTypes",
                newName: "UsageType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsageType",
                table: "UsageType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_UsageType_UsageTypeId",
                table: "Specifications",
                column: "UsageTypeId",
                principalTable: "UsageType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
