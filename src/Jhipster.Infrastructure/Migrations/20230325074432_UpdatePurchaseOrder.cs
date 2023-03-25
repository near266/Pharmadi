using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class UpdatePurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "PurchaseOrders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "PurchaseOrders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractNumber",
                table: "PurchaseOrders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantName",
                table: "PurchaseOrders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PurchaseOrders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ContractNumber",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "MerchantName",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PurchaseOrders");
        }
    }
}
