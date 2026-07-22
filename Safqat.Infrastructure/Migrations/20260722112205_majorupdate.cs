using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Safqat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class majorupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SafqaMedias_Safqat_SafqaId",
                table: "SafqaMedias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SafqaMedias",
                table: "SafqaMedias");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Safqat");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Safqat");

            migrationBuilder.RenameTable(
                name: "SafqaMedias",
                newName: "SafqatMedia");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Safqat",
                newName: "ItemsQuantity");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "SafqatMedia",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_SafqaMedias_SafqaId",
                table: "SafqatMedia",
                newName: "IX_SafqatMedia_SafqaId");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Safqat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedAt",
                table: "Safqat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Safqat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Safqat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCover",
                table: "SafqatMedia",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "SizeBytes",
                table: "SafqatMedia",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SafqatMedia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SafqatMedia",
                table: "SafqatMedia",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SafqatMedia_Safqat_SafqaId",
                table: "SafqatMedia",
                column: "SafqaId",
                principalTable: "Safqat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SafqatMedia_Safqat_SafqaId",
                table: "SafqatMedia");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SafqatMedia",
                table: "SafqatMedia");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Safqat");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "Safqat");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Safqat");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Safqat");

            migrationBuilder.DropColumn(
                name: "IsCover",
                table: "SafqatMedia");

            migrationBuilder.DropColumn(
                name: "SizeBytes",
                table: "SafqatMedia");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SafqatMedia");

            migrationBuilder.RenameTable(
                name: "SafqatMedia",
                newName: "SafqaMedias");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "ItemsQuantity",
                table: "Safqat",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "SafqaMedias",
                newName: "FilePath");

            migrationBuilder.RenameIndex(
                name: "IX_SafqatMedia_SafqaId",
                table: "SafqaMedias",
                newName: "IX_SafqaMedias_SafqaId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Safqat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Safqat",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SafqaMedias",
                table: "SafqaMedias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SafqaMedias_Safqat_SafqaId",
                table: "SafqaMedias",
                column: "SafqaId",
                principalTable: "Safqat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
