using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamNewBackend.Migrations
{
    public partial class thirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_Passwd",
                table: "Users",
                newName: "User_Password");

            migrationBuilder.RenameColumn(
                name: "Tier",
                table: "Users",
                newName: "Role");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "User_Password",
                table: "Users",
                newName: "User_Passwd");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Tier");
        }
    }
}
