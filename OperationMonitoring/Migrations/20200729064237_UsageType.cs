using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class UsageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsageTypeId",
                table: "Specifications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsageType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_UsageTypeId",
                table: "Specifications",
                column: "UsageTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_UsageType_UsageTypeId",
                table: "Specifications",
                column: "UsageTypeId",
                principalTable: "UsageType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_UsageType_UsageTypeId",
                table: "Specifications");

            migrationBuilder.DropTable(
                name: "UsageType");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_UsageTypeId",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "UsageTypeId",
                table: "Specifications");
        }
    }
}
