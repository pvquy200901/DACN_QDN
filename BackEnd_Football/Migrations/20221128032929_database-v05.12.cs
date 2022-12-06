using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackEnd_Football.Migrations
{
    public partial class databasev0512 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "groupChat",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isDelete = table.Column<bool>(type: "boolean", nullable: false),
                    chat = table.Column<string>(type: "text", nullable: false),
                    time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    teamid = table.Column<long>(type: "bigint", nullable: true),
                    useNameID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groupChat", x => x.id);
                    table.ForeignKey(
                        name: "FK_groupChat_Team_teamid",
                        column: x => x.teamid,
                        principalTable: "Team",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_groupChat_User_useNameID",
                        column: x => x.useNameID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_groupChat_teamid",
                table: "groupChat",
                column: "teamid");

            migrationBuilder.CreateIndex(
                name: "IX_groupChat_useNameID",
                table: "groupChat",
                column: "useNameID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "groupChat");
        }
    }
}
