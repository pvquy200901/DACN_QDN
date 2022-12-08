using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackEnd_Football.Migrations
{
    public partial class databasev0518 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "FoodDrink",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    sellPrice = table.Column<float>(type: "real", nullable: false),
                    createTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isDelete = table.Column<bool>(type: "boolean", nullable: false),
                    userSystemID = table.Column<long>(type: "bigint", nullable: true),
                    stateID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodDrink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodDrink_Statements_stateID",
                        column: x => x.stateID,
                        principalTable: "Statements",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FoodDrink_UserSystem_userSystemID",
                        column: x => x.userSystemID,
                        principalTable: "UserSystem",
                        principalColumn: "ID");
                });



            migrationBuilder.CreateTable(
                name: "ItemOrderFD",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codeOrder = table.Column<string>(type: "text", nullable: false),
                    idFD = table.Column<long>(type: "bigint", nullable: false),
                    nameFD = table.Column<string>(type: "text", nullable: false),
                    priceFD = table.Column<float>(type: "real", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    isDelete = table.Column<bool>(type: "boolean", nullable: false),
                    SqlOrderFDid = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOrderFD", x => x.id);
                });


            migrationBuilder.CreateTable(
                name: "OrderFD",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    createOrder = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updateOrder = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isDelete = table.Column<bool>(type: "boolean", nullable: false),
                    isFinish = table.Column<bool>(type: "boolean", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    stateOrderID = table.Column<long>(type: "bigint", nullable: true),
                    userManagerOrderID = table.Column<long>(type: "bigint", nullable: true),
                    orderStadiumid = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderFD", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderFD_Statements_stateOrderID",
                        column: x => x.stateOrderID,
                        principalTable: "Statements",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OrderFD_UserSystem_userManagerOrderID",
                        column: x => x.userManagerOrderID,
                        principalTable: "UserSystem",
                        principalColumn: "ID");
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "ItemOrderFD");


            migrationBuilder.DropTable(
                name: "OrderFD");

       
        }
    }
}
