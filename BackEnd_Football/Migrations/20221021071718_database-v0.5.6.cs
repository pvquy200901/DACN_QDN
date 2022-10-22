using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_Football.Migrations
{
    public partial class databasev056 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "content",
                table: "News",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "contact",
                table: "News",
                newName: "shortDes");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdTime",
                table: "News",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "News",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdTime",
                table: "News");

            migrationBuilder.DropColumn(
                name: "description",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "News",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "shortDes",
                table: "News",
                newName: "contact");
        }
    }
}
