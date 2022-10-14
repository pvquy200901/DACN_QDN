using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_Football.Migrations
{
    public partial class databasev054 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "userCreateTeamID",
                table: "Team",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_userCreateTeamID",
                table: "Team",
                column: "userCreateTeamID");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_User_userCreateTeamID",
                table: "Team",
                column: "userCreateTeamID",
                principalTable: "User",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_User_userCreateTeamID",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_userCreateTeamID",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "userCreateTeamID",
                table: "Team");
        }
    }
}
