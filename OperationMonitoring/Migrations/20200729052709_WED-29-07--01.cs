using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class WED290701 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Presets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    EquipmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presets_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PresetItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresetId = table.Column<int>(nullable: true),
                    NomenclatureId = table.Column<int>(nullable: true),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresetItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PresetItems_Nomenclatures_NomenclatureId",
                        column: x => x.NomenclatureId,
                        principalTable: "Nomenclatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PresetItems_Presets_PresetId",
                        column: x => x.PresetId,
                        principalTable: "Presets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PresetItems_NomenclatureId",
                table: "PresetItems",
                column: "NomenclatureId");

            migrationBuilder.CreateIndex(
                name: "IX_PresetItems_PresetId",
                table: "PresetItems",
                column: "PresetId");

            migrationBuilder.CreateIndex(
                name: "IX_Presets_EquipmentId",
                table: "Presets",
                column: "EquipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PresetItems");

            migrationBuilder.DropTable(
                name: "Presets");
        }
    }
}
