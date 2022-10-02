using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiMensageria.Migrations
{
    public partial class ajusta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_messagens_UserIssuerId",
                table: "messagens",
                column: "UserIssuerId");

            migrationBuilder.AddForeignKey(
                name: "FK_messagens_users_UserIssuerId",
                table: "messagens",
                column: "UserIssuerId",
                principalTable: "users",
                principalColumn: "UserModelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messagens_users_UserIssuerId",
                table: "messagens");

            migrationBuilder.DropIndex(
                name: "IX_messagens_UserIssuerId",
                table: "messagens");
        }
    }
}
