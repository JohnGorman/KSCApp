using Microsoft.EntityFrameworkCore.Migrations;

namespace KSCApp.Data.Migrations
{
    public partial class UpdatePlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PlayerId",
                table: "AspNetUsers",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Player_PlayerId",
                table: "AspNetUsers",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Player_PlayerId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PlayerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "AspNetUsers");
        }
    }
}
