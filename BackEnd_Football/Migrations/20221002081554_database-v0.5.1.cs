using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_Football.Migrations
{
    public partial class databasev051 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Statement_stateID",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStadium_Statement_stateOrderID",
                table: "OrderStadium");

            migrationBuilder.DropForeignKey(
                name: "FK_Stadium_Statement_stateID",
                table: "Stadium");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statement",
                table: "Statement");

            migrationBuilder.RenameTable(
                name: "Statement",
                newName: "Statements");

            migrationBuilder.AddColumn<DateTime>(
                name: "birthday",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statements",
                table: "Statements",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Statements_stateID",
                table: "News",
                column: "stateID",
                principalTable: "Statements",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStadium_Statements_stateOrderID",
                table: "OrderStadium",
                column: "stateOrderID",
                principalTable: "Statements",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadium_Statements_stateID",
                table: "Stadium",
                column: "stateID",
                principalTable: "Statements",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Statements_stateID",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStadium_Statements_stateOrderID",
                table: "OrderStadium");

            migrationBuilder.DropForeignKey(
                name: "FK_Stadium_Statements_stateID",
                table: "Stadium");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statements",
                table: "Statements");

            migrationBuilder.DropColumn(
                name: "birthday",
                table: "User");

            migrationBuilder.RenameTable(
                name: "Statements",
                newName: "Statement");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statement",
                table: "Statement",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Statement_stateID",
                table: "News",
                column: "stateID",
                principalTable: "Statement",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStadium_Statement_stateOrderID",
                table: "OrderStadium",
                column: "stateOrderID",
                principalTable: "Statement",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadium_Statement_stateID",
                table: "Stadium",
                column: "stateID",
                principalTable: "Statement",
                principalColumn: "ID");
        }
    }
}
