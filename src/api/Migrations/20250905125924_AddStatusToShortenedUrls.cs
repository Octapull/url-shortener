using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToShortenedUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ShortenedUrlId",
                table: "UrlClickStats",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ShortenedUrls",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UrlClickStats_ShortenedUrlId",
                table: "UrlClickStats",
                column: "ShortenedUrlId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlClickStats_ShortenedUrls_ShortenedUrlId",
                table: "UrlClickStats",
                column: "ShortenedUrlId",
                principalTable: "ShortenedUrls",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlClickStats_ShortenedUrls_ShortenedUrlId",
                table: "UrlClickStats");

            migrationBuilder.DropIndex(
                name: "IX_UrlClickStats_ShortenedUrlId",
                table: "UrlClickStats");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ShortenedUrls");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShortenedUrlId",
                table: "UrlClickStats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
