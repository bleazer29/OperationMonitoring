using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class UsageType3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Parts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "UsageTypes",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "General" });

            migrationBuilder.InsertData(
                table: "UsageTypes",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "Special" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsageTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UsageTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Parts");
        }
    }
}
