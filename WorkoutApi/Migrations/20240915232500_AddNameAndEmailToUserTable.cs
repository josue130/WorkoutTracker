using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNameAndEmailToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "users",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "FullName");
        }
    }
}
