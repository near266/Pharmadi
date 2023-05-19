using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class updatename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Function",
                table: "Products",
                newName: "UserObject");

            migrationBuilder.RenameColumn(
                name: "Effect",
                table: "Products",
                newName: "Warning");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Warning",
                table: "Products",
                newName: "Effect");

            migrationBuilder.RenameColumn(
                name: "UserObject",
                table: "Products",
                newName: "Function");
        }
    }
}
