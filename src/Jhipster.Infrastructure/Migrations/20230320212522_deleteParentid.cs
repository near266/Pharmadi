using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class deleteParentid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseProducts_Warehouse_WarehouseId",
                table: "WarehouseProducts");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseProducts_WarehouseId",
                table: "WarehouseProducts");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "WarehouseProducts");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Ton kho",
                table: "WarehouseProducts",
                newName: "AvailabelQuantity");

            migrationBuilder.AddColumn<string>(
                name: "DateExp",
                table: "WarehouseProducts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lot",
                table: "WarehouseProducts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateExp",
                table: "WarehouseProducts");

            migrationBuilder.DropColumn(
                name: "Lot",
                table: "WarehouseProducts");

            migrationBuilder.RenameColumn(
                name: "AvailabelQuantity",
                table: "WarehouseProducts",
                newName: "Ton kho");

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                table: "WarehouseProducts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Categories",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProducts_WarehouseId",
                table: "WarehouseProducts",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseProducts_Warehouse_WarehouseId",
                table: "WarehouseProducts",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
