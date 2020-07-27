using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class DatabaseFix6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Width",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "Properties",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "OperatingTime",
                table: "Specifications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperatingTime",
                table: "Specifications");

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Specifications",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Properties",
                table: "Parts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
