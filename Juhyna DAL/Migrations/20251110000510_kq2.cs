using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juhyna_DAL.Migrations
{
    /// <inheritdoc />
    public partial class kq2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SaltAccessToken",
                table: "TokenSales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SaltRefreshToken",
                table: "TokenSales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SaltAccessToken",
                table: "TokenAdminstratives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SaltRefreshToken",
                table: "TokenAdminstratives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaltAccessToken",
                table: "TokenSales");

            migrationBuilder.DropColumn(
                name: "SaltRefreshToken",
                table: "TokenSales");

            migrationBuilder.DropColumn(
                name: "SaltAccessToken",
                table: "TokenAdminstratives");

            migrationBuilder.DropColumn(
                name: "SaltRefreshToken",
                table: "TokenAdminstratives");
        }
    }
}
