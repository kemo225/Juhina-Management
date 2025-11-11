using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juhyna_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Change7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Sales_SaleID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "SaleID",
                table: "Orders",
                newName: "CreatedBySaleID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_SaleID",
                table: "Orders",
                newName: "IX_Orders_CreatedBySaleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Sales_CreatedBySaleID",
                table: "Orders",
                column: "CreatedBySaleID",
                principalTable: "Sales",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Sales_CreatedBySaleID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "CreatedBySaleID",
                table: "Orders",
                newName: "SaleID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CreatedBySaleID",
                table: "Orders",
                newName: "IX_Orders_SaleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Sales_SaleID",
                table: "Orders",
                column: "SaleID",
                principalTable: "Sales",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
