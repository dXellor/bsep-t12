using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bsep_dll.Migrations
{
    /// <inheritdoc />
    public partial class Fix_User_Entity_Configuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "package",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_users_email",
                table: "users",
                column: "email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_users_email",
                table: "users");

            migrationBuilder.DropColumn(
                name: "package",
                table: "users");
        }
    }
}
