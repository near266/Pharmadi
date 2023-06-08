using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class DisPro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Range",
                table: "productDiscounts",
                newName: "Min");

            migrationBuilder.AddColumn<int>(
                name: "Max",
                table: "productDiscounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Max",
                table: "productDiscounts");

            migrationBuilder.RenameColumn(
                name: "Min",
                table: "productDiscounts",
                newName: "Range");
        }
    }
}
