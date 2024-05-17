using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bsep_dll.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
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
                name: "ActivationToken",
                table: "user_identities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActivationTokenExpirationDateTime",
                table: "user_identities",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BlockedUntilDateTime",
                table: "user_identities",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Otp",
                table: "user_identities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpExpirationDateTime",
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
                name: "ActivationToken",
                table: "user_identities");

            migrationBuilder.DropColumn(
                name: "ActivationTokenExpirationDateTime",
                table: "user_identities");

            migrationBuilder.DropColumn(
                name: "BlockedUntilDateTime",
                table: "user_identities");

            migrationBuilder.DropColumn(
                name: "Otp",
                table: "user_identities");

            migrationBuilder.DropColumn(
                name: "OtpExpirationDateTime",
                table: "user_identities");
        }
    }
}
