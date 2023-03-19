using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orderings_OrderingId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orderings_OrderingId1",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "OrderingId1",
                table: "OrderItems",
                newName: "PurchaseOrderId1");

            migrationBuilder.RenameColumn(
                name: "OrderingId",
                table: "OrderItems",
                newName: "PurchaseOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderingId1",
                table: "OrderItems",
                newName: "IX_OrderItems_PurchaseOrderId1");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderingId",
                table: "OrderItems",
                newName: "IX_OrderItems_PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orderings_PurchaseOrderId",
                table: "OrderItems",
                column: "PurchaseOrderId",
                principalTable: "Orderings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orderings_PurchaseOrderId1",
                table: "OrderItems",
                column: "PurchaseOrderId1",
                principalTable: "Orderings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orderings_PurchaseOrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orderings_PurchaseOrderId1",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "PurchaseOrderId1",
                table: "OrderItems",
                newName: "OrderingId1");

            migrationBuilder.RenameColumn(
                name: "PurchaseOrderId",
                table: "OrderItems",
                newName: "OrderingId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_PurchaseOrderId1",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderingId1");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_PurchaseOrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orderings_OrderingId",
                table: "OrderItems",
                column: "OrderingId",
                principalTable: "Orderings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orderings_OrderingId1",
                table: "OrderItems",
                column: "OrderingId1",
                principalTable: "Orderings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
