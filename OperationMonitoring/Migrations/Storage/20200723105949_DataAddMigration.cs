using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations.Storage
{
    public partial class DataAddMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storages_Storages_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nomenclatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Specifications = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomenclatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nomenclatures_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    NomenclatureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Nomenclatures_NomenclatureId",
                        column: x => x.NomenclatureId,
                        principalTable: "Nomenclatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(nullable: false),
                    NomenclatureId = table.Column<int>(nullable: false),
                    StorageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Nomenclatures_NomenclatureId",
                        column: x => x.NomenclatureId,
                        principalTable: "Nomenclatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Address1", "Provider1" },
                    { 2, "8 Wang Hoi Road, Kowloon Bay, Hong Kong", "New Provider" },
                    { 3, "148 Wing Lok Street, Sheung Wan, Hong Kong", "AIP Company" }
                });

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, "Main Building", null },
                    { 2, "Side Building", null }
                });

            migrationBuilder.InsertData(
                table: "Nomenclatures",
                columns: new[] { "Id", "Amount", "Name", "ProviderId", "Specifications" },
                values: new object[,]
                {
                    { 1, 15, "Motor", 1, "Small" },
                    { 2, 0, "Spacer", 1, "Big" },
                    { 5, 5, "Ring", 1, "Big" },
                    { 6, 10, "Shaft", 1, "Big" },
                    { 3, 16, "Ring", 2, "Small" },
                    { 4, 15, "Shaft", 2, "Big" }
                });

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                    { 3, "Floor 1", 1 },
                    { 4, "Floor 2", 1 },
                    { 5, "Floor 3", 2 }
                });

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "Id", "Amount", "Date", "NomenclatureId" },
                values: new object[,]
                {
                    { 7, 15, new DateTime(2020, 7, 23, 13, 59, 47, 289, DateTimeKind.Local).AddTicks(4708), 1 },
                    { 3, 5, new DateTime(2020, 7, 13, 13, 59, 47, 289, DateTimeKind.Local).AddTicks(4607), 5 },
                    { 1, 5, new DateTime(2020, 7, 23, 13, 59, 47, 286, DateTimeKind.Local).AddTicks(6523), 6 },
                    { 2, 10, new DateTime(2020, 7, 13, 13, 59, 47, 289, DateTimeKind.Local).AddTicks(4444), 6 },
                    { 5, 15, new DateTime(2020, 4, 23, 13, 59, 47, 289, DateTimeKind.Local).AddTicks(4619), 3 },
                    { 6, 1, new DateTime(2020, 7, 23, 13, 59, 47, 289, DateTimeKind.Local).AddTicks(4705), 3 },
                    { 4, 15, new DateTime(2020, 7, 23, 13, 59, 47, 289, DateTimeKind.Local).AddTicks(4616), 4 }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Amount", "NomenclatureId", "StorageId" },
                values: new object[,]
                {
                    { 1, 5, 6, 4 },
                    { 3, 5, 1, 4 },
                    { 4, 5, 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                    { 6, "Room 4", 3 },
                    { 7, "Room 5", 3 },
                    { 8, "Room 6", 4 },
                    { 9, "Room 7", 4 }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Amount", "NomenclatureId", "StorageId" },
                values: new object[] { 2, 2, 6, 6 });

            migrationBuilder.CreateIndex(
                name: "IX_Nomenclatures_ProviderId",
                table: "Nomenclatures",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_NomenclatureId",
                table: "Purchases",
                column: "NomenclatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_NomenclatureId",
                table: "Stocks",
                column: "NomenclatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StorageId",
                table: "Stocks",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_ParentId",
                table: "Storages",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Nomenclatures");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
