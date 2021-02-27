using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RektaRetailApp.Web.Migrations
{
    public partial class OrderCartFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCarts_Sales_SaleId",
                table: "OrderCarts");

            migrationBuilder.DropIndex(
                name: "IX_OrderCarts_SaleId",
                table: "OrderCarts");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "OrderCarts");

            migrationBuilder.RenameColumn(
                name: "EmptyCart",
                table: "OrderCarts",
                newName: "CloseCart");

            migrationBuilder.AddColumn<string>(
                name: "OrderCartSessionId",
                table: "OrderCarts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "OrderDate",
                table: "OrderCarts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Sales_OrderCardId",
                table: "Sales",
                column: "OrderCardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_OrderCarts_OrderCardId",
                table: "Sales",
                column: "OrderCardId",
                principalTable: "OrderCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_OrderCarts_OrderCardId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_OrderCardId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "OrderCartSessionId",
                table: "OrderCarts");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "OrderCarts");

            migrationBuilder.RenameColumn(
                name: "CloseCart",
                table: "OrderCarts",
                newName: "EmptyCart");

            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "OrderCarts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderCarts_SaleId",
                table: "OrderCarts",
                column: "SaleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCarts_Sales_SaleId",
                table: "OrderCarts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
