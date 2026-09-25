using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pj_Inventory_System.Migrations
{
    /// <inheritdoc />
    public partial class AddUIDToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UID",
                table: "UserFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UID",
                table: "UserFiles");
        }
    }
}
