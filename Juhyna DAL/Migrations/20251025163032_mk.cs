using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juhyna_DAL.Migrations
{
    /// <inheritdoc />
    public partial class mk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventortID",
                table: "Sales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventortID",
                table: "Sales",
                type: "int",
                nullable: true);
        }
    }
}
