using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juhyna_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Updation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Inventories",
                table: "Sales");


            migrationBuilder.DropColumn(
                name: "ManagerResponse",
                table: "Inventories");

            migrationBuilder.AddColumn<int>(
                name: "InventoryID",
                table: "Adminstratives",
                type: "int",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_Adminstratives_InventoryID",
                table: "Adminstratives",
                column: "InventoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Adminstratives_Inventories_InventoryID",
                table: "Adminstratives",
                column: "InventoryID",
                principalTable: "Inventories",
                principalColumn: "ID");

 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adminstratives_Inventories_InventoryID",
                table: "Adminstratives");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventories_Inventories_InventoryID1",
                table: "ProductInventories");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventories_InventoryID1",
                table: "ProductInventories");

            migrationBuilder.DropIndex(
                name: "IX_Adminstratives_InventoryID",
                table: "Adminstratives");

            migrationBuilder.DropColumn(
                name: "InventoryID1",
                table: "ProductInventories");

            migrationBuilder.DropColumn(
                name: "InventoryID",
                table: "Adminstratives");

            migrationBuilder.AddColumn<string>(
                name: "ManagerResponse",
                table: "Inventories",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_InventortID",
                table: "Sales",
                column: "InventortID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Inventories",
                table: "Sales",
                column: "InventortID",
                principalTable: "Inventories",
                principalColumn: "ID");
        }
    }
}
