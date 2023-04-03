using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class Productvst3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanOrder",
                table: "Products",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanOrder",
                table: "Products");
        }
    }
}
