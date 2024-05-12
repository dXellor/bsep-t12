using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bsep_dll.Migrations
{
    /// <inheritdoc />
    public partial class Switch_Foreign_Key_Of_User_And_UserIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_user_identities_email",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "UserIdentityEmail",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_UserIdentityEmail",
                table: "users",
                column: "UserIdentityEmail",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_user_identities_UserIdentityEmail",
                table: "users",
                column: "UserIdentityEmail",
                principalTable: "user_identities",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_user_identities_UserIdentityEmail",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_UserIdentityEmail",
                table: "users");

            migrationBuilder.DropColumn(
                name: "UserIdentityEmail",
                table: "users");

            migrationBuilder.AddForeignKey(
                name: "FK_users_user_identities_email",
                table: "users",
                column: "email",
                principalTable: "user_identities",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
