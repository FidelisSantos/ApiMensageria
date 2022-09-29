using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMensageria.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Genre = table.Column<char>(type: "TEXT", maxLength: 1, nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserModelId);
                });

            migrationBuilder.CreateTable(
                name: "login",
                columns: table => new
                {
                    LoginModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    UserModelId = table.Column<int>(type: "INTEGER", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "messagens",
                columns: table => new
                {
                    MessageModelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Sent = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserIssuerId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserReceiverId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messagens", x => x.MessageModelId);
                    table.ForeignKey(
                        name: "FK_messagens_users_UserReceiverId",
                        column: x => x.UserReceiverId,
                        principalTable: "users",
                        principalColumn: "UserModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_login_UserModelId",
                table: "login",
                column: "UserModelId",
                unique: true);

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
