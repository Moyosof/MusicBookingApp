using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicBookingApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAristTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Artist");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Artist",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Artist",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Artist");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Artist",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Artist",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Artist",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Artist",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Artist",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Artist",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Artist",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Artist",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Artist",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Artist",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Artist",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Artist",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Artist",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Artist",
                type: "text",
                nullable: true);
        }
    }
}
