using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMensageria.Migrations
{
    public partial class ajuste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_login_users_UserModelId",
                table: "login");

            migrationBuilder.AddForeignKey(
                name: "FK_login_users_UserModelId",
                table: "login",
                column: "UserModelId",
                principalTable: "users",
                principalColumn: "UserModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_login_users_UserModelId",
                table: "login");

            migrationBuilder.AddForeignKey(
                name: "FK_login_users_UserModelId",
                table: "login",
                column: "UserModelId",
                principalTable: "users",
                principalColumn: "UserModelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
