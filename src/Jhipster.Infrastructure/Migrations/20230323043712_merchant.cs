using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class merchant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "Merchants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Channel",
                table: "Merchants",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractNumber",
                table: "Merchants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Merchants",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeCustomer",
                table: "Merchants",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branch",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "Channel",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "ContractNumber",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "TypeCustomer",
                table: "Merchants");
        }
    }
}
