using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class MaintenanceCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaintenanceCategoryId",
                table: "Maintenances",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MaintenanceCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_MaintenanceCategoryId",
                table: "Maintenances",
                column: "MaintenanceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_MaintenanceCategory_MaintenanceCategoryId",
                table: "Maintenances",
                column: "MaintenanceCategoryId",
                principalTable: "MaintenanceCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_MaintenanceCategory_MaintenanceCategoryId",
                table: "Maintenances");

            migrationBuilder.DropTable(
                name: "MaintenanceCategory");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_MaintenanceCategoryId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "MaintenanceCategoryId",
                table: "Maintenances");
        }
    }
}
