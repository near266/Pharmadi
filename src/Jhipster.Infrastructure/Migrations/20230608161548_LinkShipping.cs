using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class LinkShipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkShipping",
                table: "PurchaseOrders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkShipping",
                table: "PurchaseOrders");
        }
    }
}
