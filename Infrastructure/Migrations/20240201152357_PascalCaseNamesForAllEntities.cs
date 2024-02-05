using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class PascalCaseNamesForAllEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "messageBody",
                table: "Messages",
                newName: "MessageBody");

            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "Messages",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "isPrivate",
                table: "Games",
                newName: "IsPrivate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageBody",
                table: "Messages",
                newName: "messageBody");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Messages",
                newName: "dateTime");

            migrationBuilder.RenameColumn(
                name: "IsPrivate",
                table: "Games",
                newName: "isPrivate");
        }
    }
}
