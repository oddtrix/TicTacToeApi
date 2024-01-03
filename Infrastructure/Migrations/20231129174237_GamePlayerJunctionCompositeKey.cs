using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class GamePlayerJunctionCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayerJunction_Games_GameId",
                table: "GamePlayerJunction");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayerJunction_Players_PlayerId",
                table: "GamePlayerJunction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlayerJunction",
                table: "GamePlayerJunction");

            migrationBuilder.DropIndex(
                name: "IX_GamePlayerJunction_PlayerId",
                table: "GamePlayerJunction");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GamePlayerJunction");

            migrationBuilder.RenameTable(
                name: "GamePlayerJunction",
                newName: "GamePlayers");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlayerJunction_GameId",
                table: "GamePlayers",
                newName: "IX_GamePlayers_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlayers",
                table: "GamePlayers",
                columns: new[] { "PlayerId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayers_Games_GameId",
                table: "GamePlayers",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayers_Players_PlayerId",
                table: "GamePlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayers_Games_GameId",
                table: "GamePlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayers_Players_PlayerId",
                table: "GamePlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlayers",
                table: "GamePlayers");

            migrationBuilder.RenameTable(
                name: "GamePlayers",
                newName: "GamePlayerJunction");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlayers_GameId",
                table: "GamePlayerJunction",
                newName: "IX_GamePlayerJunction_GameId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GamePlayerJunction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlayerJunction",
                table: "GamePlayerJunction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerJunction_PlayerId",
                table: "GamePlayerJunction",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayerJunction_Games_GameId",
                table: "GamePlayerJunction",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayerJunction_Players_PlayerId",
                table: "GamePlayerJunction",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
