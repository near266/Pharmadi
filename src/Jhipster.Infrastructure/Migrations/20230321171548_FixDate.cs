using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class FixDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateExp",
                table: "WarehouseProducts");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateExpire",
                table: "WarehouseProducts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateExpire",
                table: "WarehouseProducts");

            migrationBuilder.AddColumn<string>(
                name: "DateExp",
                table: "WarehouseProducts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
