using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juhyna_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Change6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SaleID",
                table: "Orders",
                column: "SaleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Sales_SaleID",
                table: "Orders",
                column: "SaleID",
                principalTable: "Sales",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Sales_SaleID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SaleID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SaleID",
                table: "Orders");
        }
    }
}
