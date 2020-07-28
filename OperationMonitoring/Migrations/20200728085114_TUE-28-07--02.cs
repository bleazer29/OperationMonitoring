using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationMonitoring.Migrations
{
    public partial class TUE280702 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assemblies_Equipment_EquipmentId",
                table: "Assemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentCategories_CategoryId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Departments_DepartmentId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentTypes_TypeId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentHistory_Equipment_EquipmentId",
                table: "EquipmentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Equipment_EquipmentId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Equipment_EquipmentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Equipment_EquipmentId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Equipment_EquipmentId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_EquipmentId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Parts_EquipmentId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EquipmentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_EquipmentId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentHistory_EquipmentId",
                table: "EquipmentHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipment",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Assemblies_EquipmentId",
                table: "Assemblies");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "EquipmentHistory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Assemblies");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentEquipId",
                table: "Stocks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentEquipId",
                table: "Parts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentEquipId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentEquipId",
                table: "Maintenances",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentEquipId",
                table: "EquipmentHistory",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Equipment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Equipment",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNum",
                table: "Equipment",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InventoryNum",
                table: "Equipment",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Equipment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Equipment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipId",
                table: "Equipment",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentEquipId",
                table: "Assemblies",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipment",
                table: "Equipment",
                column: "EquipId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_EquipmentEquipId",
                table: "Stocks",
                column: "EquipmentEquipId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_EquipmentEquipId",
                table: "Parts",
                column: "EquipmentEquipId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EquipmentEquipId",
                table: "Orders",
                column: "EquipmentEquipId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_EquipmentEquipId",
                table: "Maintenances",
                column: "EquipmentEquipId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistory_EquipmentEquipId",
                table: "EquipmentHistory",
                column: "EquipmentEquipId");

            migrationBuilder.CreateIndex(
                name: "IX_Assemblies_EquipmentEquipId",
                table: "Assemblies",
                column: "EquipmentEquipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assemblies_Equipment_EquipmentEquipId",
                table: "Assemblies",
                column: "EquipmentEquipId",
                principalTable: "Equipment",
                principalColumn: "EquipId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentCategories_CategoryId",
                table: "Equipment",
                column: "CategoryId",
                principalTable: "EquipmentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Departments_DepartmentId",
                table: "Equipment",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentTypes_TypeId",
                table: "Equipment",
                column: "TypeId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentHistory_Equipment_EquipmentEquipId",
                table: "EquipmentHistory",
                column: "EquipmentEquipId",
                principalTable: "Equipment",
                principalColumn: "EquipId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Equipment_EquipmentEquipId",
                table: "Maintenances",
                column: "EquipmentEquipId",
                principalTable: "Equipment",
                principalColumn: "EquipId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Equipment_EquipmentEquipId",
                table: "Orders",
                column: "EquipmentEquipId",
                principalTable: "Equipment",
                principalColumn: "EquipId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Equipment_EquipmentEquipId",
                table: "Parts",
                column: "EquipmentEquipId",
                principalTable: "Equipment",
                principalColumn: "EquipId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Equipment_EquipmentEquipId",
                table: "Stocks",
                column: "EquipmentEquipId",
                principalTable: "Equipment",
                principalColumn: "EquipId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assemblies_Equipment_EquipmentEquipId",
                table: "Assemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentCategories_CategoryId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Departments_DepartmentId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentTypes_TypeId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentHistory_Equipment_EquipmentEquipId",
                table: "EquipmentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Equipment_EquipmentEquipId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Equipment_EquipmentEquipId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Equipment_EquipmentEquipId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Equipment_EquipmentEquipId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_EquipmentEquipId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Parts_EquipmentEquipId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EquipmentEquipId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_EquipmentEquipId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentHistory_EquipmentEquipId",
                table: "EquipmentHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipment",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Assemblies_EquipmentEquipId",
                table: "Assemblies");

            migrationBuilder.DropColumn(
                name: "EquipmentEquipId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "EquipmentEquipId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "EquipmentEquipId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EquipmentEquipId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "EquipmentEquipId",
                table: "EquipmentHistory");

            migrationBuilder.DropColumn(
                name: "EquipId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "EquipmentEquipId",
                table: "Assemblies");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Maintenances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "EquipmentHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Equipment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "SerialNum",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "InventoryNum",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Equipment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Equipment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Assemblies",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipment",
                table: "Equipment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_EquipmentId",
                table: "Stocks",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_EquipmentId",
                table: "Parts",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EquipmentId",
                table: "Orders",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_EquipmentId",
                table: "Maintenances",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistory_EquipmentId",
                table: "EquipmentHistory",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assemblies_EquipmentId",
                table: "Assemblies",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assemblies_Equipment_EquipmentId",
                table: "Assemblies",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentCategories_CategoryId",
                table: "Equipment",
                column: "CategoryId",
                principalTable: "EquipmentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Departments_DepartmentId",
                table: "Equipment",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentTypes_TypeId",
                table: "Equipment",
                column: "TypeId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentHistory_Equipment_EquipmentId",
                table: "EquipmentHistory",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Equipment_EquipmentId",
                table: "Maintenances",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Equipment_EquipmentId",
                table: "Orders",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Equipment_EquipmentId",
                table: "Parts",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Equipment_EquipmentId",
                table: "Stocks",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
