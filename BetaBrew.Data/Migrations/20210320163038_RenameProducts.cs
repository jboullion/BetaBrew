using Microsoft.EntityFrameworkCore.Migrations;

namespace BetaBrew.Data.Migrations
{
    public partial class RenameProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventories_Product_ProductId",
                table: "ProductInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventorySnapshots_Product_ProductId",
                table: "ProductInventorySnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderItems_Product_ProductId",
                table: "SalesOrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventories_Products_ProductId",
                table: "ProductInventories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventorySnapshots_Products_ProductId",
                table: "ProductInventorySnapshots",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderItems_Products_ProductId",
                table: "SalesOrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventories_Products_ProductId",
                table: "ProductInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventorySnapshots_Products_ProductId",
                table: "ProductInventorySnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderItems_Products_ProductId",
                table: "SalesOrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventories_Product_ProductId",
                table: "ProductInventories",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventorySnapshots_Product_ProductId",
                table: "ProductInventorySnapshots",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderItems_Product_ProductId",
                table: "SalesOrderItems",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
