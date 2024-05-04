using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bsep_dll.Migrations
{
    /// <inheritdoc />
    public partial class Add_RefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "refresh_token",
                table: "user_identities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "refresh_token_expires",
                table: "user_identities",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "refresh_token",
                table: "user_identities");

            migrationBuilder.DropColumn(
                name: "refresh_token_expires",
                table: "user_identities");
        }
    }
}
