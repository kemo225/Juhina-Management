using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juhyna_DAL.Migrations
{
    /// <inheritdoc />
    public partial class mm1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "TokenSales",
                newName: "IsLogin");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "TokenAdminstratives",
                newName: "IsLogin");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "TokenAdmins",
                newName: "IsLogin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLogin",
                table: "TokenSales",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsLogin",
                table: "TokenAdminstratives",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsLogin",
                table: "TokenAdmins",
                newName: "IsActive");
        }
    }
}
