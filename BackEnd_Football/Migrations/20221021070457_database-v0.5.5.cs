using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackEnd_Football.Migrations
{
    public partial class databasev055 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "File",
            //    columns: table => new
            //    {
            //        ID = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        key = table.Column<string>(type: "text", nullable: false),
            //        link = table.Column<string>(type: "text", nullable: false),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_File", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Statements",
            //    columns: table => new
            //    {
            //        ID = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        code = table.Column<int>(type: "integer", nullable: false),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        des = table.Column<string>(type: "text", nullable: false),
            //        isdeleted = table.Column<bool>(type: "boolean", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Statements", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserRole",
            //    columns: table => new
            //    {
            //        ID = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        code = table.Column<string>(type: "text", nullable: false),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        des = table.Column<string>(type: "text", nullable: false),
            //        note = table.Column<string>(type: "text", nullable: false),
            //        isdeleted = table.Column<bool>(type: "boolean", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserRole", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserSystem",
            //    columns: table => new
            //    {
            //        ID = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        user = table.Column<string>(type: "text", nullable: false),
            //        username = table.Column<string>(type: "text", nullable: false),
            //        password = table.Column<string>(type: "text", nullable: false),
            //        token = table.Column<string>(type: "text", nullable: false),
            //        isdeleted = table.Column<bool>(type: "boolean", nullable: false),
            //        phoneNumber = table.Column<string>(type: "text", nullable: false),
            //        des = table.Column<string>(type: "text", nullable: false),
            //        avatar = table.Column<string>(type: "text", nullable: false),
            //        roleID = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserSystem", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_UserSystem_UserRole_roleID",
            //            column: x => x.roleID,
            //            principalTable: "UserRole",
            //            principalColumn: "ID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Stadium",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        address = table.Column<string>(type: "text", nullable: false),
            //        contact = table.Column<string>(type: "text", nullable: false),
            //        images = table.Column<List<string>>(type: "text[]", nullable: false),
            //        isFinish = table.Column<bool>(type: "boolean", nullable: false),
            //        isDelete = table.Column<bool>(type: "boolean", nullable: false),
            //        createdTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        price = table.Column<int>(type: "integer", nullable: false),
            //        userSystemID = table.Column<long>(type: "bigint", nullable: true),
            //        stateID = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Stadium", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_Stadium_Statements_stateID",
            //            column: x => x.stateID,
            //            principalTable: "Statements",
            //            principalColumn: "ID");
            //        table.ForeignKey(
            //            name: "FK_Stadium_UserSystem_userSystemID",
            //            column: x => x.userSystemID,
            //            principalTable: "UserSystem",
            //            principalColumn: "ID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Comment",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        comments = table.Column<string>(type: "text", nullable: false),
            //        time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        Newsid = table.Column<long>(type: "bigint", nullable: true),
            //        useCommentsID = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Comment", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "News",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        content = table.Column<string>(type: "text", nullable: false),
            //        contact = table.Column<string>(type: "text", nullable: false),
            //        images = table.Column<List<string>>(type: "text[]", nullable: false),
            //        userID = table.Column<long>(type: "bigint", nullable: true),
            //        managerID = table.Column<long>(type: "bigint", nullable: true),
            //        stateID = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_News", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_News_Statements_stateID",
            //            column: x => x.stateID,
            //            principalTable: "Statements",
            //            principalColumn: "ID");
            //        table.ForeignKey(
            //            name: "FK_News_UserSystem_managerID",
            //            column: x => x.managerID,
            //            principalTable: "UserSystem",
            //            principalColumn: "ID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrderStadium",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        code = table.Column<string>(type: "text", nullable: false),
            //        orderTime = table.Column<int>(type: "integer", nullable: false),
            //        endTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        startTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        isFinish = table.Column<bool>(type: "boolean", nullable: false),
            //        isDelete = table.Column<bool>(type: "boolean", nullable: false),
            //        price = table.Column<int>(type: "integer", nullable: false),
            //        stateOrderID = table.Column<long>(type: "bigint", nullable: true),
            //        userOrderID = table.Column<long>(type: "bigint", nullable: true),
            //        stadiumOrderid = table.Column<long>(type: "bigint", nullable: true),
            //        userManagerOrderID = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrderStadium", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_OrderStadium_Stadium_stadiumOrderid",
            //            column: x => x.stadiumOrderid,
            //            principalTable: "Stadium",
            //            principalColumn: "id");
            //        table.ForeignKey(
            //            name: "FK_OrderStadium_Statements_stateOrderID",
            //            column: x => x.stateOrderID,
            //            principalTable: "Statements",
            //            principalColumn: "ID");
            //        table.ForeignKey(
            //            name: "FK_OrderStadium_UserSystem_userManagerOrderID",
            //            column: x => x.userManagerOrderID,
            //            principalTable: "UserSystem",
            //            principalColumn: "ID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Team",
            //    columns: table => new
            //    {
            //        id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        name = table.Column<string>(type: "text", nullable: false),
            //        shortName = table.Column<string>(type: "text", nullable: false),
            //        logo = table.Column<string>(type: "text", nullable: false),
            //        createdTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        quantity = table.Column<int>(type: "integer", nullable: false),
            //        address = table.Column<string>(type: "text", nullable: false),
            //        PhoneNumber = table.Column<string>(type: "text", nullable: false),
            //        imagesTeam = table.Column<List<string>>(type: "text[]", nullable: false),
            //        des = table.Column<string>(type: "text", nullable: false),
            //        isdeleted = table.Column<bool>(type: "boolean", nullable: false),
            //        userCreateTeamID = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Team", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "User",
            //    columns: table => new
            //    {
            //        ID = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        token = table.Column<string>(type: "text", nullable: false),
            //        UID = table.Column<string>(type: "text", nullable: false),
            //        Name = table.Column<string>(type: "text", nullable: false),
            //        username = table.Column<string>(type: "text", nullable: false),
            //        password = table.Column<string>(type: "text", nullable: false),
            //        Email = table.Column<string>(type: "text", nullable: false),
            //        PhotoURL = table.Column<string>(type: "text", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
            //        Phone = table.Column<string>(type: "text", nullable: false),
            //        ChucVu = table.Column<bool>(type: "boolean", nullable: false),
            //        birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        SqlTeamid = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_User", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_User_Team_SqlTeamid",
            //            column: x => x.SqlTeamid,
            //            principalTable: "Team",
            //            principalColumn: "id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comment_Newsid",
            //    table: "Comment",
            //    column: "Newsid");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comment_useCommentsID",
            //    table: "Comment",
            //    column: "useCommentsID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_News_managerID",
            //    table: "News",
            //    column: "managerID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_News_stateID",
            //    table: "News",
            //    column: "stateID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_News_userID",
            //    table: "News",
            //    column: "userID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderStadium_stadiumOrderid",
            //    table: "OrderStadium",
            //    column: "stadiumOrderid");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderStadium_stateOrderID",
            //    table: "OrderStadium",
            //    column: "stateOrderID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderStadium_userManagerOrderID",
            //    table: "OrderStadium",
            //    column: "userManagerOrderID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderStadium_userOrderID",
            //    table: "OrderStadium",
            //    column: "userOrderID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Stadium_stateID",
            //    table: "Stadium",
            //    column: "stateID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Stadium_userSystemID",
            //    table: "Stadium",
            //    column: "userSystemID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Team_userCreateTeamID",
            //    table: "Team",
            //    column: "userCreateTeamID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_User_SqlTeamid",
            //    table: "User",
            //    column: "SqlTeamid");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserSystem_roleID",
            //    table: "UserSystem",
            //    column: "roleID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Comment_News_Newsid",
            //    table: "Comment",
            //    column: "Newsid",
            //    principalTable: "News",
            //    principalColumn: "id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Comment_User_useCommentsID",
            //    table: "Comment",
            //    column: "useCommentsID",
            //    principalTable: "User",
            //    principalColumn: "ID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_News_User_userID",
            //    table: "News",
            //    column: "userID",
            //    principalTable: "User",
            //    principalColumn: "ID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderStadium_User_userOrderID",
            //    table: "OrderStadium",
            //    column: "userOrderID",
            //    principalTable: "User",
            //    principalColumn: "ID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Team_User_userCreateTeamID",
            //    table: "Team",
            //    column: "userCreateTeamID",
            //    principalTable: "User",
            //    principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Team_User_userCreateTeamID",
            //    table: "Team");

            //migrationBuilder.DropTable(
            //    name: "Comment");

            //migrationBuilder.DropTable(
            //    name: "File");

            //migrationBuilder.DropTable(
            //    name: "OrderStadium");

            //migrationBuilder.DropTable(
            //    name: "News");

            //migrationBuilder.DropTable(
            //    name: "Stadium");

            //migrationBuilder.DropTable(
            //    name: "Statements");

            //migrationBuilder.DropTable(
            //    name: "UserSystem");

            //migrationBuilder.DropTable(
            //    name: "UserRole");

            //migrationBuilder.DropTable(
            //    name: "User");

            //migrationBuilder.DropTable(
            //    name: "Team");
        }
    }
}
