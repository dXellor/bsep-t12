using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace bsep_dll.Migrations
{
    /// <inheritdoc />
    public partial class Add_Reset_Password_Fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountStatus",
                table: "user_identities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "user_identities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetTokenExpirationDateTime",
                table: "user_identities",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountStatus",
                table: "user_identities");

            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "user_identities");

            migrationBuilder.DropColumn(
                name: "PasswordResetTokenExpirationDateTime",
                table: "user_identities");
        }
    }
}
