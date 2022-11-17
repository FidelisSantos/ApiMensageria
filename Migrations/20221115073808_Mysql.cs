using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMensageria.Migrations
{
    public partial class Mysql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Genre = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTime>(type: "datetime(6)", maxLength: 100, nullable: false),
                    LoginModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserModelId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "login",
                columns: table => new
                {
                    LoginModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login", x => x.LoginModelId);
                    table.ForeignKey(
                        name: "FK_login_users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "users",
                        principalColumn: "UserModelId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "messagens",
                columns: table => new
                {
                    MessageModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sent = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserIssuerId = table.Column<int>(type: "int", nullable: false),
                    UserReceiverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messagens", x => x.MessageModelId);
                    table.ForeignKey(
                        name: "FK_messagens_users_UserIssuerId",
                        column: x => x.UserIssuerId,
                        principalTable: "users",
                        principalColumn: "UserModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messagens_users_UserReceiverId",
                        column: x => x.UserReceiverId,
                        principalTable: "users",
                        principalColumn: "UserModelId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_login_UserModelId",
                table: "login",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_messagens_UserIssuerId",
                table: "messagens",
                column: "UserIssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_messagens_UserReceiverId",
                table: "messagens",
                column: "UserReceiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "login");

            migrationBuilder.DropTable(
                name: "messagens");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
