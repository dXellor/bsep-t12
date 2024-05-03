using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bsep_dll.Migrations
{
    /// <inheritdoc />
    public partial class User_UserIdentity_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "user_identities",
                type: "character varying(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_user_identities_users_email",
                table: "user_identities",
                column: "email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_identities_users_email",
                table: "user_identities");

            migrationBuilder.AddColumn<string>(
                name: "UserIdentityEmail",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "user_identities",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)");

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
    }
}
