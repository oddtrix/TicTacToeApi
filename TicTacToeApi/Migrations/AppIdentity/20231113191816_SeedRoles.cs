using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToeApi.Migrations.AppIdentity
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("cfe5952e-3d20-47f2-be4e-fa7b7e72b93b"), "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d3819781-f572-4598-abb0-e7cd5775708d"), "2", "User", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfe5952e-3d20-47f2-be4e-fa7b7e72b93b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d3819781-f572-4598-abb0-e7cd5775708d"));
        }
    }
}
