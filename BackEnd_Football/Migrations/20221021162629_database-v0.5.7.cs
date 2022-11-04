using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_Football.Migrations
{
    public partial class databasev057 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "News",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "News");
        }
    }
}
