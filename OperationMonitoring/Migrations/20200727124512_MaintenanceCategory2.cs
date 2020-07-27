using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class MaintenanceCategory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Docs_DocType_TypeId",
                table: "Docs");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_MaintenanceCategory_MaintenanceCategoryId",
                table: "Maintenances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceCategory",
                table: "MaintenanceCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocType",
                table: "DocType");

            migrationBuilder.RenameTable(
                name: "MaintenanceCategory",
                newName: "MaintenanceCategories");

            migrationBuilder.RenameTable(
                name: "DocType",
                newName: "DocTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceCategories",
                table: "MaintenanceCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocTypes",
                table: "DocTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Docs_DocTypes_TypeId",
                table: "Docs",
                column: "TypeId",
                principalTable: "DocTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_MaintenanceCategories_MaintenanceCategoryId",
                table: "Maintenances",
                column: "MaintenanceCategoryId",
                principalTable: "MaintenanceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Docs_DocTypes_TypeId",
                table: "Docs");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_MaintenanceCategories_MaintenanceCategoryId",
                table: "Maintenances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceCategories",
                table: "MaintenanceCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocTypes",
                table: "DocTypes");

            migrationBuilder.RenameTable(
                name: "MaintenanceCategories",
                newName: "MaintenanceCategory");

            migrationBuilder.RenameTable(
                name: "DocTypes",
                newName: "DocType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceCategory",
                table: "MaintenanceCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocType",
                table: "DocType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Docs_DocType_TypeId",
                table: "Docs",
                column: "TypeId",
                principalTable: "DocType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_MaintenanceCategory_MaintenanceCategoryId",
                table: "Maintenances",
                column: "MaintenanceCategoryId",
                principalTable: "MaintenanceCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
