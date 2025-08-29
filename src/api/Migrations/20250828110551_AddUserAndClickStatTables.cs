using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndClickStatTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                table: "ShortenedUrls",
                newName: "LastAccessedAt");

            migrationBuilder.AddColumn<long>(
                name: "ClickCount",
                table: "ShortenedUrls",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ShortenedUrls",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ShortenedUrls",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UrlClickStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShortenedUrlId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClickedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Referer = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlClickStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Provider = table.Column<string>(type: "text", nullable: false),
                    ProviderId = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortenedUrls_UserId",
                table: "ShortenedUrls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Provider_ProviderId",
                table: "Users",
                columns: new[] { "Provider", "ProviderId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShortenedUrls_Users_UserId",
                table: "ShortenedUrls",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortenedUrls_Users_UserId",
                table: "ShortenedUrls");

            migrationBuilder.DropTable(
                name: "UrlClickStats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ShortenedUrls_UserId",
                table: "ShortenedUrls");

            migrationBuilder.DropColumn(
                name: "ClickCount",
                table: "ShortenedUrls");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ShortenedUrls");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShortenedUrls");

            migrationBuilder.RenameColumn(
                name: "LastAccessedAt",
                table: "ShortenedUrls",
                newName: "CreatedOnUtc");
        }
    }
}
