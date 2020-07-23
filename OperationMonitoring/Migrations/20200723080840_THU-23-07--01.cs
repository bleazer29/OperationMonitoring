using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class THU230701 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Categories",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Categories", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Departments",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Departments", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EquipmentStatuses",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EquipmentStatuses", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Types",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Types", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Equipment",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        StatusId = table.Column<int>(nullable: true),
            //        DepartmentId = table.Column<int>(nullable: true),
            //        CategoryId = table.Column<int>(nullable: true),
            //        TypeId = table.Column<int>(nullable: true),
            //        Title = table.Column<string>(nullable: true),
            //        SerialNum = table.Column<string>(nullable: true),
            //        DiameterOuter = table.Column<int>(nullable: false),
            //        DiameterInner = table.Column<int>(nullable: false),
            //        Length = table.Column<int>(nullable: false),
            //        OperatingTime = table.Column<int>(nullable: false),
            //        WarningTime = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Equipment", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Equipment_Categories_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "Categories",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Equipment_Departments_DepartmentId",
            //            column: x => x.DepartmentId,
            //            principalTable: "Departments",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Equipment_EquipmentStatuses_StatusId",
            //            column: x => x.StatusId,
            //            principalTable: "EquipmentStatuses",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Equipment_Types_TypeId",
            //            column: x => x.TypeId,
            //            principalTable: "Types",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Maintenances",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        EquipmentId = table.Column<int>(nullable: true),
            //        DateStart = table.Column<DateTime>(nullable: false),
            //        DateDue = table.Column<DateTime>(nullable: false),
            //        DateFinish = table.Column<DateTime>(nullable: false),
            //        IsOpen = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Maintenances", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Maintenances_Equipment_EquipmentId",
            //            column: x => x.EquipmentId,
            //            principalTable: "Equipment",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Operations",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        EquipmentId = table.Column<int>(nullable: true),
            //        ClientId = table.Column<int>(nullable: true),
            //        ContractId = table.Column<int>(nullable: true),
            //        WellId = table.Column<int>(nullable: true),
            //        OperationInfo = table.Column<string>(nullable: true),
            //        DateStart = table.Column<DateTime>(nullable: false),
            //        DateFinish = table.Column<DateTime>(nullable: false),
            //        IsOpen = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Operations", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Operations_Equipment_EquipmentId",
            //            column: x => x.EquipmentId,
            //            principalTable: "Equipment",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Parts",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(nullable: true),
            //        SerialNum = table.Column<string>(nullable: true),
            //        Properties = table.Column<string>(nullable: true),
            //        OperationTime = table.Column<int>(nullable: false),
            //        WarningTime = table.Column<int>(nullable: false),
            //        EquipmentId = table.Column<int>(nullable: true),
            //        StatusId = table.Column<int>(nullable: true),
            //        IsUsed = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Parts", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Parts_Equipment_EquipmentId",
            //            column: x => x.EquipmentId,
            //            principalTable: "Equipment",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Parts_EquipmentStatuses_StatusId",
            //            column: x => x.StatusId,
            //            principalTable: "EquipmentStatuses",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.InsertData(
            //    table: "EquipmentStatuses",
            //    columns: new[] { "Id", "Title" },
            //    values: new object[,]
            //    {
            //        { 1, "RFU" },
            //        { 2, "JF" },
            //        { 3, "WS" },
            //        { 4, "SP" },
            //        { 5, "RP" },
            //        { 6, "Scrap" }
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Equipment_CategoryId",
            //    table: "Equipment",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Equipment_DepartmentId",
            //    table: "Equipment",
            //    column: "DepartmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Equipment_StatusId",
            //    table: "Equipment",
            //    column: "StatusId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Equipment_TypeId",
            //    table: "Equipment",
            //    column: "TypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Maintenances_EquipmentId",
            //    table: "Maintenances",
            //    column: "EquipmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Operations_EquipmentId",
            //    table: "Operations",
            //    column: "EquipmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Parts_EquipmentId",
            //    table: "Parts",
            //    column: "EquipmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Parts_StatusId",
            //    table: "Parts",
            //    column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "EquipmentStatuses");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
