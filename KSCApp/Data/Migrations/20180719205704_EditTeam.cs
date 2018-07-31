using Microsoft.EntityFrameworkCore.Migrations;

namespace KSCApp.Data.Migrations
{
    public partial class EditTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "Team",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section",
                table: "Team");
        }
    }
}
