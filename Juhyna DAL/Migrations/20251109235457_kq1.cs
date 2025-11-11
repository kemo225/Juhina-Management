using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juhyna_DAL.Migrations
{
    /// <inheritdoc />
    public partial class kq1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SaltAccessToken",
                table: "TokenAdmins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SaltRefreshToken",
                table: "TokenAdmins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaltAccessToken",
                table: "TokenAdmins");

            migrationBuilder.DropColumn(
                name: "SaltRefreshToken",
                table: "TokenAdmins");
        }
    }
}
