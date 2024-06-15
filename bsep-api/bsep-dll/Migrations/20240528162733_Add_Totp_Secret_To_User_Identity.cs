using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bsep_dll.Migrations
{
    /// <inheritdoc />
    public partial class Add_Totp_Secret_To_User_Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TotpSecret",
                table: "user_identities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotpSecret",
                table: "user_identities");
        }
    }
}
