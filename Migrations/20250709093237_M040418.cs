using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace toytoy_store_backend.Migrations
{
    /// <inheritdoc />
    public partial class M040418 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Members",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Members",
                newName: "Family");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Members",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Family",
                table: "Members",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Members",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "Members",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
