using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamNewBackend.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_Games_Game_Id",
                table: "UserGames");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_Users_User_Id",
                table: "UserGames");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "UserGames",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Game_Id",
                table: "UserGames",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGames_User_Id",
                table: "UserGames",
                newName: "IX_UserGames_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGames_Game_Id",
                table: "UserGames",
                newName: "IX_UserGames_GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_Games_GameId",
                table: "UserGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_Users_UserId",
                table: "UserGames",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_Games_GameId",
                table: "UserGames");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_Users_UserId",
                table: "UserGames");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserGames",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "UserGames",
                newName: "Game_Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserGames_UserId",
                table: "UserGames",
                newName: "IX_UserGames_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserGames_GameId",
                table: "UserGames",
                newName: "IX_UserGames_Game_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_Games_Game_Id",
                table: "UserGames",
                column: "Game_Id",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_Users_User_Id",
                table: "UserGames",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
