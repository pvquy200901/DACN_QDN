using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_Football.Migrations
{
    public partial class databasev058 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SqlStateID",
                table: "User",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_SqlStateID",
                table: "User",
                column: "SqlStateID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Statements_SqlStateID",
                table: "User",
                column: "SqlStateID",
                principalTable: "Statements",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Statements_SqlStateID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_SqlStateID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SqlStateID",
                table: "User");
        }
    }
}
