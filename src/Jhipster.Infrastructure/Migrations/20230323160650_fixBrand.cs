using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class fixBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_GroupBrands_GroupBrandId",
                table: "Brands");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupBrandId",
                table: "Brands",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_GroupBrands_GroupBrandId",
                table: "Brands",
                column: "GroupBrandId",
                principalTable: "GroupBrands",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_GroupBrands_GroupBrandId",
                table: "Brands");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupBrandId",
                table: "Brands",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_GroupBrands_GroupBrandId",
                table: "Brands",
                column: "GroupBrandId",
                principalTable: "GroupBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
