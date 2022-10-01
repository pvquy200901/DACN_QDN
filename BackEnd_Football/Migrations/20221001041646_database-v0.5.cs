using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_Football.Migrations
{
    public partial class databasev05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token",
                table: "User");
        }
    }
}
