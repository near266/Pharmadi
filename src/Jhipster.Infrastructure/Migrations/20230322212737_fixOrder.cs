using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class fixOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orderings");

            migrationBuilder.AlterColumn<bool>(
                name: "Archived",
                table: "Tags",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "Archived",
                table: "Products",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "Archived",
                table: "Labels",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Carts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Carts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Carts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<bool>(
                name: "IsChoice",
                table: "Carts",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "IsChoice",
                table: "Carts");

            migrationBuilder.AlterColumn<bool>(
                name: "Archived",
                table: "Tags",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Archived",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Archived",
                table: "Labels",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Carts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Carts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Carts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            //migrationBuilder.CreateTable(
            //    name: "Orderings",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uuid", nullable: false),
            //        MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
            //        CreatedBy = table.Column<string>(type: "text", nullable: true),
            //        CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
            //        LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            //        ShippingFee = table.Column<decimal>(type: "numeric", nullable: false),
            //        Status = table.Column<int>(type: "integer", nullable: false),
            //        TotalPayment = table.Column<decimal>(type: "numeric", nullable: false),
            //        TotalPrice = table.Column<decimal>(type: "numeric", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Orderings", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Orderings_Merchants_MerchantId",
            //            column: x => x.MerchantId,
            //            principalTable: "Merchants",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrderItems",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uuid", nullable: false),
            //        ProductId = table.Column<Guid>(type: "uuid", nullable: false),
            //        PurchaseOrderId1 = table.Column<Guid>(type: "uuid", nullable: false),
            //        PurchaseOrderId = table.Column<Guid>(type: "uuid", nullable: false),
            //        Quantity = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrderItems", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_OrderItems_Orderings_PurchaseOrderId",
            //            column: x => x.PurchaseOrderId,
            //            principalTable: "Orderings",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_OrderItems_Orderings_PurchaseOrderId1",
            //            column: x => x.PurchaseOrderId1,
            //            principalTable: "Orderings",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_OrderItems_Products_ProductId",
            //            column: x => x.ProductId,
            //            principalTable: "Products",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Orderings_MerchantId",
            //    table: "Orderings",
            //    column: "MerchantId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderItems_ProductId",
            //    table: "OrderItems",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderItems_PurchaseOrderId",
            //    table: "OrderItems",
            //    column: "PurchaseOrderId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderItems_PurchaseOrderId1",
            //    table: "OrderItems",
            //    column: "PurchaseOrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
