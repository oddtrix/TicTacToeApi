using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToeApi.Migrations
{
    public partial class PlayerModelDefaultValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 100,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarURL",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "https://static.wikia.nocookie.net/dota2_gamepedia/images/3/33/Bot_passive_icon.png/revision/latest?cb=20170711173119",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 100);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarURL",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "https://static.wikia.nocookie.net/dota2_gamepedia/images/3/33/Bot_passive_icon.png/revision/latest?cb=20170711173119");
        }
    }
}
