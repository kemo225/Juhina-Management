using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juhyna_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Change5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bought",
                table: "InvoicebetweenSaleAdminstrative",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rest",
                table: "InvoicebetweenSaleAdminstrative",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "InvoicebetweenSaleAdminstrative",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bought",
                table: "InvoicebetweenSaleAdminstrative");

            migrationBuilder.DropColumn(
                name: "Rest",
                table: "InvoicebetweenSaleAdminstrative");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "InvoicebetweenSaleAdminstrative");
        }
    }
}
