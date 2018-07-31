using Microsoft.EntityFrameworkCore.Migrations;

namespace KSCApp.Data.Migrations
{
    public partial class EditMatchSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "MatchSlot",
                newName: "BookingSlot");

            migrationBuilder.AddColumn<int>(
                name: "JuniorLevels",
                table: "League",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JuniorLevels",
                table: "League");

            migrationBuilder.RenameColumn(
                name: "BookingSlot",
                table: "MatchSlot",
                newName: "MyProperty");
        }
    }
}
