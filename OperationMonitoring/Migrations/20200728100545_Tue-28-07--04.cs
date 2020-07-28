using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class Tue280704 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Department1" },
                    { 2, "Department2" },
                    { 3, "Department3" }
                });

            migrationBuilder.InsertData(
                table: "EquipmentCategories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Category1" },
                    { 2, "Category2" },
                    { 3, "Category3" }
                });

            migrationBuilder.InsertData(
                table: "EquipmentTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Department1" },
                    { 2, "Department2" },
                    { 3, "Department3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquipmentCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquipmentCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquipmentCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquipmentTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquipmentTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquipmentTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
