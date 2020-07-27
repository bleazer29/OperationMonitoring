using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class AgreementFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFinish",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Agreements");

            migrationBuilder.AddColumn<string>(
                name: "MaintenanceReason",
                table: "Maintenances",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RelatedEntityId",
                table: "Docs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Docs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgreementNumber",
                table: "Agreements",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DocType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Docs_TypeId",
                table: "Docs",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Docs_DocType_TypeId",
                table: "Docs",
                column: "TypeId",
                principalTable: "DocType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Docs_DocType_TypeId",
                table: "Docs");

            migrationBuilder.DropTable(
                name: "DocType");

            migrationBuilder.DropIndex(
                name: "IX_Docs_TypeId",
                table: "Docs");

            migrationBuilder.DropColumn(
                name: "MaintenanceReason",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "RelatedEntityId",
                table: "Docs");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Docs");

            migrationBuilder.DropColumn(
                name: "AgreementNumber",
                table: "Agreements");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFinish",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Maintenances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Agreements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
