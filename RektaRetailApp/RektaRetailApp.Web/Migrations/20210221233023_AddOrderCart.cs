using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RektaRetailApp.Web.Migrations
{
    public partial class AddOrderCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsForSale_Sales_SaleId",
                table: "ProductsForSale");

            migrationBuilder.RenameColumn(
                name: "GrandTotal",
                table: "Sales",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "ProductsForSale",
                newName: "OrderCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsForSale_SaleId",
                table: "ProductsForSale",
                newName: "IX_ProductsForSale_OrderCartId");

            migrationBuilder.AddColumn<int>(
                name: "OrderCardId",
                table: "Sales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "ProductsForSale",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductsForSale",
                type: "decimal(9,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "ProductsForSale",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "ProductsForSale",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId2",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Inventories",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateTable(
                name: "OrderCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SaleId = table.Column<int>(type: "integer", nullable: false),
                    SalesStaffId = table.Column<string>(type: "text", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    EmptyCart = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderCarts_AspNetUsers_SalesStaffId",
                        column: x => x.SalesStaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderCarts_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderCarts_SaleId",
                table: "OrderCarts",
                column: "SaleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderCarts_SalesStaffId",
                table: "OrderCarts",
                column: "SalesStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsForSale_OrderCarts_OrderCartId",
                table: "ProductsForSale",
                column: "OrderCartId",
                principalTable: "OrderCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsForSale_OrderCarts_OrderCartId",
                table: "ProductsForSale");

            migrationBuilder.DropTable(
                name: "OrderCarts");

            migrationBuilder.DropColumn(
                name: "OrderCardId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "ProductsForSale");

            migrationBuilder.DropColumn(
                name: "InventoryId2",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Sales",
                newName: "GrandTotal");

            migrationBuilder.RenameColumn(
                name: "OrderCartId",
                table: "ProductsForSale",
                newName: "SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsForSale_OrderCartId",
                table: "ProductsForSale",
                newName: "IX_ProductsForSale_SaleId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "ProductsForSale",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductsForSale",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "ProductsForSale",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "Inventories",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsForSale_Sales_SaleId",
                table: "ProductsForSale",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
