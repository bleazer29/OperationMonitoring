using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class Tue280705 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EquipmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Type1");

            migrationBuilder.UpdateData(
                table: "EquipmentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Type2");

            migrationBuilder.UpdateData(
                table: "EquipmentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Type3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EquipmentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Department1");

            migrationBuilder.UpdateData(
                table: "EquipmentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Department2");

            migrationBuilder.UpdateData(
                table: "EquipmentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Department3");
        }
    }
}
