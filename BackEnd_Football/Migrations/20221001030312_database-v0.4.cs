using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackEnd_Football.Migrations
{
    public partial class databasev04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_User_userNewsID",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_Stadium_User_userID",
                table: "Stadium");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_User_SqlUserID",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_SqlUserID",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Stadium_userID",
                table: "Stadium");

            migrationBuilder.DropColumn(
                name: "SqlUserID",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "userID",
                table: "Stadium");

            migrationBuilder.RenameColumn(
                name: "userNewsID",
                table: "News",
                newName: "userID");

            migrationBuilder.RenameIndex(
                name: "IX_News_userNewsID",
                table: "News",
                newName: "IX_News_userID");

            migrationBuilder.AddColumn<long>(
                name: "SqlTeamid",
                table: "User",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "managerID",
                table: "News",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "stateID",
                table: "News",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderStadium",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    orderTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    endTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    startTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isFinish = table.Column<bool>(type: "boolean", nullable: false),
                    isDelete = table.Column<bool>(type: "boolean", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    stateOrderID = table.Column<long>(type: "bigint", nullable: true),
                    userOrderID = table.Column<long>(type: "bigint", nullable: true),
                    stadiumOrderid = table.Column<long>(type: "bigint", nullable: true),
                    userManagerOrderID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStadium", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderStadium_Stadium_stadiumOrderid",
                        column: x => x.stadiumOrderid,
                        principalTable: "Stadium",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_OrderStadium_Statement_stateOrderID",
                        column: x => x.stateOrderID,
                        principalTable: "Statement",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OrderStadium_User_userOrderID",
                        column: x => x.userOrderID,
                        principalTable: "User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OrderStadium_UserSystem_userManagerOrderID",
                        column: x => x.userManagerOrderID,
                        principalTable: "UserSystem",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_SqlTeamid",
                table: "User",
                column: "SqlTeamid");

            migrationBuilder.CreateIndex(
                name: "IX_News_managerID",
                table: "News",
                column: "managerID");

            migrationBuilder.CreateIndex(
                name: "IX_News_stateID",
                table: "News",
                column: "stateID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStadium_stadiumOrderid",
                table: "OrderStadium",
                column: "stadiumOrderid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStadium_stateOrderID",
                table: "OrderStadium",
                column: "stateOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStadium_userManagerOrderID",
                table: "OrderStadium",
                column: "userManagerOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStadium_userOrderID",
                table: "OrderStadium",
                column: "userOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Statement_stateID",
                table: "News",
                column: "stateID",
                principalTable: "Statement",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_News_User_userID",
                table: "News",
                column: "userID",
                principalTable: "User",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_News_UserSystem_managerID",
                table: "News",
                column: "managerID",
                principalTable: "UserSystem",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Team_SqlTeamid",
                table: "User",
                column: "SqlTeamid",
                principalTable: "Team",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Statement_stateID",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_News_User_userID",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_News_UserSystem_managerID",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Team_SqlTeamid",
                table: "User");

            migrationBuilder.DropTable(
                name: "OrderStadium");

            migrationBuilder.DropIndex(
                name: "IX_User_SqlTeamid",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_News_managerID",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_stateID",
                table: "News");

            migrationBuilder.DropColumn(
                name: "SqlTeamid",
                table: "User");

            migrationBuilder.DropColumn(
                name: "managerID",
                table: "News");

            migrationBuilder.DropColumn(
                name: "stateID",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "userID",
                table: "News",
                newName: "userNewsID");

            migrationBuilder.RenameIndex(
                name: "IX_News_userID",
                table: "News",
                newName: "IX_News_userNewsID");

            migrationBuilder.AddColumn<long>(
                name: "SqlUserID",
                table: "Team",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "userID",
                table: "Stadium",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_SqlUserID",
                table: "Team",
                column: "SqlUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Stadium_userID",
                table: "Stadium",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_News_User_userNewsID",
                table: "News",
                column: "userNewsID",
                principalTable: "User",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadium_User_userID",
                table: "Stadium",
                column: "userID",
                principalTable: "User",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_User_SqlUserID",
                table: "Team",
                column: "SqlUserID",
                principalTable: "User",
                principalColumn: "ID");
        }
    }
}
